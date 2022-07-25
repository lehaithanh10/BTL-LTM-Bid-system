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

void send_time_notification(int room_id, char buff[], vector<Room> *rooms);
