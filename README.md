# Welcome to Webstep Fokus Asp.Net Core Fagdag!
This readme contains information for the Asp.Net Core WebApi hands-on exercise. 

## Your mission should you choose to accept it
The goal of this hands-on exercise is to implement a REST-based API for the booking department of the Forkus Hotel. 
This document will provide a specification of API calls with expected results and it will be up to you to implement them. 
You can then use tests to test if your implementation follows the spec. 

## Specification
What follows is the spec for the API. 
To keep it simple don't worry about JSON serialization details (like dates). These are up to your implementation

### Retrieve all room types
#### Request
* Route: /api/booking/roomtypes
* Method: Get
#### Response
* Status: 200 - Ok
* JSON Body: 
``` json
{ "roomTypes" : [ 
    "Single", "Double", "Twin", 
    "DeluxeDouble", "JuniorSuite", "Suite", 
    "ForkusSuite"
]}
```

### Book a room
#### Request
* Route: /api/booking/new
* Method: Post
* JOSN Body(example):
``` json
{ 
    "roomType" : "ForkusSuite",
    "startDate" : "2016-10-21T13:28:06.419Z",
    "numberOfNights" : "3",
    "guestName" : "Kjell Lj0stad"
}
```
#### Response on success: 
* Status: 201 - Created
* Location header: /api/booking/\{newBookingId\}
* JSON Body(example):
``` json
{ "bookingId" : "{newBookingId}" }
```
#### Response if roomtype is unknown
* Status: 400 - Bad request
* JSON Body:
``` json
{ "error" : "Specified room-type is unavailable for time period specified" }
```
#### Response if specified roomtype is not available for time period
* Staus: 409 - Conflict

### Retrieve details for a booking
* Route: /api/booking/{bookingId}
* Method: Get
#### Response on success: 
* Status: 200 - Ok
* JSON Body(example):
``` json
{ 
    "roomType" : "ForkusSuite",
    "startDate" : "2016-10-21T13:28:06.419Z",
    "numberOfNights" : "3",
    "guestName" : "Kjell Lj0stad",
    "paymentConfirmed" : false,
    "checkedIn" : true,
    "checkedOut" : false
}
```
#### Response if bookingId is unknown
* Status: 404 - Not found


Happy coding!