Feature: NavigationTests

# Some of the tests will fail since the link values are not using the correct schema (http and not https)

    Scenario Outline: SiteMap
        Given Home page is open
        When go to "<link>" link
        Then URL "<url>" is opened

        Examples:
          | link                  | url                                        |
          | Vue.js                | http://vuejs.org/                          |
          | Documentation         | http://vuejs.org/guide/                    |
          | API Reference         | http://vuejs.org/api/                      |
          | Examples              | http://vuejs.org/examples/                 |
          | Vue.js on GitHub      | https://github.com/vuejs/vue               |
          | Twitter               | http://twitter.com/vuejs                   |
          | Gitter Channel        | https://gitter.im/yyx990803/vue            |
          | Discussions on GitHub | https://github.com/vuejs/Discussion/issues |
          | let us know           | https://github.com/tastejs/todomvc/issues  |

    Scenario: FooterTabNavigation
        Given Home page is open
        When add "test123" item
        And go to footer tab "All"
        Then verify footer tab "All" is active
        When go to footer tab "Active"
        Then verify footer tab "Active" is active
        When go to footer tab "Completed"
        Then verify footer tab "Completed" is active