// Server.cpp : Defines the entry point for the console application.
//
#include "stdafx.h"
#include <WinSock2.h>
#include <WS2tcpip.h>
#include <iostream>
#include <string>
#include "process.h"
#include "communication.h"
#include "shared_type.h"
#include "handle_request_function.h"
#include "define_variable.h"

using namespace std;
#pragma comment(lib, "Ws2_32.lib")

#define SERVER_ADDR "127.0.0.1"
#define PORT 5500


char rcv_buff[100];
char payload_buff[BUFF_SIZE];
char header_buff[BUFF_SIZE];
char send_buff_for_user[BUFF_SIZE];
char send_buff_for_other_user[BUFF_SIZE];

vector<User> users;
vector<Room> rooms;
/*
* @function handle_request: send message payload to the suitable message handler based on message method
* @param message(string): message detail
* @param client_socket(SOCKET): contain socket of request user
* @no return
*/
void handle_request(unsigned char, char*, SOCKET client_socket);

/*
* @function log_in_handler: verify user email and password based on account.txt
* @param email(string): user email
* @param password(string): user password
* @param client_socket(SOCKET): contain socket of request user
* @no return
*/
void login_handler(char[], SOCKET);


/*
* @function join_room_handler: add user to a room
* @param room_id(string): room id
* @param user_id(string): user id
* @param client_socket(SOCKET): contain socket of request user
* @no return
*/
void join_room_handler(char[], SOCKET);

/*
* @function bid_handler: update a new bid price, reset timer thread
* @param room_id(string): room id
* @param user_id(string): user id
* @param client_socket(SOCKET): contain socket of request user
* @no return
*/
void bid_handler(char[], SOCKET);

/*
* @function buy_immediately_handler: set new owner of the item, and timer thread immediately
* @param room_id(string): room id
* @param user_id(string): user id
* @param client_socket(SOCKET): contain socket of request user
* @no return
*/
void buy_now_handler(char[], SOCKET);

/*
* @function create_room_handler: create and run new timer thread, create new room in rooms list
* @param user_id(string): user id
* @param item_name(string): name of the item
* @param item_description(string): description of the item
* @param starting_price(int): starting price of the item
* @param buy_immediately_price(int): price that user can buy immediately
* @param client_socket(SOCKET): contain socket of request user
* @no return
*/
void create_room_handler(SOCKET client);

/*
* @function create_room_handler: create and run new timer thread, create new room in rooms list
* @param user_id(string): user id
* @param item_name(string): name of the item
* @param item_description(string): description of the item
* @param starting_price(int): starting price of the item
* @param buy_immediately_price(int): price that user can buy immediately
* @param client_socket(SOCKET): contain socket of request user
* @no return
*/
void sell_item_handler(string item_name, string item_description, int owner_id, int start_price, int buy_now_price, SOCKET client, int room_id);

/*
* @thread timer_thread: time count and notify the owner when the time is over
*/
unsigned __stdcall timer_thread(void *param);

/*
* @thread worker_thread: handle connection, new children worker_thread will be created when the number of parent thread exccess maximum number of 64 clients
*/
unsigned __stdcall worker_thread(void *param);
int main(int argc, char* argv[])
{
	//Initiate WinSock
	WSADATA wsaData;
	WORD wVersion = MAKEWORD(2, 2);
	if (WSAStartup(wVersion, &wsaData)) {
		printf("Winsock 2.2 is not supported\n");
		return 0;
	}

	//Construct LISTEN socket	
	SOCKET listenSock;
	listenSock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	//Bind address to socket
	sockaddr_in serverAddr;
	serverAddr.sin_family = AF_INET;
	serverAddr.sin_port = htons(PORT);
	inet_pton(AF_INET, SERVER_ADDR, &serverAddr.sin_addr);


	if (bind(listenSock, (sockaddr *)&serverAddr, sizeof(serverAddr)))
	{
		printf("Error %d: Cannot associate a local address with server socket.", WSAGetLastError());
		return 0;
	}

	// Listen request from client
	if (listen(listenSock, 10)) {
		printf("Error %d: Cannot place server socket in state LISTEN.", WSAGetLastError());
		return 0;
	}

	printf("Server started!\n");

	// bind listen sock to the first worker thread
	SOCKET param[2];
	param[0] = listenSock;
	param[1] = INVALID_SOCKET;

	_beginthreadex(0, 0, worker_thread, (void*)param, 0, 0);
	while (1) {
		// keep the server loop 
	}
	return 0;
}

unsigned __stdcall worker_thread(void *param) {
	// resource initialization
	DWORD		nEvents = 0;
	DWORD		index;
	SOCKET		socks[WSA_MAXIMUM_WAIT_EVENTS];
	WSAEVENT	events[WSA_MAXIMUM_WAIT_EVENTS];
	WSANETWORKEVENTS sockEvent;
	SOCKET connSock;
	sockaddr_in clientAddr;
	int thread_capacity_is_full = 0, ret, clientAddrLen = sizeof(clientAddr);

	//get listenSock from parent worker thread
	SOCKET listenSock = ((SOCKET*)param)[0];
	if (((SOCKET*)param)[1] != INVALID_SOCKET)
	{
		SOCKET connSock = ((SOCKET*)param)[1];
		//add connSock to array of clients, 
		//create an event and assign it to connSock with reading and closing event
		socks[1] = connSock;
		events[1] = WSACreateEvent();
		cout << "New client connected" << endl;
		WSAEventSelect(socks[1], events[1], FD_READ | FD_CLOSE);
		nEvents++;
	}
	//set first element of client array with listenSock
	socks[0] = listenSock;
	events[0] = WSACreateEvent(); //create new events
	nEvents++;

	// Assign an event types FD_ACCEPT and FD_CLOSE
	// with the listening socket and newEvent   
	WSAEventSelect(socks[0], events[0], FD_ACCEPT | FD_CLOSE);

	for (int i = 1; i < WSA_MAXIMUM_WAIT_EVENTS; i++) {
		if (i == 1 && ((SOCKET*)param)[1] != INVALID_SOCKET)
			continue;
		socks[i] = 0;
	}

	HANDLE worker_thread_handler;
	while (1) {
		//wait for network events on all socket
		index = WSAWaitForMultipleEvents(nEvents, events, FALSE, WSA_INFINITE, FALSE);
		if (index == WSA_WAIT_FAILED) {
			printf("Error %d: WSAWaitForMultipleEvents() failed\n", WSAGetLastError());
		}

		index = index - WSA_WAIT_EVENT_0;
		WSAEnumNetworkEvents(socks[index], events[index], &sockEvent);

		//reset event
		WSAResetEvent(events[index]);

		if (sockEvent.lNetworkEvents & FD_ACCEPT) {
			if (sockEvent.iErrorCode[FD_ACCEPT_BIT] != 0) {
				printf("FD_ACCEPT failed with error %d\n", sockEvent.iErrorCode[FD_READ_BIT]);
			}

			if ((connSock = accept(socks[index], (sockaddr *)&clientAddr, &clientAddrLen)) == SOCKET_ERROR) {
				printf("Error %d: Cannot permit incoming connection.\n", WSAGetLastError());
			}

			//Add new socket into socks array
			int i;
			if (nEvents == WSA_MAXIMUM_WAIT_EVENTS) {
				//check if there is no thread was created before
				if (thread_capacity_is_full == 0)
				{
					SOCKET param[2];
					param[0] = listenSock;
					param[1] = connSock;
					printf("Maximum clients reached: new worker thread will be created.\n");
					thread_capacity_is_full = 1;
					worker_thread_handler = (HANDLE)_beginthreadex(0, 0, worker_thread, (void*)param, 0, 0);
				}
			}
			else {
				printf("Accept incoming connection from client\n");
				socks[nEvents] = connSock;
				events[nEvents] = WSACreateEvent();
				WSAEventSelect(socks[nEvents], events[nEvents], FD_READ | FD_CLOSE);
				nEvents++;
			}
			for (i = 1; i < WSA_MAXIMUM_WAIT_EVENTS; i++)
				if (socks[i] == 0) {
					socks[i] = connSock;
					events[i] = WSACreateEvent();
					WSAEventSelect(socks[i], events[i], FD_READ | FD_CLOSE);
					nEvents++;
					break;
				}
		}

		if (sockEvent.lNetworkEvents & FD_READ) {
			//Receive message from client
			cout << "Read event" << endl;
			if (sockEvent.iErrorCode[FD_READ_BIT] != 0) {
				printf("FD_READ failed with error %d\n", sockEvent.iErrorCode[FD_READ_BIT]);
			}

			ret = recv(socks[index], rcv_buff, 5, 0);
			cout << rcv_buff << " " << ret << endl;

			if (ret == SOCKET_ERROR) {
				printf("Error %d", WSAGetLastError());
			}
			//Release socket and event if an error occurs
			if (ret <= 0) {
				closesocket(socks[index]);
				WSACloseEvent(events[index]);
				socks[index] = socks[nEvents - 1];
				events[index] = events[nEvents - 1];
				socks[nEvents - 1] = 0;
				events[nEvents - 1] = 0;
				nEvents--;
			}
			else {
				rcv_buff[ret] = 0;
				byte_stream_receiver(socks[index], payload_buff, rcv_buff, 0);
				handle_request((unsigned char)rcv_buff[0], payload_buff, socks[index]);
				//reset event
				WSAResetEvent(events[index]);
			}
		}

		if (sockEvent.lNetworkEvents & FD_CLOSE) {
			if (sockEvent.iErrorCode[FD_CLOSE_BIT] != 0) {
				printf("An account has unexpectedly disconnected \n");
			}

			//Release socket and event
			closesocket(socks[index]);
			WSACloseEvent(events[index]);
			socks[index] = socks[nEvents - 1];
			events[index] = events[nEvents - 1];
			socks[nEvents - 1] = 0;
			events[nEvents - 1] = 0;
			nEvents--;
		}
	}
	WaitForSingleObject(worker_thread_handler, INFINITE);
	return 0;

}
void login_handler(char payload_buff[], SOCKET s) {
	int send_bytes = login(payload_buff, s, rooms, users, send_buff_for_user);
	cout << "count user " << users.size();
	Send(s, send_buff_for_user, send_bytes, 0);
}

void create_room_handler(SOCKET client) {
	int send_bytes = create_room(client, users, rooms, send_buff_for_user, send_buff_for_other_user);
	cout << "count room " << rooms.size();

	// send response for user
	int ret1 = Send(client, send_buff_for_user, 6, 0);
	// send update for other user in system 
	for (int i = 0; i < users.size(); i++) {
		if (users[i].joined_room_id != -1) {
			int ret2 = Send(users[i].socket, send_buff_for_other_user, 6, 0);
		}
	}
};


void join_room_handler(char payload_buff[], SOCKET s) {
	int current_user_count;
	int room_id = payload_buff[0];
	int send_bytes = join_room(payload_buff, s, rooms, users, send_buff_for_user, current_user_count);
	Send(s, send_buff_for_user, send_bytes, 0);

	//send update information to other client
	send_buff_for_other_user[0] = NOTI_SUCCESS_JOIN_ROOM;
	int length = 1;
	memcpy(send_buff_for_other_user + 1, &length, 4);
	memcpy(send_buff_for_other_user + 5, &current_user_count, 4);
	for (auto &u : users) {
		if (u.joined_room_id != -1 && u.joined_room_id == room_id && u.socket != s) {
			Send(u.socket, send_buff_for_other_user, 9, 0);
		}
	}
};

void sell_item_handler(string item_name, string item_description, int owner_id, int start_price, int buy_now_price, SOCKET client, int room_id) {
	int send_bytes = sell_item(item_name, item_description, owner_id, start_price, buy_now_price, rooms, room_id, send_buff_for_user, send_buff_for_other_user);

	//hthread = (HANDLE)_beginthreadex(0, 0, timer_thread, (void *)room_id, 0, 0); //start time thread
	//rooms[room_id].timer_thread = hthread;

	int ret = Send(client, send_buff_for_user, 5, 0);
	if (ret == SOCKET_ERROR) {
		printf("Error %d", WSAGetLastError());
	}

	Room current_room;
	for (auto r : rooms) {
		if (r.room_id == room_id) {
			current_room = r;
		}
	}

	for (int i = 0; i < current_room.user_list.size(); i++) {
		if (users[i].user_id != owner_id) {
			int ret2 = Send(current_room.user_list[i].socket, send_buff_for_other_user, 6, 0);
		}
	}

	cout << "room id: " << room_id << " count item in room: " << current_room.item_list.size();

};

void bid_handler(char payload_buff[], SOCKET s) {
	int tmp;
	char user_name[102];
	int current_price;
	int room_id = payload_buff[0];
	int send_bytes = bid(payload_buff, s, rooms, users, send_buff_for_user, user_name, current_price);
	Send(s, send_buff_for_user, send_bytes, 0);
	//send to other user
	send_buff_for_other_user[0] = NOTI_SUCCESS_BID_ITEM;
	int length = 104;
	memcpy(send_buff_for_other_user + 1, &length, 4);
	memcpy(send_buff_for_other_user + 5, user_name, 100);
	memcpy(send_buff_for_other_user + 105, &current_price, 4);
	for (auto &u : users) {
		if (u.joined_room_id != -1 && u.joined_room_id == room_id && u.socket != s) {
			Send(u.socket, send_buff_for_other_user, 109, 0);
		}
	}

};

void buy_now_handler(char payload_buff[], SOCKET s) {
	int tmp;
	char user_name[102];
	int current_price;
	int room_id = payload_buff[0];
	int send_bytes = buy_now(payload_buff, s, rooms, users, send_buff_for_user, user_name);
	Send(s, send_buff_for_user, send_bytes, 0);
	//send to other user
	send_buff_for_other_user[0] = NOTI_SUCCESS_BID_ITEM;
	int length = 104;
	memcpy(send_buff_for_other_user + 1, &length, 4);
	memcpy(send_buff_for_other_user + 5, user_name, 100);
	for (auto &u : users) {
		if (u.joined_room_id != -1 && u.joined_room_id == room_id && u.socket != s) {
			Send(u.socket, send_buff_for_other_user, 109, 0);
		}
	}
};


void handle_request(unsigned char opcode, char* payloadBuff, SOCKET client_socket) {
	cout << "opcode " << (long)opcode << endl;
	if (opcode == LOGIN) {
		login_handler(payload_buff, client_socket);
	}
	else if (opcode == CREATEROOM) {
		create_room_handler(client_socket);
	}

	else if (opcode == JOINROOM) {
		join_room_handler(payload_buff, client_socket);
	}


	if (opcode == JOINROOM) {
		join_room_handler(payload_buff, client_socket);
	}
	else if (opcode == SELLITEM) {
		int room_id = payloadBuff[0];
		int owner_id = client_socket;
		char item_name[100];
		memcpy(item_name, payloadBuff + 3, 100);
		int start_price = *(int*)(payloadBuff + 103);
		int buy_now_price = *(int*)(payloadBuff + 107);
		char item_description[100];
		memcpy(item_name, payloadBuff + 111, 100);
		sell_item_handler(string(item_name), string(item_description), owner_id, start_price, buy_now_price, client_socket, room_id);
	}
	else if (opcode == BIDITEM) {
		bid_handler(payload_buff, client_socket);
	}
	else if (opcode == BUYNOW) {
		buy_now_handler(payloadBuff, client_socket);
	}
}
