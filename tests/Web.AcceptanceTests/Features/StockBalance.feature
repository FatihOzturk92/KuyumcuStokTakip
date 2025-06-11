@StockBalance
Feature: Stock Balance
    Verify stock balance filtering

    Scenario: Filter stock balance by product name
        Given the user is on the stock balance page
        Then the stock balance table is visible
        When the user filters by product name
        Then only filtered rows are shown
