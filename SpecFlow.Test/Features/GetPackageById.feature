Feature: GetPackageById
  As a user, I want to retrieve a package by its ID, so that I can get the details of the package.

  Scenario: Retrieve an existing package by ID
    Given the package with ID "existing-package-id" exists
    When I send a GET request to "/api/packages/existing-package-id"
    Then the response status code should be 200
    And the response body should contain the package details

  Scenario: Retrieve a non-existing package by ID
    Given the package with ID "non-existing-package-id" does not exist
    When I send a GET request to "/api/packages/non-existing-package-id"
    Then the response status code should be 400
    And the response body should contain "Package with ID no existe."
