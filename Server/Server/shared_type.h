#pragma once

#include <string>
#include <vector>
#include <WS2tcpip.h>

using namespace std;

struct User {
	int user_id;
	SOCKET socket;
	int joined_room_id = -1;
	string name;
};

struct Item {
	int start_price;
	int buy_now_price;
	int current_price;
	int owner_id;
	string description;
	string name;
};

struct Room {
	int room_id;
	string hosterName;
	vector<User> user_list;
	vector<Item> item_list;
	Item current_item;
	int current_highest_bid_user_id;
	HANDLE timer_thread;

};

