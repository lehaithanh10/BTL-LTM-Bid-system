#pragma once
#include <iostream>
#include <WinSock2.h>
#include <vector>
#include "shared_type.h"
using namespace std;

/*
* @function login: verify email and password
* @param email(string): room id
* @param password(string): user id
* @param client_socket(SOCKET): contain socket of request user

* @return response code (defined in status_code.h)
*/
int login(char[], SOCKET, vector<Room>, char[]);

/*
* @function show_room: display all created room
* @param room(vector<Room>*): created room list
* @return response code (defined in status_code.h)
*/
string view_room();

/*
* @function join_room: add user to room
* @param room_id(string): room id
* @param user_id(string): user id
* @param room(vector<Room>*): created room list
* @param user(vector<User>*): connected user list
* @return response code (defined in status_code.h)
*/
string join_room();

/*
* @function bid: reset timer thread and update new price, or refuse if information is invalid
* @param room_id(string): room id
* @param user_id(string): user id
* @param room(vector<Room>*): created room list
* @return response code (defined in status_code.h)
*/
string bid();

/*
* @function buy_immediately: end timer thread immediately and update new owner, or refuse if information is invalid
* @param room_id(string): room id
* @param user_id(string): user id
* @param room(vector<Room>*): created room list
* @return response code (defined in status_code.h)
*/
string buy_now();

/*
* @function buy_immediately: end timer thread immediately and update new owner, or refuse if information is invalid
* @param room_id(string): room id
* @param user_id(string): user id
* @param room(vector<Room>*): created room list
* @return response code (defined in status_code.h)
*/
string sell_item();


/*
* @function create_room: start new timer thread, or refuse if information is invalid
* @param user_id(string): user id
* @param item_name(string): name of the item
* @param item_description(string): description of the item
* @param starting_price(int): starting price of the item
* @param buy_immediately_price(int): price that user can buy immediately
* @param room(vector<Room>*): created room list
* @param id_count(int*): room of previous created room id, use to auto generate room id
* @return response code (defined in status_code.h)
*/
string create_room();