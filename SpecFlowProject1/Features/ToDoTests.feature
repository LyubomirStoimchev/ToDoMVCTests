Feature: ToDoTests

    Scenario: AddMultipleItems
        Given Home page is open
        Then add items successfully:
          | items   |
          | test    |
          | test123 |
          | 123     |
          | t       |

    Scenario: AddItem
        Given Home page is open
        When add "test123" item
        Then item "test123" is added

    Scenario: RemoveItem
        Given Home page is open
        When add "test123" item
        And remove "test123" item
        Then item "test123" is removed

    Scenario: AddMultipleItemsAndRemoveSingle
        Given Home page is open
        When add items successfully:
          | items   |
          | test    |
          | test123 |
          | 123     |
          | t       |
        And remove "test" item
        Then item "test" is removed

    Scenario: AddEmptyItem
        Given Home page is open
        When add "" item
        Then item list is empty

    Scenario: CheckUncheckItem
        Given Home page is open
        When add "test123" item
        Then item "test123" is added
        When select item "test123"
        Then item "test123" is checked
        When select item "test123"
        Then item "test123" is unchecked

    Scenario: VerifyCheckedItemTextStyle
        Given Home page is open
        When add "test123" item
        Then item "test123" is added
        When select item "test123"
        Then item "test123" is checked
        And item "test123" text style is "line-through"

    Scenario: VerifyItemCounterUpdatesCorrectly
        Given Home page is open
        When add items successfully:
          | items   |
          | test    |
          | test123 |
        Then item count is "2"
        When add items successfully:
          | items   |
          | test456 |
          | test789 |
        Then item count is "4"

    Scenario: VerifyActiveItems
        Given Home page is open
        When add items successfully:
          | items            |
          | test_active_1    |
          | test_active_2    |
          | test_completed_1 |
          | test_completed_2 |
        And select item "test_completed_1"
        And select item "test_completed_2"
        And go to footer tab "Active"
        Then verify items are:
          | items         |
          | test_active_1 |
          | test_active_2 |
        When go to footer tab "Completed"
        Then verify items are:
          | items            |
          | test_completed_1 |
          | test_completed_2 |