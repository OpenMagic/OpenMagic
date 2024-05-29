Feature: MustNotBeEmpty

Scenario: Array has elements
	Given param is an array with elements
	When I call Argument.MustNotBeEmpty(int[] param, paramName)
	Then the param should be returned

Scenario: Array has zero elements
	Given param is an array with zero elements
	When I call Argument.MustNotBeEmpty(int[] param, paramName)
	Then ArgumentEmptyException should be thrown
	And the exception message should be:
		"""
		Value cannot be empty. (Parameter 'dummy')
		"""

Scenario: Guid has a value
	Given param is a guid with a value
	When I call Argument.MustNotBeEmpty(Guid param, paramName)
	Then the param should be returned

Scenario: Guid has an empty value
	Given param is a guid with an empty value
	When I call Argument.MustNotBeEmpty(Guid param, paramName)
	Then ArgumentEmptyException should be thrown
	And the exception message should be:
		"""
		Value cannot be empty. (Parameter 'dummy')
		"""
