Feature: Random Number Generation
	In order to play the game
	As a game player
	I want to be able to have random numbers generated that do not equal previous numbers in the game

Scenario: I can generate a random number between 1 and 100
	Given I have a random number generator
	When I request a random number
	Then I get a random number returned between 1 and 100

Scenario: I can consistantly generate a random number between 1 and 100 over many iterations
	Given I have a random number generator
	When I request a random number '1000' times
	Then I get only random numbers between 1 and 100

Scenario: When I generate a number that has already been generated, then the number generator will continue until a unique number has been generated
	Given I have a random number generator set to return '10' first, then '10' second, then '20' on the third time
	And I have a Game object
	When I request a random number from the random number checker from the Game object
	When I request a random number from the random number checker from the Game object
	Then I retry until I get a number that I have not yet had and the random number generator random number request will have been invoked '3' times
	

