Feature: TimeSpanExtensions.WarnWhenGreaterThan

Scenario: Elapsed time is less than maximum
	Given elapsed time is '100' milliseconds
	And maximum is '200' milliseconds
	And warning message template is 'fake method took {milliseconds}ms.'
	When WarnWhenGreaterThan(stopwatch, message) is called
	Then a warning is not logged

Scenario: Elapsed time is equal to maximum
	Given elapsed time is '100' milliseconds
	And maximum is '200' milliseconds
	And warning message template is 'fake method took {milliseconds}ms.'
	When WarnWhenGreaterThan(stopwatch, message) is called
	Then a warning is not logged

Scenario: Elapsed time is greater than maximum and warning message template uses {milliseconds}
	Given elapsed time is '300' milliseconds
	And maximum is '200' milliseconds
	And warning message template is 'fake method took {milliseconds}ms.'
	When WarnWhenGreaterThan(stopwatch, message) is called
	Then 'fake method took 300ms.' is added to the log

Scenario: Elapsed time is greater than maximum and warning message template uses {0}
	Given elapsed time is '300' milliseconds
	And maximum is '200' milliseconds
	And warning message template is 'fake method took {0}.'
	When WarnWhenGreaterThan(stopwatch, message) is called
	Then 'fake method took 00:00:00.3000000.' is added to the log
