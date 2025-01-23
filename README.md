# .NET Core 8 User Registration & Login API with Email Queue

This repository contains a simple API built with .NET Core 8 for user registration and login functionality, along with email integration using SMTP for email sending. The API encrypts user details before saving them into a SQL Server database in JSON format.

## Features:
- **User Registration**: Accepts user details (name, email, password, phone), encrypts the data, and saves it in the SQL Server database in JSON format.
- **Email Sending**: After successful registration, an email is sent via SMTP (used for testing purposes in development).
- **User Login**: Allows users to log in with the same credentials used during registration, validating the encrypted data.

## Tech Stack:
- **.NET Core 8** for API development
- **SQL Server** for database
- **SMTP** for sending emails
- **Entity Framework Core** for database interactions
- **ASP.NET Core** for API controller and middleware
- **Encryption**: AES encryption for user data before storing it in the database

## Installation & Setup:
1. Clone the repository:
   ```bash
   git clone <repository-url>
