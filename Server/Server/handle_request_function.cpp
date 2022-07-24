#include <iostream>
#include  <WinSock2.h>
#include <vector>
#include "shared_type.h"
#include "stdafx.h"
#include "handle_request_function.h"
#include "define_variable.h"
#pragma comment (lib, "Ws2_32.lib")

using namespace std;

int login(char payload_buff[], SOCKET s, vector<Room> rooms, char send_buff[]) {
	User u;
	u.name = payload_buff;
	u.user_id = s;
	u.socket = s;
	int payloadLen = rooms.size();
	int tmp = payloadLen;
	for (int i = 4; i >= 1; i--) {
		send_buff[i] = (int)tmp % 256;
		tmp = tmp / 256;
	}
	send_buff[0] = 10;
	for (unsigned int i = 0; i < rooms.size(); i++) {
		send_buff[i + 5] = rooms[i].room_id;
	}
	return payloadLen + 5;
};


char* create_room(string user_id, string use_name, SOCKET client, vector<Room> list_room) {
	char response[BUFF_SIZE];
	Room new_room;
	User room_hoster;
	room_hoster.joined_room_id = 1;
	room_hoster.user_id = user_id;
	room_hoster.socket = client;
	room_hoster.name = use_name;
	new_room.room_id = list_room.size();
	new_room.user_list.push_back(room_hoster);
	new_room.hoster_id = user_id;
	list_room.push_back(new_room);
	response[5] = new_room.room_id;
	memcpy(response, SUCCESS_CREATE_ROOM + "0001", HEADER_LENGTH);
	return response;
}

string sell_item() {
	string res;
	return res;
}

string join_room() {
	string res;
	return res;
}

string bid() {
	string res;
	return res;
}

string buy_now() {
	string res;
	return res;
}

