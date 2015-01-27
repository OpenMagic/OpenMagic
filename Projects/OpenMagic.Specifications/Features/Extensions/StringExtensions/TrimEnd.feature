Feature: TrimEnd
	I want remove strings from the end of a string

Scenario Outline: <value> ends with <trimString>
	When I call TrimEnd(<value>, <trimString>)
	Then the result should be <expectedResult>

	Examples: 
	| value    | trimString | expectedResult |
	| HomePage | Page       | Home           |

Scenario Outline: <value> ends with <trimString> multiple times
	When I call TrimEnd(<value>, <trimString>)
	Then the result should be <expectedResult>

	Examples: 
	| value        | trimString | expectedResult |
	| HomePagePage | Page       | Home           |

Scenario Outline: <value> does not end with <trimString>
	When I call TrimEnd(<value>, <trimString>)
	Then the result should be <expectedResult>

	Examples: 
	| value | trimString | expectedResult |
	| Home  | Page       | Home           |
