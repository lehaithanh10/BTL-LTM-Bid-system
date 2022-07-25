
#include <WinSock2.h>
#include "stdio.h"
#include "stdafx.h"
#include "shared_type.h"
#include <WS2tcpip.h>
#include <iostream>
#include "vector"
#include "communication.h"
#define BUFF_SIZE 2048

#pragma comment (lib, "Ws2_32.lib")

using namespace std;

int findPayloadLen(char in[]) {
	int j; int res = 0;
	for (j = 1; j <= 4; j++) {
		int tmp = in[j];
		if (tmp < 0) tmp += 256;
		res = res * 256 + tmp;
	}
	return res;
}

void byte_stream_receiver(SOCKET s, char *payloadBuff, char* headerBuff, int flags) {
	int payloadLen = *(int*)(headerBuff+1);
	cout << "payloadLen" << payloadLen <<endl;
	if (payloadLen > 0) {
		int ret = Receive(s, payloadBuff, payloadLen, flags);
	}
	else
		memset(payloadBuff, 0, BUFF_SIZE);
}

int Receive(SOCKET s, char *buff, int size, int flags) {
	int n;

	n = recv(s, buff, size, flags);
	if (n == SOCKET_ERROR)
		printf("Error %d: Cannot receive data.\n", WSAGetLastError());
	else if (n == 0)
		printf("Client disconnects.\n");
	return n;
}

int Send(SOCKET s, char *buff, int size, int flags) {
	int n;

	n = send(s, buff, size, flags);
	if (n == SOCKET_ERROR) {
		printf("%s \n", buff);
		printf("Error %d: Cannot send data.\n", WSAGetLastError());
	}

	return n;
}

void send_time_notification(int room_id, char buff[], vector<Room> *rooms, int response_length) {
	for (int i = 0; i < (*rooms).size(); i++) {
		if ((*rooms)[i].room_id == room_id) {
			vector<User> participants = (*rooms)[i].user_list;
			for (int j = 0; j < participants.size(); j++) {
				cout << "send notification here" << endl;
				Send(participants[j].socket, buff, response_length, 0);
			}
		}
	}
}
