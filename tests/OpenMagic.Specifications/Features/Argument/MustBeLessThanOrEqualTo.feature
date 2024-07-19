Feature: MustBeLessThanOrEqualTo

Scenario Outline: Date value is in range
	Given value is today
	And maximumValue is <maximumValue>
	When I call Argument.MustBeLessThanOrEqualTo(value, maximumValue)
	Then today should be returned

Examples:
	| maximumValue  |
	| today         |
	| today + 1 day |

Scenario Outline: Date value is out of range
	Given value is today
	And maximumValue is <maximumValue>
	When I call Argument.MustBeLessThanOrEqualTo(value, maximumValue)
	Then ArgumentOutOfRangeException should be thrown
	And the exception message should be:
		"""
		Value must be less than or equal to maximumValue. (Parameter 'value')
		"""
Examples:
	| maximumValue  |
	| today - 1 day |
