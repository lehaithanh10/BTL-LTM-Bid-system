#include <iostream>
#include  <WinSock2.h>
#include <vector>
#include "shared_type.h"
#include "stdafx.h"
#include "handle_request_function.h"
#include "define_variable.h"
#pragma comment (lib, "Ws2_32.lib")

using namespace std;
User* find_user_by_id(int id, vector<User> users) {
	for (struct User &u : users) {
		if (u.user_id = id) {
			return &u;
		}
	}
	return NULL;
}
int login(char payload_buff[], SOCKET s, vector<Room> rooms,vector<User> &users, char send_buff[]) {
	struct User u ;
	string name(payload_buff);
	u.name = name;
	u.user_id = s;
	u.socket = s;
	cout << name << endl;
	cout << u.name << endl;
	users.push_back(u);
	int payload_len = rooms.size();


	send_buff[0] = SUCCESS_LOGIN;
	memcpy(send_buff + 1, &payload_len, 4);
	if (payload_len == 0) return 5;
	for (unsigned int i = 0; i < rooms.size(); i++) {
		send_buff[i + 5] = rooms[i].room_id;
	}
	return payload_len + 5;
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
			list_room[i].item_list.push_back(new_item);
		}
	}

	send_buff[0] = SUCCESS_SELL_ITEM;

	return 1;
}

int join_room(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[],int& current_user_count) {
	int room_id = payload_buff[0];
	for (auto &u : rooms) {
		if (room_id == u.room_id) {
			for (auto &v : users) {
				if (v.socket == s) {
					u.user_list.push_back(v);
					v.joined_room_id = room_id;
					current_user_count = u.user_list.size();
					send_buff[0] = SUCCESS_JOIN_ROOM;
					//if dont have any item on rooms
					if (u.item_list.size() == 0) {
						memset(send_buff, 0, sizeof(send_buff));
						send_buff[0] = SUCCESS_JOIN_ROOM;//opcode
						int payload_len = 320;
						memcpy(send_buff + 1, &payload_len, 4);//length
						memcpy(send_buff+5, v.name.c_str(), 100);//userName
						int userQuantity = u.user_list.size();
						memcpy(send_buff + 105,&userQuantity, 4);//userQuantity
						return payload_len + 5;
					}
					//append send_buff
					int payload_len = 320 + u.current_item.description.size();
					//append header
					send_buff[0] = SUCCESS_JOIN_ROOM;
					memcpy(send_buff + 1, &payload_len, 4);
					//append payload
					memcpy(send_buff+5, v.name.c_str(), 100);//userHostName
					int user_quantity = u.user_list.size();
					memcpy(send_buff + 105, &user_quantity, 4);//userQuantity
					int item_quantity = u.item_list.size();
					memcpy(send_buff + 109, &item_quantity, 4);//itemQuantity
					User* highest_bid = find_user_by_id(u.current_highest_bid_user_id, users);
					if(highest_bid != NULL)
						memcpy(send_buff + 113,highest_bid->name.c_str(), 100);//currentHighestBidName
					memcpy(send_buff + 213, u.current_item.name.c_str(), 100);//currentItemName
					int currentPrice = u.current_item.current_price;
					memcpy(send_buff + 313, &currentPrice, 4);//currentPrice
					int startPrice = u.current_item.start_price;
					memcpy(send_buff + 317, &startPrice, 4);//startPrice
					int buyNowPrice = u.current_item.buy_now_price;
					memcpy(send_buff + 321, &buyNowPrice, 4);//buyNowPrice
					memcpy(send_buff + 325, u.current_item.description.c_str(), u.current_item.description.size());//Description
					return payload_len + 5;

				}
			}
		}
	}
	return 99;
}

string bid() {
	string res;
	return res;
}

string buy_now() {
	string res;
	return res;
}

