# Customer management

This project is a <u>web API</u> for managing customer data. It is built using .NET Framework, C#, and follows the <u>Repository pattern</u> with a <u>Unit of Work</u> implementation for data access. **JWT authentication and authorization** have also been implemented to ensure secure access to the API.

## Getting Started

To get started with this project, you will need to have Visual Studio installed on your computer. Once you have cloned the repository, you can open the solution file in Visual Studio and run the project.

## Prerequisites

Visual Studio/ Visual Studio code
.NET Framework

## Installing

Clone the repository to your local machine.

Open the solution file in Visual Studio.

Update connection settings in `appsettings.json`.

Build and run the project or `dotnet restore`.

## Usage

This API allows you to manage student data. You can create, read, update, and delete student records using the endpoints provided.

## Endpoints

#### Customer

- 🔎GET api/Customer - Returns a list of all customers.
- 🔍GET api/Customer/{id} - Returns a single customer record by ID.
- ➕POST api/Customer - Creates a new customer record.
- ✏️PUT api/Customer/{id} - Updates an existing customer record by ID.
- 🗑️DELETE api/Customer/{id} - Deletes a customer record by ID.

#### User

- ➕POST api/User/Register - Registers a new user.
- 👤POST api/User/Login - Logs in a user and returns a JWT token.
- 🔍GET api/User - Returns information about the currently logged in user.

## Authentication

This API uses JWT authentication to secure access to the endpoints. You will need to provide a valid token in the Authorization header to access the endpoints.

## Authorization

Authorization has also been implemented for certain endpoints. Only users with the appropriate role can access these endpoints. Please refer to the documentation for more information on the required roles.

## Contributing

If you find this project useful, please give it a stars⭐⭐⭐! Contributions are also welcome. Please fork the repository and submit a pull request with your changes.

## Authors

Võ Thương Trường Nhơn

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT). See the LICENSE.md file for details.
