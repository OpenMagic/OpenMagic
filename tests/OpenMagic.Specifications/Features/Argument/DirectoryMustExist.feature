Feature: DirectoryMustExist
	I want directory argument to validate directory exists before proceeding with the method

Scenario: Directory exists
	Given directory exists
	When I call Argument.DirectoryExists(<param>, <paramName>)
	Then passed <param> should be returned

Scenario: Directory does not exists
	Given directory does not exists
	When I call Argument.DirectoryExists(<param>, <paramName>)
	Then ArgumentException should be thrown
	And the exception message should be:
		"""
		Directory must exist. (Parameter 'dummy')
		"""