#include <iostream>
#include "stdafx.h"
#include "define_variable.h"
#include <string>
#include "communication.h"
#include "helpers.h"
#include "shared_type.h"
#pragma comment (lib, "Ws2_32.lib")
using namespace std;

void get_message_send_time_notification(int count, char send_buff_for_send_notification[], int current_price, int current_highest_bid_user_id) {
	int code_send_noti = TIME_NOTIFICATION;
	memcpy(send_buff_for_send_notification, &code_send_noti, 1);
	string message_payload;
	switch (count) {
	case 1: {
		message_payload = current_price + "VND the first time";
	};
			break;
	case 2: {
		message_payload = current_price + "VND the second time";
	}
			break;
	case 3: {
		message_payload = current_price + "VND the last time";

	}
			break;
	case 4: {
		int code_send_noti = NOTI_ITEM_SOLD;
		memcpy(send_buff_for_send_notification, &code_send_noti, 1);
		message_payload = current_highest_bid_user_id;
	}
			break;
	}
	memcpy(send_buff_for_send_notification, &message_payload, 100);
}
void update_current_item(char send_buff_for_other_user[],const char item_name[],int start_price, int buy_now_price,const char description[],vector<User> users,int room_id) {

	int length = 108 + strlen(description);
	send_buff_for_other_user[0] = NOTI_UPDATE_CURRENT_ITEM;
	memcpy(send_buff_for_other_user + 1, &length, 4);
	memcpy(send_buff_for_other_user + 5, item_name, 100);
	memcpy(send_buff_for_other_user + 105, &start_price, 4);
	memcpy(send_buff_for_other_user + 109, &buy_now_price, 4);
	for (auto &user : users) {
		if (user.joined_room_id == room_id) {
			Send(user.socket, send_buff_for_other_user, length + HEADER_LENGTH, 0);
		}
	}
}