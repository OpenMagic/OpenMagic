Feature: IsReposonding
	I want to test if a URI is responding

Scenario: URI is responding
	Given URI is responding
	When I call IsResponding(<uri>)
	Then True should be returned

Scenario: URI is not responding
	Given URI is not responding
	When I call IsResponding(<uri>)
	Then False should be returned
