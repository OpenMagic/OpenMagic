Feature: InsertStringBeforeEachUpperCaseCharacter
	I want to insert a string before each upper case character in a string


Scenario Outline: InsertStringBeforeEachUpperCaseCharacter(<value>, <insert>)
	When I call InsertStringBeforeEachUpperCaseCharacter(<value>, <insert>)
	Then the result should be <expectedResult>

	Examples: 
	| value                                       | insert | expectedResult                                     |
	| HomePage                                    | -      | Home-Page                                          |
	| HomePage                                    | _      | Home_Page                                          |
	| ALongerExampleWithServalUpperCaseCharacters | _      | A_Longer_Example_With_Serval_Upper_Case_Characters |
