#include "stdafx.h"
#include "define_variable.h"
#include <string>
#include "communication.h"
#include "helpers.h"
#include "shared_type.h"
#pragma comment (lib, "Ws2_32.lib")
using namespace std;

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