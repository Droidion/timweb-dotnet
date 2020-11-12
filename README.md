# Timgroup web infrastructe

There is a site https://timseminar.ru. It'w ancient, written in PHP, uses MySQL, has no admin interface.

This project tries to bring it to the modern era using .NET 5.0 and PostgreSQL.

It also tries to split the existing monolith into several dockerized parts:

- API
- Server-rendered main site.
- Server-rendered admin panel.
- Switch `Google Analytics` to self hosted `umami`.
- Leave the same design, but modernize it.