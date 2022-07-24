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
	send_buff[0] = SUCCESS_LOGIN;
	for (unsigned int i = 0; i < rooms.size(); i++) {
		send_buff[i + 5] = rooms[i].room_id;
	}
	return payloadLen + 5;
};


int create_room(char user_name[], SOCKET client, vector<Room> list_room, char send_buff_for_user[], char send_buff_for_other_user[]) {
	Room new_room;
	User room_hoster;
	string name(user_name);
	room_hoster.joined_room_id = 1;
	room_hoster.user_id = client;
	room_hoster.socket = client;
	room_hoster.name = user_name;
	new_room.room_id = list_room.size();
	new_room.user_list.push_back(room_hoster);
	new_room.hosterName = user_name;
	list_room.push_back(new_room);
	int code_for_user = SUCCESS_CREATE_ROOM;
	int length = 1;
	memcpy(send_buff_for_user, &code_for_user, 1);
	memcpy(send_buff_for_user + 1, &length, 4);
	memcpy(send_buff_for_user + 5, &new_room.room_id, 1);

	int code_for_other_user = NOTI_SUCCESS_CREATE_ROOM;
	memcpy(send_buff_for_other_user, &code_for_other_user, 1);
	memcpy(send_buff_for_other_user + 1, &length, 4);
	memcpy(send_buff_for_other_user + 5, &new_room.room_id, 1);
	return 5;
}

int sell_item(string item_name, string item_description, int owner_id, int start_price, int buy_now_price, vector<Room> list_room, int room_id, char send_buff[]) {
	Item new_item;
	new_item.name = item_name;
	new_item.description = item_description;
	new_item.owner_id = owner_id;
	new_item.start_price = start_price;
	new_item.buy_now_price = buy_now_price;
	for (int i = 0; i < list_room.size(); i++) {
		if (list_room[i].room_id == room_id) {
			list_room[i].itemList.push_back(new_item);
		}
	}

	send_buff[0] = SUCCESS_SELL_ITEM;

	return 1;
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

