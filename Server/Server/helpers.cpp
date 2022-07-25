#include <iostream>
#include "stdafx.h"
#include "define_variable.h"
#include <string>
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