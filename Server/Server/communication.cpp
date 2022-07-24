
#include <WinSock2.h>
#include "stdio.h"
#include "stdafx.h"

#include <WS2tcpip.h>
#include <iostream>
#include "vector"
#include "communication.h"
#define BUFF_SIZE 2048

#pragma comment (lib, "Ws2_32.lib")

using namespace std;

int findPayloadLen(char in[]) {
	int j; int res = 0;
	for (j = 1; j <= 4; j++) {
		int tmp = in[j];
		if (tmp < 0) tmp += 256;
		res = res * 256 + tmp;
	}
	return res;
}

void byte_stream_receiver(SOCKET s, char *payloadBuff, char* headerBuff, int flags) {
	//cout << "headerBuff: " << headerBuff << endl ;
	int payloadLen = *(int*)(headerBuff+1);
	if (payloadLen > 0) {
		int ret = Receive(s, payloadBuff, payloadLen, flags);
	}
	else
		memset(payloadBuff, 0, BUFF_SIZE);
}

int Receive(SOCKET s, char *buff, int size, int flags) {
	int n;

	n = recv(s, buff, size, flags);
	if (n == SOCKET_ERROR)
		printf("Error %d: Cannot receive data.\n", WSAGetLastError());
	else if (n == 0)
		printf("Client disconnects.\n");
	return n;
}

int Send(SOCKET s, char *buff, int size, int flags) {
	int n;

	n = send(s, buff, size, flags);
	if (n == SOCKET_ERROR) {
		printf("%s \n", buff);
		printf("Error %d: Cannot send data.\n", WSAGetLastError());
	}

	return n;
}

