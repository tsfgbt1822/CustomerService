# Customer Management System

This is a console-based Customer Management System written in C#. It allows users to add and remove customers, view the current customer list, and ensures data integrity through input validation. The program demonstrates core concepts such as object-oriented programming, events and delegates, and regular expressions.

## Features

- Add new customers with:
  - First name
  - Last name
  - Email
  - Phone number (used as a unique customer ID)
- Remove existing customers by phone number (ID)
- Input validation:
  - Maximum name length of 25 characters
  - Valid email format
  - Phone number must be exactly 10 digits
  - No duplicate customer IDs allowed
- Automatically updates and displays the customer list when changes occur

## Concepts Demonstrated

- Object-oriented programming (OOP)
- Custom classes and constructors
- Events and delegates
- Regular expressions for input validation
- Iteration using `foreach`
- User interaction via the console
