#pragma once
#include <iostream>
#include <WinSock2.h>
#include <vector>
#include "shared_type.h"
using namespace std;

/*
* @function login: verify username and return list room for client
* @param payload_buff: payload receive from client
* @param s: Socket that connect with client
* @param rooms: list of room in server
* @param users: list of user have login to server
* @param send_buff: buffer that use to send message back to client
* @param accounts: list of account on database
* @param numact: number of account on database
* @return length of the message send back to client
*/
int login(char payload_buff[], SOCKET s, vector<Room> rooms, vector<User> &users, char send_buff[], string accounts[], int numact);


/*
* @function join_room: add user to room
* @param payload_buff: payload receive from client
* @param s: Socket that connect with client
* @param rooms: list of room in server
* @param users: list of user have login to server
* @param send_buff: buffer that use to send message back to client
* @param current_user_count: number of user in room
* @return length of the message send back to client
*/
int join_room(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[], int& current_user_count);

/*
* @function bid: verified the bid information and send back to client updated price for other user if bid success
* @param payload_buff: payload receive from client
* @param s: Socket that connect with client
* @param rooms: list of room in server
* @param users: list of user have login to server
* @param send_buff: buffer that use to send message back to client
* @param send_buff_for_other_user: buffer that send to other client(not client with socket s) in the same room with client with socket s if bid success
* @return length of the message send back to client
*/
int bid(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[], char send_buff_for_other_user[]);

/*
* @function buy_now: verified the buy_now information and send back to client updated price for other user if buy_now success
* @param payload_buff: payload receive from client
* @param s: Socket that connect with client
* @param rooms: list of room in server
* @param users: list of user have login to server
* @param send_buff: buffer that use to send message back to client
* @param send_buff_for_other_user: buffer that send to other client(not client with socket s) in the same room with client with socket s if buy_now success
* @return length of the message send back to client
*/
int buy_now(char payload_buff[], SOCKET s, vector<Room> &rooms, vector<User>& users, char send_buff[], char send_buff_for_other_user[]);


/*
* @function buy_immediately: end timer thread immediately and update new owner, or refuse if information is invalid
* @param item_name: name of item
* @param item_description: description of item
* @param owner_id: owner id of item
* @param start_price: start price of item
* @param buy_now_price: buy now price of item
* @param rooms: list of room in server
* @param users: list of user have login to server
* @param room_id(string): room id
* @param send_buff: buffer that use to send message back to client
* @param send_buff_for_other_user: buffer that send to other client(not client with socket s) in the same room with client with socket s if buy_now success
* @return length of the message send back to client
*/
int sell_item(string item_name, string item_description, int owner_id, int start_price, int buy_now_price, vector<Room> &list_room, vector<User>, int room_id, char send_buff_for_user[], char send_buff_for_other_user[]);


/*
* @function create_room: start new timer thread, or refuse if information is invalid
* @param client: Socket that connect with client
* @param rooms: list of room in server
* @param users: list of user have login to server
* @param send_buff: buffer that use to send message back to client
* @param send_buff_for_other_user: buffer that send to other client(not client with socket s) in the same room with client with socket s if buy_now success
* @return length of the message send back to client
*/
int create_room(SOCKET client, vector<User> &list_user, vector<Room> &list_room, char send_buff_for_user[], char send_buff_for_other_user[]);

/*
* @function leave_room: remove user in user list of room, return list current room for user
* @param room_id: room id
* @param user_id: user id
* @param rooms: list of room in server
* @param users: list of user have login to server
* @param send_buff: buffer that use to send message back to client
* @param send_buff_for_other_user: buffer that send to other client(not client with socket s) in the same room with client with socket s if buy_now success
* @return length of the message send back to client
*/
int leave_room(int room_id, int user_id, vector<Room> &rooms, vector<User> &users, char send_buff_for_user[], char send_buff_for_other_user[]);