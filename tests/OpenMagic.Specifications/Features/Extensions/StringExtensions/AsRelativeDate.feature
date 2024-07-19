Feature: AsRelativeDate
	As a developer
	I want to be able to convert a string representation of a date to a relative date
	So that I can write dates in a human readable format

Scenario Outline: Call AsRelativeDate with a valid date string
	Given date string is '<date string>'
	When I call AsRelativeDate
	Then '<expected date>' should be returned
Examples:
	| date string                 | expected date             |
	| DateTime.MaxValue           | DateTime.MaxValue         |
	| DateTime.MinValue           | DateTime.MinValue         |
	| DateTime.UtcNow             | DateTime.UtcNow           |
	| 16 July 2024                | 16 July 2024              |
	| today                       | today                     |
	| today + 1 day               | tomorrow                  |
	| today - 1 day               | yesterday                 |
	| today + 2 days              | today + 2 days            |
	| today - 2 days              | today - 2 days            |
	| today + 1 month             | next month                |
	| today - 1 month             | last month                |
	| today + 1 month and 1 day   | next month and 1 day      |
	| today - 1 month and 1 day   | last month and 1 day      |
	| today + 1 month and 2 days  | next month and 2 days     |
	| today - 1 month and 2 days  | last month and 2 days     |
	| today + 2 months and 2 days | plus 2 months and 2 days  |
	| today - 2 months and 2 days | minus 2 months and 2 days |

Scenario Outline: Call AsRelativeDate with an invalid
	Given date string is '<date string>'
	When I call AsRelativeDate
	Then ArgumentException should be thrown
	And the exception message should be:
		"""
		Cannot handle relative date '<date string>'. (Parameter 'value')
		"""
Examples:
	| date string                 |
	| abc                         |
	| today + 1 dayz              |
	| today + 1 monthz            |
	| today + 1 monthz and 1 day  |
	| today - 1 month and 1 dayz  |
	| today + 1 monthz and 2 dayz |

