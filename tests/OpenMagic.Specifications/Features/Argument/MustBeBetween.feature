Feature: MustBeBetween

Scenario Outline: value is in range
	Given value is <value>
	And minimumValue is <minimumValue>
	And maximumValue is <maximumValue>
	When I call value.MustBetween(minimumValue, maximumValue)
	Then <value> should be returned

	Examples:
		| value | minimumValue | maximumValue |
		| 1     | 1            | 5            |
		| 2     | 1            | 5            |
		| 3     | 1            | 5            |
		| 4     | 1            | 5            |
		| 5     | 1            | 5            |

Scenario Outline: value is out of range
	Given value is <value>
	And minimumValue is <minimumValue>
	And maximumValue is <maximumValue>
	When I call value.MustBetween(minimumValue, maximumValue)
	Then ArgumentOutOfRangeException should be thrown
	And the exception message should be:
		"""
		Value must be between <minimumValue> and <maximumValue>.
		Parameter name: value
		"""

	Examples:
		| value | minimumValue | maximumValue |
		| -1    | 1            | 5            |
		| 0     | 1            | 5            |
		| 6     | 1            | 5            |
		| 7     | 1            | 5            |
