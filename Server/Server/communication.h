#pragma once
#include "winsock2.h"
#include "windows.h"
#include "stdio.h"
#include <WS2tcpip.h>
#include "string.h"
#include "iostream"
#include <vector>
#include "shared_type.h"
#define BUFF_SIZE 2048
#pragma comment (lib, "Ws2_32.lib")

using namespace std;

void byte_stream_receiver(SOCKET, char*, char*, int);

int Receive(SOCKET s, char *buff, int size, int flags);

int Send(SOCKET s, char *buff, int size, int flags);

/*
* @function send_time_notification: start new timer thread, or refuse if information is invalid
* @param user_id(string): user id
* @param item_name(string): name of the item
* @param item_description(string): description of the item
* @param starting_price(int): starting price of the item
* @param buy_immediately_price(int): price that user can buy immediately
* @param room(vector<Room>*): created room list
* @param id_count(int*): room of previous created room id, use to auto generate room id
* @return response code (defined in status_code.h)
*/
void send_time_notification(int room_id, char buff[], vector<Room> *rooms, int response_length);
