# FakeFootballApi

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Usage](#usage)
* [Updates](#updates)

## General info
Simple Api with JWT Bearer Authentication that provides user with fake football league teams and scores.

## Technologies
* ASP .Net Core 3.1 API
* JWT Bearer Authentication
* Hangfire
* AutoMapper

## Usage
Call the endpoints using tool f.ex. [Postman](https://www.postman.com/downloads/) or write your own client.
Remember that for /api/scores you need to be authenticated so you have to login (feature coming soon...)

**Endpoints:**
* GET
* POST
* PUT
* PATCH
* DELETE

Note:
There are several GET depending on what you provide (single id, date or team name). For more information contact me.

If you call the endpoint for teams like: [this](http://pptak-fakefootballapi.herokuapp.com/api/teams) for example in Postman, your output should be similar:
![photo](https://raw.githubusercontent.com/ptakpiotr/FakeFootballAPI/master/img.png)

Note:
For this call you do not have to be authenticated.

## Updates
*
