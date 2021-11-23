Feature: APITests
	
Scenario: Test1-Verify API response for GET All Users
Given Make a call to API to get all users
When API Response is Received
Then Verify API Response has all the expected users data

Scenario: Test2-Verify API response for sepcific User
Given Make a call to API to get specific user with id 2 
When API Response is Received
Then Verify API Response has following users data
| id | email                  | first_name | last_name | avatar                                  | url                                | text                                                                     |
| 2  | janet.weaver@reqres.in | Janet      | Weaver    | https://reqres.in/img/faces/2-image.jpg | https://reqres.in/#support-heading | To keep ReqRes free, contributions towards server costs are appreciated! |

Scenario: Test3-Verify API response for Non-Existing User
Given Make a call to API to get specific user with id 23
Then Verify API Response throws 404 Not Found

