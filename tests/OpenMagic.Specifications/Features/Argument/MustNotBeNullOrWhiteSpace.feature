Feature: MustNotBeNullOrWhiteSpace.
  As a developer
  I want to ensure that a string argument is not null or whitespace
  So that I can validate user input effectively

Scenario: Valid string argument
	Given a string argument "Hello, World!"
	When I check if the string is not null or whitespace
	Then the string argument should be returned

Scenario: Null string argument
	Given a null string argument
	When I check if the string is not null or whitespace
	Then ArgumentNullOrWhiteSpaceException should be thrown

Scenario: Empty string argument
	Given an empty string argument
	When I check if the string is not null or whitespace
	Then ArgumentNullOrWhiteSpaceException should be thrown

Scenario: Whitespace string argument
	Given a whitespace string argument
	When I check if the string is not null or whitespace
	Then ArgumentNullOrWhiteSpaceException should be thrown
