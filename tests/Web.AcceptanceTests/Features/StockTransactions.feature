@StockTransactions
Feature: Stock Transactions
    Create stock entries via the web UI

    Scenario: Add new stock transaction
        Given the user is on the stock transactions page
        When the user creates a stock entry
        Then the stock entry is visible
