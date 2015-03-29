Feature: FileMustExist
	I want file argument to validate file exists before proceeding with the method

Scenario: File exists
	Given file exists
	When I call Argument.FileExists(<param>, <paramName>)
	Then <param> should be returned

Scenario: File does not exists
	Given file does not exists
	When I call Argument.FileExists(<param>, <paramName>)
	Then ArgumentExection should be thrown
	And exception message should be:
		"""
		File must exist.
		Parameter name: dummy
		"""
