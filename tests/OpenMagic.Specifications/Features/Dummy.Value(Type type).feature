Feature: Dummy.Value(Type type)

Scenario Outline: happy path
	Given type is <type>
	When Dummy.Value(type) is called
	Then the type of the result should be <type>
	
	Examples:
		| type      | comments                            |
		| Boolean   |                                     |
		| DateTime  |                                     |
		| String    |                                     |
		| Byte      |                                     |
		| Char      |                                     |
		| Decimal   |                                     |
		| Double    |                                     |
		| Single    |                                     |
		| Int32     |                                     |
		| Int64     |                                     |
		| SByte     |                                     |
		| Int16     |                                     |
		| UInt32    |                                     |
		| UInt64    |                                     |
		| UInt16    |                                     |
		| Exception | any class will do for test purposes |

Scenario: type is List<T>
	Given type is List<T>
	When Dummy.Value(type) is called
	Then the type of the result should be List<T>
	And the result should be a list of random number of items

Scenario: type is array
	Given todo
