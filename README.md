# ConfR .NET API

## 1. Short coding guidelines
1. lowecase parameters
2. camelcase for fields and classes
3. interfaces with I
4. private fields with _ (underscore) at the beginning
5. in doubt please ask!
6. comments -> https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments
7. others -> https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/index


## 2. Informations about authorization


[OpenID Connect dicovery document](https://patronage-dotnet.azurewebsites.net/common/.well-known/openid-configuration)  
[ClientID](https://patronage-dotnet.azurewebsites.net/clientId)  
TenantID - 67cf6398-28ec-44cd-b3c0-881f597f02f3

### 2.1. Office365 users

```
rick.sanchez@patronage.onmicrosoft.com //@ (GOD)
beth.smith@patronage.onmicrosoft.com // @
morty.smith@patronage.onmicrosoft.com // @
```

### 2.2 Obtaining tokens for authorization


#### 2.2.1 Using Postman

1. Download [Postman](https://www.getpostman.com/downloads/) application.
2. Click on "Authorization" in request builder.
3. Select OAuth 2.0 type from drop down list and click "Get New Access Token" button.
4. Use following configuration:
    ```
    Grant Type:         Authorization Code
    Callback URL:       https://localhost
    Auth URL:           https://login.microsoftonline.com/common/oauth2/v2.0/authorize
    Access Token URL:   https://login.microsoftonline.com/common/oauth2/v2.0/token
    Client ID:          X
    Client Secret:      X
    Scope:              X
    ```
5. Click "Request Token" button and in pop-up window, log in using one of test accounts. 
6. Copy id_token and use it in request header "Authorization: Bearer token".
7. Copy access_token and use it in request header "Access_token: token".

#### 2.2.2 Using browser

1. Use following URL in browser:
    ```
    https://login.microsoftonline.com/67cf6398-28ec-44cd-b3c0-881f597f02f3/oauth2/v2.0/authorize?
    client_id=
    &response_type=id_token+token
    &redirect_uri=https%3A%2F%2Flocalhost
    &scope=openid
    &response_mode=fragment
    &state=12345
    &nonce=678910
    ```
2. All data will be in returned URL.
3. Copy id_token and use it in request header "Authorization: Bearer token".
4. Copy access_token and use it in request header "Access_token: token".

#### 2.2.3 In application for specific user

##### Request:
* URL: `https://login.microsoftonline.com/67cf6398-28ec-44cd-b3c0-881f597f02f3/oauth2/v2.0/token`
* Method: POST
* Content-Type: application/x-www-form-urlencoded
* Body:
```
    scope: user.read openid
    client_id: 50dd4cf2-61c2-47a8-99ac-079b2cf82ca3
    client_secret: X
    grant_type: password
    username: rick.sanchez@patronage.onmicrosoft.com
    password: X

```

##### Response:
```
{
    "token_type": "Bearer",
    "scope": "Calendars.ReadWrite Files.ReadWrite openid User.Read User.Read.All profile email",
    "expires_in": 3600,
    "ext_expires_in": 3600,
    "access_token": "X",
    "id_token": "X"
}
```

##### C# code example

```
public async Task<string> GetAccessToken()
{
    var client = new HttpClient();

    var url = _options.AuthUrl;

    var body = $"scope=user.read openid" +
                $"X";

    var stringContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

    var result = await client.PostAsync(url, stringContent);
    var content = await result.Content.ReadAsStringAsync();

    var json = JObject.Parse(content);

    return json["access_token"].ToString();
}
```

## 3. Controllers endpoints

### 3.1. Rooms Controller

#### [GET] GetAllRooms

Request:
* URL: /api/rooms
* Method: GET
* Headers: 
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* cURL:
```
curl -X GET "http://localhost:8000/api/Rooms"
-H "accept: text/plain"
-H "App-Key: X"
```

Response:
* 200 Success
```
{
  "rooms": [
    {
      "email": "string",
      "name": "string",
      "location": "string",
      "seatsNumber": 10 - constant number
    }
  ]
}
```
* 400 Bad Request
* 401 Unauthorized
* 500 Internal Server Error

#### [GET] GetRoomDetails

Request:
* URL: /api/rooms/(email)
* Method: GET
* Headers: 
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
* cURL:
```
curl -X GET "http://localhost:8000/api/Rooms/confroomdamian@patronage.onmicrosoft.com"
-H "accept: application/json"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
```

Response:
* 200 Success
```
{
  "name": "string",
  "companyName": "string",
  "department": "string",
  "email": "string",
  "location": "string",
  "streetAddress": "string",
  "city": "string",
  "seatsNumber": 0,
  "phoneNumber": "string"
}
```
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [GET] GetRoomReservations

Request:
* URL: /api/rooms/(email)/reservations
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
  * (query) from - DateTime UTC
  * (query) to - DateTime UTC
* cURL:
```
curl -X GET "http://localhost:52468/api/Rooms/thegarage@patronage.onmicrosoft.com/reservations?from=2019-05-19T12:00:00&to=2019-05-23T12:00:00"
-H "accept: text/plain"
-H "Authorization: Bearer id_token"
-H "Access_token: access_token"
-H "App-Key: X"
```

Response:
* 200 Success
```
{
  "reservations": [
    {
      "id": "string",
      "subject": "string",
      "startTime": "2019-05-08T11:04:41",
      "endTime": "2019-05-08T11:04:41"
    }
  ]
}
```
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [GET] GetRoomAvailability

Request:
* URL: /api/rooms/(email)/availability
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
  * (query) from - DateTime UTC
  * (query) to - DateTime UTC
  * (query) availabilityViewInterval - int (Time in minutes. Needs to be less than the checked time window and between 5 and 1440 minutes. Default value is 30 minutes.)
* cURL:
```
curl -X GET "http://localhost:52468/api/Rooms/thegarage%40patronage.onmicrosoft.com/availability?from=2019-05-19T12:00:00&to=2019-05-20T16:00:00"
-H "accept: text/plain"
-H "Authorization: Bearer id_token"
-H "Access_token: access_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
```

Response:
* 200 Success
```
{
  "isReserved": bool,
  "availabilityString": "string"
}
```
availabilityString - 0 = free, 1 = tentative, 2 = busy, 3 = out of office, 4 = working elsewhere
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [POST] CreateRoom - Not implemented

Request:
* URL: /api/rooms/
* Method: POST
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Body:
```
```
* cURL:
```
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [PUT] UpdateRoom - Not implemented

Request:
* URL: /api/rooms/(email)
* Method: PUT
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
* Body:
```
```
* cURL:
```

```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [DELETE] RemoveRoom - Not implemented

Request:
* URL: /api/rooms/(email)
* Method: DELETE
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
* cURL:
```

```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

### 3.2. Reservations Controller

#### [GET] GetAllReservations

Request:
* URL: /api/reservations/?mail={mail}&start={start}&count={count}
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (query) mail - string
  * (query) start - string($date-time) __is optional__  
  * (query) count - int32 __is optional__
* cURL:
```
curl -X GET "http://localhost:52467/api/Reservations?mail=black%40patronage.onmicrosoft.com&start=2019%2F05%2F24%2008%3A00%3A00&count=5"
-H 'accept: application/json'
-H "Authorization: Bearer id_token"
-H 'Access_token: access_token'
-H 'App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919'
```

Response:
* 200 Success
```
{
  "value": [
    {
      "id": "string",
      "subject": "string",
      "body": "string",
      "start": "2019-05-20T08:59:20.711Z",
      "end": "2019-05-20T08:59:20.711Z",
      "locations": [
        "string",
        "string",
        "string"
      ],
      "attendees": [
        {
          "type": "string",
          "status": {
            "response": "string",
            "time": "2019-05-20T08:59:20.711Z"
          },
          "emailAddress": {
            "name": "string",
            "address": "string"
          }
        }
      ],
      "organizer": {
        "emailAddress": {
          "name": "string",
          "address": "string"
        }
      }
    }
  ]
}
```
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [GET] GetReservation by ID
Request:
* URL: /api/reservations/id?id={id}&mail={mail}
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (query) id - string
  * (query) mail - string
* cURL:
```
curl -X GET "http://localhost:52467/api/Reservations/id?id=AAMkADMxYzdmN2M3LTVjYWItNDA1ZS04YTlhLWY0ZWQ1ZGE0YjdkMABGAAAAAAC1tKvdiIRvR7wsIz-lA-g9BwCiNsokqvTaQIO1K25R1zkiAAAAAAENAACiNsokqvTaQIO1K25R1zkiAAACJjr7AAA=&mail=black@patronage.onmicrosoft.com" 
-H 'accept: application/json'
-H "Authorization: Bearer id_token"
-H 'Access_token: access_token'
-H 'App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919'
```

Response:
* 200 Success
```
{
  "id": "string",
  "subject": "string",
  "body": "string",
  "start": "2019-05-20T21:37:00",
  "end": "2019-05-21T00:00:00",
  "locations": [
      "string",
      "string",
      "string"
  ],
  "attendees": [
    {
      "type": "string",
      "status": {
        "response": "string",
        "time": "2019-05-20T12:34:56"
      },
      "emailAddress": {
        "name": "string",
        "address": "string"
      }
    }
  ],
  "organizer": {
    "emailAddress": {
      "name": "string",
      "address": "string"
    }
  }
}
```
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [POST] CreateReservation

Request:
* URL: /api/reservations/(email)
* Method: POST
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Body:
  * DateTime UTC
  * Attendee type - required/optional
```
{
  "rooms": [
    "string"
  ],
  "from": "2019-05-08T11:26:42",
  "to": "2019-05-08T11:26:42",
  "subject": "string",
  "content": "string",
  "attendees": [
    {
      "type": "string",
      "emailAddress": {
        "name": "string",
        "address": "string"
      }
    }
  ]
}
```
* cURL:
```
curl -X POST  "http://localhost:8000/api/reservations"
-H 'Content-Type: application/json' 
-H "Authorization: Bearer id_token"
-H 'Access_token: access_token' 
-H 'App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919' 
-d '{
  "rooms": ["thegarage@patronage.onmicrosoft.com"],
  "from": "2019-05-07T20:30:00",
  "to": "2019-05-07T21:00:00",
  "subject": "TEST",
  "content": "Test",
  "attendees": [
        {
                "type": "required",
                "emailAddress": {
                    "name": "Beth Smith",
                    "address": "beth.smith@patronage.onmicrosoft.com"
                }
        },
    ]
}'
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [POST] CreateRandomRoomReservation

Request:
* URL: /api/reservations/random
* Method: POST
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Body:
  * DateTime UTC
  * Attendee type - required/optional
```
{
  "from": "2019-05-08T11:26:42",
  "to": "2019-05-08T11:26:42",
  "subject": "string",
  "content": "string",
  "attendees": [
    {
      "type": "string",
      "emailAddress": {
        "name": "string",
        "address": "string"
      }
    }
  ]
}
```
* cURL:
```
curl -X POST  "http://localhost:8000/api/reservations/random"
-H 'Content-Type: application/json' 
-H "Authorization: Bearer id_token"
-H 'Access_token: access_token' 
-H 'App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919'
-d '{
  "from": "2019-05-07T20:30:00",
  "to": "2019-05-07T21:00:00",
  "subject": "TEST",
  "content": "Test",
  "attendees": [
        {
                "type": "required",
                "emailAddress": {
                    "name": "Beth Smith",
                    "address": "beth.smith@patronage.onmicrosoft.com"
                }
        },
    ]
}'

```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [DELETE] DeleteReservation - for invited person

Request:
* URL: /api/reservations/{id}
* Method: DELETE
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (query) id - string
* cURL:
```
curl -X DELETE "http://localhost:52467/api/Reservations/{id}" 
-H 'accept: application/json'
-H "Authorization: Bearer id_token"
-H 'Access_token: access_token'
-H 'App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919'
```

Response:
* 204 Success
* 400 Failure
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

#### [DELETE] CancelReservation - for reservation owner 

Request:
* URL: /api/reservations/cancel?id={id}&comment={comment}
* Method: DELETE
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (query) id - string
  * (query) comment - string
* cURL:
```
curl -X DELETE "http://localhost:52467/api/Reservations/cancel?Id={id}&Comment={comment}
-H 'accept: application/json'
-H "Authorization: Bearer id_token"
-H 'Access_token: access_token'
-H 'App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919'
```

Response:
* 202 Accepted
* 400 Failure
* 401 Unauthorized
* 404 Not Found
* 500 Internal Server Error

### 3.3. Photos Controller

#### [GET] GetRoomPhotos

Request:
* URL: /api/rooms/photos/(email)
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
  * (query) requireSas - bool (true - link with a signature giving access to viewing photo for 24h)
* cURL:
```
curl -X GET "http://localhost:8000/api/Rooms/Photos/confroomdamian@patronage.onmicrosoft.com?requireSas=true"
-H "accept: text/plain"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
```

Response:
* 200 Success
```
[
  {
    "name": "string",
    "path": "string"
  }
]
```
* 400 Bad Request
* 401 Unauthorized
* 500 Internal Server Error

#### [GET] GetRoomPhoto

Request:
* URL: /api/rooms/photos/(email)/(name)
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
  * (path) name - string
  * (query) requireSas - bool (true - link with a signature giving access to viewing photo for 24h)
* cURL:
```
curl -X GET "http://localhost:52468/api/Rooms/Photos/confroomdamian@patronage.onmicrosoft.com/room-photo-132017904099910962.jpeg?requireSas=true"
-H "accept: text/plain"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
```

Response:
* 200 Success
```
{
  "name": "string",
  "path": "string"
}
```
* 400 Bad Request
* 401 Unauthorized
* 500 Internal Server Error

#### [POST] AddPhoto

Acceptable formats: PNG, JPG, JPEG  
Request:
* URL: /api/rooms/photos
* Method: POST
* Headers: 
  * Authorization: Bearer id_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Body:
```
{
  "roomEmail": "string",
  "photoUri": "string"
}
```
* cURL:
```
curl -X POST "http://localhost:8000/api/Rooms/Photos"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
-H "Content-Type: application/json-patch+json"
-d "{ \"roomEmail\": \"confroomdamian@patronage.onmicrosoft.com\", 
      \"photoUri\": \"https://i.etsystatic.com/14449774/r/il/51f082/1814091683/il_794xN.1814091683_i9j2.jpg\"}"
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 500 Internal Server Error

#### [PUT] UpdatePhoto

Acceptable formats: PNG, JPG, JPEG  
Request:
* URL: /api/rooms/photos/(email)
* Method: PUT
* Headers: 
  * Authorization: Bearer id_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
* Body:
```
{
  "roomEmail": "string",
  "photoName": "string",
  "photoUri": "string"
}
```
* cURL:
```
curl -X PUT "http://localhost:8000/api/Rooms/Photos/confroomdamian@patronage.onmicrosoft.com"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
-H "Content-Type: application/json-patch+json"
-d "{ \"roomEmail\": \"confroomdamian@patronage.onmicrosoft.com\",
      \"photoName\": \"room-photo-132017904099910962.jpeg\",
      \"photoUri\": \"https://ih0.redbubble.net/image.549432132.7024/st%2Csmall%2C215x235-pad%2C210x230%2Cf8f8f8.lite-1u2.jpg\"}"
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 500 Internal Server Error

#### [DELETE] RemovePhoto

Request:
* URL: /api/rooms/photos/(email)
* Method: DELETE
* Headers: 
  * Authorization: Bearer id_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
* Body:
```
{
  "roomEmail": "string",
  "photoName": "string"
}
```
* cURL:
```
curl -X DELETE "http://localhost:8000/api/Rooms/Photos/confroomdamian@patronage.onmicrosoft.com"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
-H "Content-Type: application/json-patch+json"
-d "{ \"roomEmail\": \"confroomdamian@patronage.onmicrosoft.com\",
      \"photoName\": \"room-photo-132017896417791086.jpeg\"}"
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 500 Internal Server Error

### 3.4. Comments Controller

#### [PUT] UpdateComment

Comment Id Expected format: Guid
Request:
* URL: /api/Rooms/Comments
* Method: PUT
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Body:
```
{
  "commentId": "string",
  "body": "string"
}
```
* cURL:
```
curl -X PUT "http://localhost:52467/api/Rooms/Comments"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
-H "Content-Type: application/json-patch+json"
-d "{ \"commentId\": \"4B1B883B-A898-4702-98A5-08D6DF5E5C2C\", \"body\": \"OK\"}"
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Comment not found
* 422 Invalid input format
* 500 Internal Server Error

#### [POST] CreateComment

Request:
* URL: /api/Rooms/Comments
* Method: POST
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Body:
```
{
  "roomEmail": "string",
  "body": "string"
}
```
* cURL:
```
curl -X POST "http://localhost:52467/api/Rooms/Comments"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
-H "Content-Type: application/json-patch+json"
-d "{ \"roomEmail\": \"silver@patronage.onmicrosoft.com\", \"body\": \"Nice\"}"
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Room not found
* 422 Invalid input format
* 500 Internal Server Error

#### [DELETE] DeleteComment

Comment Id Expected format: Guid
Request:
* URL: /api/Rooms/Comments/E175F9F9-8490-49A0-98A4-08D6DF5E5C2C
* Method: DELETE
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) commentId - string
* cURL:
```
curl -X DELETE "http://localhost:52467/api/Rooms/Comments/E175F9F9-8490-49A0-98A4-08D6DF5E5C2C"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
```

Response:
* 204 No Content
* 400 Bad Request
* 401 Unauthorized
* 404 Comment not found
* 422 Invalid input format
* 500 Internal Server Error

#### [GET] GetComment

Comment Id Expected format: Guid
Request:
* URL: /api/Rooms/Comments/(commentId)
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) commentId - string
* cURL:
```
curl -X GET "http://localhost:52467/api/Rooms/Comments/4B1B883B-A898-4702-98A5-08D6DF5E5C2C"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
```

Response:
* 200 Success
```
{
  "commentId": "string",
  "roomEmail": "string",
  "body": "string",
  "createdDateTime": "2019-05-23T12:05:20.231Z",
  "lastModifiedDateTime": "2019-05-23T12:05:20.231Z",
  "userDisplayName": "string"
}
```
* 400 Bad Request
* 401 Unauthorized
* 404 Comment not found
* 422 Invalid input format
* 500 Internal Server Error

#### [GET] GetComment

Comment Id Expected format: Guid
Request:
* URL: /api/Rooms/Comments/all/(email)
* Method: GET
* Headers: 
  * Authorization: Bearer id_token
  * Access_token: access_token
  * App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919
* Parameters:
  * (path) email - string
* cURL:
```
curl -X GET "http://localhost:52467/api/Rooms/Comments/all/black%40patronage.onmicrosoft.com"
-H "accept: application/json"
-H "Authorization: Bearer id_token"
-H "App-Key: 5c3a725a-525b-4d2f-838d-5030ec46d919"
```

Response:
* 200 Success
```
[
  {
    "commentId": "string",
    "roomEmail": "string",
    "body": "string",
    "createdDateTime": "2019-05-23T12:12:03.251Z",
    "lastModifiedDateTime": "2019-05-23T12:12:03.251Z",
    "userDisplayName": "string"
  }
]
```
* 400 Bad Request
* 401 Unauthorized
* 404 Room not found
* 422 Invalid input format
* 500 Internal Server Error

## 4. SignalR Server-Sent Events

### 4.1. Information for hub clients

Hub URL: `/confrhub`  
Java client:
* [Client](https://docs.microsoft.com/en-us/aspnet/core/signalr/java-client?view=aspnetcore-2.2)
* [API reference](https://docs.microsoft.com/en-us/java/api/com.microsoft.signalr?view=aspnet-signalr-java&viewFallbackFrom=aspnetcore-2.2)

JavaScript client:
* [Client](https://docs.microsoft.com/en-us/aspnet/core/signalr/javascript-client?view=aspnetcore-2.2)
* [API reference](https://docs.microsoft.com/en-us/javascript/api/)

### 4.2. Hub authorization

* Bearer authorization
* App-Key

### 4.3. Client methods from hub

#### 4.3.1. Reservations:

##### New reservation
* Method: `AddedRoomReservation`
* Response:
  *  string - room email
  *  Reservation - information about reservation

```
    public class Reservation
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
```

##### Canceled reservation
* Method: `CanceledRoomReservation`
* Response:
  *  string - room email
  *  Reservation - information about reservation

```
    public class Reservation
    {
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
```

#### 4.3.2. Photos:

##### Added room photo
* Method: `CanceledRoomReservation`
* Response:
  *  string - room email
  *  RoomPhoto - information about new photo

```
    public class RoomPhoto
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
```

##### Updated room photo
* Method: `UpdatedRoomPhoto`
* Response:
  *  string - room email
  *  string - old photo name
  *  RoomPhoto - information about updated photo

```
    public class RoomPhoto
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
```

##### Removed room photo
* Method: `RemovedRoomPhoto`
* Response:
  *  string - room email
  *  string - removed photo name


# Version control guidelines

## The flow
* Commits with corrections (eg. done in response to code review feedback) mustn't be preserved as standalone commits, but squashed with the entire submission to keep version history clean.
* Make sure to understand the implications of rewriting version history, keep local backup and **DO NOT** overwrite `development` nor `master`.

* Whenever merged branches lag behind the target branch (`development` or `master`), they need to be **rebased** on the target branch first. We only merge *towards* trunk branches, not the other way around:
    * ![#c5f015](https://placehold.it/15/c5f015/000000?text=+) `feature/PATRONSZCZ19-123-short-desc` → `development` is good.
    * ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) ~~`development` → `features/PATRONSZCZ19-123-short-desc`~~ is bad, rebase `features/PATRONSZCZ19-123-short-desc` on `development` if it lags behind it.

## Branch naming
Branch naming follows the `type/PATRONSZCZ19-123-short-desc` convention where `123` stands for the number of the task on JIRA while `type` defines the general character of the work. It can be:
* `feature` - for submissions that add or enhance app's functionality
* `bugfix` - self-explanatory
* `refactoring` - for submissions that alter code without an impact on its functionality (eg. code cleanup or replacing a third-party dependency with another)
* `docs` - for documenting (adding files or updating README.md).

Other may be added on an as-needed basis.

## Commit messages

The following format is suggested for commit messages:

```
PATRONSZCZ-123: Brief description of the nature of submitted changes

Any additional information regarding the commit, 
for example its relation to other tasks (if applicable),
or a brief summary of pending TODOs.
```
It is recommended to briefly explain the nature of changes esp. if they're non-obvious. Eg.:

* ![#c5f015](https://placehold.it/15/c5f015/000000?text=+)
```
PATRONSZCZ-718: Added connection to MS Graph API

OldService replaced with NewService as service didn't work correctly for some reason
```
Avoid commit messages that are: 
* ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) overly general:
    ```
    PATRONSZCZ19-123: Fix to PR
    PATRONSZCZ19-123: Work/tmp and others
    ```
* ![#f03c15](https://placehold.it/15/f03c15/000000?text=+) focused on "what" instead of "why" (the former question is answered by code itself, commit message should answers the latter):
    ```
    PATRONSZCZ19-123: Added delay(1000L) to BankTransferInteractor.fetchFee()
    ```
If there is no corresponding task on Jira (acceptable for quick fixes and small code submissions), the same rules are recommended, apart from:
* Ommit the _"PATRONSZCZ19-123:"_ prefix from the commit message (obviously)
* Use the briefest possible description of the task as the branch name, eg. `bugfix/startup_crash`.