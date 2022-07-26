#include <iostream>
#include <WinSock2.h>
#include <vector>
#include "shared_type.h"
#include "stdafx.h"
#include "handle_request_function.h"
#include "define_variable.h"
#include "communication.h"
#include "helpers.h"
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

Room* find_room_by_id(int id, vector<Room> rooms) {
	for (auto &u : rooms) {
		if (u.room_id == id)
			return &u;
	}
	return NULL;
}


int login(char payload_buff[], SOCKET s, vector<Room> rooms, vector<User> &users, char send_buff[]) {
	struct User u;

	string name(payload_buff);
	u.name = name;
	u.user_id = s;
	u.socket = s;
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


int create_room(SOCKET client, vector<User> &list_user, vector<Room> &list_room, char send_buff_for_user[], char send_buff_for_other_user[]) {
	Room new_room;
	for (int i = 0; i < list_user.size(); i++) {
		if (list_user[i].user_id == client) {
			list_user[i].joined_room_id = list_room.size();
			new_room.user_list.push_back(list_user[i]);
			new_room.hoster_name = list_user[i].name;
		}
	}
	new_room.room_id = list_room.size();
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

int sell_item(string item_name, string item_description, int owner_id, int start_price, int buy_now_price, vector<Room> &list_room, vector<User> users, int room_id, char send_buff_for_user[], char send_buff_for_other_user[]) {
	Item new_item;
	new_item.name = item_name;
	new_item.description = item_description;
	new_item.owner_id = owner_id;
	new_item.start_price = start_price;
	new_item.current_price = start_price;
	new_item.buy_now_price = buy_now_price;
	int item_quantity;
	for (int i = 0; i < list_room.size(); i++) {
		if (list_room[i].room_id == room_id) {
			if (list_room[i].item_list.size() == 0) {
				list_room[i].current_item = new_item;
				update_current_item(send_buff_for_other_user, item_name.c_str(), start_price, buy_now_price, item_description.c_str(), users, room_id);
			}
			list_room[i].item_list.push_back(new_item);
			item_quantity = list_room[i].item_list.size();
		}

	}

	int code_for_user = SUCCESS_SELL_ITEM;

	int length_for_user = 0;
	memcpy(send_buff_for_user, &code_for_user, 1);
	memcpy(send_buff_for_user + 1, &length_for_user, 4);
	int length_for_other = 4;
	int code_for_other_user = NOTI_SUCCESS_SELL_ITEM;
	memcpy(send_buff_for_other_user, &code_for_other_user, 1);
	memcpy(send_buff_for_other_user + 1, &length_for_other, 4);
	memcpy(send_buff_for_other_user + 5, &item_quantity, 4);
	return 1;
}

int join_room(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[], int& current_user_count) {

	int room_id = (unsigned char)payload_buff[0];
	for (auto &room : rooms) {
		if (room_id == room.room_id) {
			for (auto &user : users) {
				if (user.socket == s) {
					user.joined_room_id = room_id;
					room.user_list.push_back(user);
					current_user_count = room.user_list.size();
					send_buff[0] = SUCCESS_JOIN_ROOM;
					//if dont have any item on rooms
					if (room.item_list.size() == 0) {
						memset(send_buff, 0, sizeof(send_buff));
						send_buff[0] = SUCCESS_JOIN_ROOM;//opcode
						int payload_len = 320;
						memcpy(send_buff + 1, &payload_len, 4);//length
						memcpy(send_buff + 5, room.hoster_name.c_str(), 100);//userName
						int userQuantity = room.user_list.size();
						memcpy(send_buff + 105, &userQuantity, 4);//userQuantity
						return payload_len + HEADER_LENGTH;
					}
					//append send_buff
					int payload_len = 320 + room.current_item.description.size();
					//append header
					send_buff[0] = SUCCESS_JOIN_ROOM;
					memcpy(send_buff + 1, &payload_len, 4);
					//append payload

					memcpy(send_buff + 5, room.hoster_name.c_str(), user.name.size());//userHostName
					int user_quantity = room.user_list.size();
					memcpy(send_buff + 105, &user_quantity, 4);//userQuantity
					int item_quantity = room.item_list.size();
					memcpy(send_buff + 109, &item_quantity, 4);//itemQuantity
					for (auto &highest_user : users) {
						if (highest_user.user_id == room.current_highest_bid_user_id) {
							memcpy(send_buff + 113, highest_user.name.c_str(), 100);//currentHighestBidName
						}
					}

					memcpy(send_buff + 213, room.current_item.name.c_str(), 100);//currentItemName
					int currentPrice = room.current_item.current_price;

					memcpy(send_buff + 313, &currentPrice, 4);//currentPrice
					int startPrice = room.current_item.start_price;
					memcpy(send_buff + 317, &startPrice, 4);//startPrice
					int buyNowPrice = room.current_item.buy_now_price;
					memcpy(send_buff + 321, &buyNowPrice, 4);//buyNowPrice
					memcpy(send_buff + 325, room.current_item.description.c_str(), room.current_item.description.size());//Description
					return payload_len + HEADER_LENGTH;



				}
			}
		}
	}
	return 5;
}

<<<<<<< HEAD
int bid(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[], char send_buff_for_other_user[], char user_name[], int& current_price) {
=======
int bid(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[], char send_buff_for_other_user[]) {
>>>>>>> 046c4c2d2acba36587f82d89fb347c4c45bf639d
	int res;
	int room_id = *(unsigned char*)(payload_buff);
	int price = *(int*)(payload_buff + 1);
	for (auto &r : rooms) {//find room by id
		if (room_id == r.room_id) {
			for (auto &u : users) {// find user by id
				if (u.socket == s) {
					if (s == r.current_item.owner_id) {
						send_buff[0] = CREATOR_CANT_BID_ITEM;
						int length = 0;
						memcpy(send_buff + 1, &length, 4);
						return HEADER_LENGTH;
					}
					if (price < r.current_item.start_price && price < r.current_item.current_price) {
						send_buff[0] = INVALID_PRICE_BID;
						int length = 0;
						memcpy(send_buff + 1, &length, 4);
						return HEADER_LENGTH;
					}
					r.current_highest_bid_user_id = s;
<<<<<<< HEAD

=======
>>>>>>> 046c4c2d2acba36587f82d89fb347c4c45bf639d
					send_buff[0] = SUCCESS_BID_ITEM;
					int length = 0;
					memcpy(send_buff + 1, &length, 4);
					//Send noti to other user
					send_buff_for_other_user[0] = NOTI_SUCCESS_BID_ITEM;
					int length_for_other_user = 104;
					memcpy(send_buff_for_other_user + 1, &length_for_other_user, 4);
					memcpy(send_buff_for_other_user + 5, u.name.c_str(), 100);
					memcpy(send_buff_for_other_user + 105, &price, 4);
					for (auto &u : users) {
						if (u.joined_room_id == room_id && u.socket != s) {
							Send(u.socket, send_buff_for_other_user, 109, 0);
						}
					}
					//send update current item to other user
					return HEADER_LENGTH;
				}
			}
		}
	}
<<<<<<< HEAD
=======

>>>>>>> 046c4c2d2acba36587f82d89fb347c4c45bf639d

	return HEADER_LENGTH;
}

int buy_now(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[], char send_buff_for_other_user[]) {
	int room_id = *(unsigned char*)(payload_buff);
	int price = *(int*)(payload_buff + 1);
	for (auto &r : rooms) {//find room by id
		if (room_id == r.room_id) {
			for (auto &u : users) {// find user by id
				if (u.socket == s) {
					if (s == r.current_item.owner_id) {
						send_buff[0] = CREATOR_CANT_BUY_ITEM;
						int length = 0;
						memcpy(send_buff + 1, &length, 4);
						return HEADER_LENGTH;
					}

					if (price < r.current_item.buy_now_price) {
						send_buff[0] = INVALID_PRICE_BUY;
						int length = 0;
						memcpy(send_buff + 1, &length, 4);
						return HEADER_LENGTH;
					}
					send_buff[0] = SUCCESS_BUY_IMMEDIATELY;
					int length = 0;
					memcpy(send_buff + 1, &length, 4);
					//Send noti to other user
					send_buff_for_other_user[0] = NOTI_SUCCESS_BUY_NOW;
					int length_for_other_user = 100;
					memcpy(send_buff_for_other_user + 1, &length_for_other_user, 4);
					memcpy(send_buff_for_other_user + 5, u.name.c_str(), 100);
					for (auto &other_user : users) {
						if (other_user.joined_room_id == room_id && other_user.socket != s) {
							Send(other_user.socket, send_buff_for_other_user, 105, 0);
<<<<<<< HEAD
						}
						//update item list and send update
						if (r.item_list.size() > 0) {
							r.current_item = r.item_list[0];
							update_current_item(send_buff_for_other_user, r.current_item.name.c_str(), r.current_item.start_price, r.current_item.buy_now_price, r.current_item.description.c_str(), users, room_id);
						}
						else {
							update_current_item(send_buff_for_other_user, "", 0, 0, "", users, room_id);
							TerminateThread(rooms[room_id].timer_thread, 0);
						}

						return HEADER_LENGTH;
					}
=======

						}
					}
					//update item list and send update
					r.item_list.erase(r.item_list.begin());

					if (r.item_list.size() > 0) {
						r.current_item = r.item_list[0];
						update_current_item(send_buff_for_other_user, r.current_item.name.c_str(), r.current_item.start_price, r.current_item.buy_now_price, r.current_item.description.c_str(), users, room_id);
					}
					else {
						update_current_item(send_buff_for_other_user, "", 0, 0, "", users, room_id);
						TerminateThread(rooms[room_id].timer_thread, 0);
					}
					return HEADER_LENGTH;
>>>>>>> 046c4c2d2acba36587f82d89fb347c4c45bf639d
				}
			}
		}
		return HEADER_LENGTH;
	}
}

void leave_room(int room_id, int user_id, vector<Room> &rooms, vector<User> &users, char send_buff_for_user[], char send_buff_for_other_user[]) {
	for (int i = 0; i < rooms.size(); i++) {
		if (rooms[i].room_id == room_id) {
			for (int j = 0; j < rooms[i].user_list.size(); j++) {
				if ((rooms[i].user_list)[j].user_id == user_id) {
					cout << "user_id: " << user_id << endl;
					(rooms)[i].user_list.erase(((rooms)[i].user_list).begin() + j);
					int user_quantity = (rooms)[i].user_list.size();
					cout << "Left user in room: " << user_quantity << endl;
					send_buff_for_user[0] = SUCCESS_LEAVE_ROOM;
					send_buff_for_other_user[0] = NOTI_UPDATE_USER_QUANTITY;
					int length_for_user = 0;
					int length_for_other = 4;
					memcpy(send_buff_for_user + 1, &length_for_user, 4);
					memcpy(send_buff_for_other_user + 1, &length_for_other, 4);
					memcpy(send_buff_for_other_user + 5, &user_quantity, 4);
				}
			}
		}
	}
	for (int i = 0; i < (users).size(); i++) {
		if ((users)[i].user_id == user_id) {
			(users)[i].joined_room_id = -1;
			break;
		}
	}
}
