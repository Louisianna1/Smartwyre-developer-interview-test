# Smartwyre Developer Test - Overview of solution updates

The following provides a brief overview of the refactoring and enhancements made to the original code to address the following:

1. Adherence to SOLID principles
•	S - Single-responsiblity Principle - The RebateService.cs Calculate() method should be responsible for initiating the high level processing/orchestration steps on receipt of a request. In particular, this method should not have the validation and calculation logic baked in, but instead rely on separate Classes to perform these actions.
•	Open-Closed Principle - Addition of new incentive types should not require the core logic of the RebateService.cs Class to be modified. To address this, a separate Calculator Class has been introduced for each of the existing rebate incentive types that each extend a common RebateCalculator abstract Class (please see Smartwyre.DeveloperTest/Logic
/Calculators/ folder).
•	Liskov Substitution Principle - The concrete Calculator Classes all extend a common abstract base Class, RebateCalculator. The abstract Class provides common validation logic that can be overridden or supplemented in the sub-classes, as well as an abstract CalculateRebate() method, allowing the RebateService to perform the validation and calculation steps via polymorphism (RebateCalculator Object reference).
•	Interface Segregation Principle - Currently the validation and rebate calculation steps are common irrespective of the incentive type and so this point doesn't require addressing.
•	Dependency Inversion Principle - The RebateService retrieves data, performs validations and calculations via IRebateDataStore and IProductDataStore interfaces and RebateCalculator abstract Class to achieve decoupling. Since various rebate incentive types exist and further will be added in the future, the Factory pattern together with .NET Core dependency injection is used to obtain the corresponding calculator at runtime. 


2. Testability
Two main changes were made as follows to ensure that the code was easier to test;
•	IRebateDataSource and IProductDataSource interfaces were introduced to allow dummy data sources / retrieval logic to be added in the unit tests (please see Smartwyre.DeveloperTest.Tests
/DummyServices/ directory).
•	An Enumerated ErrorCode property has been added to the CalculateRebateResult Class to allow unit tests to assert the reason for a request failing validation.

The test project was updated to reference Xunit.Microsoft.DependencyInjection and related nuget packages to configure the concrete services (dummy as well as real Classes). 


3. Readability
Logical directory and file structure and naming has been used along with comments to aid with this.


4. Extensibility
Addition of a new rebate incentive type can be achieved in a couple of simple steps;
•	Create new Class in Smartwyre.DeveloperTest/Logic
/Calculators/ directory that extends the RebateCalculator abstract Class, providing specific validation and calculation logic.
•	Add the new Class to the Service container used for dependency injection (AddServices() method of TestFixture.cs for the purpose of unit testing).
