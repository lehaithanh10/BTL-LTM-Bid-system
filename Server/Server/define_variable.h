#pragma once

// response code 
#define SUCCESS_LOGIN 10
#define SUCCESS_CREATE_ROOM 20
#define SUCCESS_JOIN_ROOM_RECEVICE_ROOM_INFO 30
#define SUCCESS_JOIN_ROOM_RECEVICE_CURRENT_ITEM_INFO 40
#define SUCCESS_SELL_ITEM 50
#define SUCCESS_BID_ITEM 60
#define CREATOR_CANT_BID_ITEM 61
#define INVALID_PRICE 62
#define SUCCESS_BUY_IMMEDIATELY 70
#define SUCCESS_BUY_ITEM 71
#define CREATOR_CANT_BUY_ITEM 72
#define SUCCESS_LEAVE_ROOM 90

// error code 
#define SOMETHING_WRONG_WHEN_LOGIN 19
#define SOMETHING_WRONG_WHEN_CREATEROOM 29
#define SOMETHING_WRONG_WHEN_JOINROOM 39
#define SOMETHING_WRONG_WHEN_SELLITEM 59
#define SOMETHING_WRONG_WHEN_BIDITEM 69
#define SOMETHING_WRONG_WHEN_BUYNOW 79

// common variable
#define BUFF_SIZE 2048
#define HEADER_LENGTH 5