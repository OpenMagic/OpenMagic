Feature: TrimStart
	I want remove strings from the start of a string

Scenario Outline: <value> starts with <trimString>
	When I call TrimStart(<value>, <trimString>)
	Then the result should be <expectedResult>

	Examples: 
	| value    | trimString | expectedResult |
	| HomePage | Home       | Page           |

Scenario Outline: <value> starts with <trimString> multiple times
	When I call TrimStart(<value>, <trimString>)
	Then the result should be <expectedResult>

	Examples: 
	| value        | trimString | expectedResult |
	| HomeHomePage | Home       | Page           |

Scenario Outline: <value> does not start with <trimString>
	When I call TrimStart(<value>, <trimString>)
	Then the result should be <expectedResult>

	Examples: 
	| value | trimString | expectedResult |
	| Home  | Page       | Home           |
