Feature: MustNotBeEmpty
	I want array argument to validate it has elements before proceeding with the method

Scenario: Array has elements
	Given param is an array with elements
	When I call Argument.MustNotBeEmpty(<param>, <paramName>)
	Then <param> should be returned

Scenario: Array has zero elements
	Given param is an array with zero elements
	When I call Argument.MustNotBeEmpty(<param>, <paramName>)
	Then ArgumentException should be thrown
	And the exception message should be:
		"""
		Value cannot be empty.
		Parameter name: dummy
		"""
