# Blazor Book Hub

Blazor Book Hub is a book management system built with ASP.NET Core and Blazor. The project features user registration, book and category management, and secured API endpoints using JWT authentication.

## Technologies

- ASP.NET Core 7
- Blazor WebAssembly
- Entity Framework Core (Code First)
- ASP.NET Core Identity
- JWT Authentication
- SQL Server LocalDB
- AutoMapper

## Getting Started

1. Clone the repository:  
   ```bash
   git clone https://github.com/DilmurodDeveloper/Blazor-Book-Hub.git
   ```
2. Open the solution and restore the packages:
    ```bash
    dotnet restore
    ```
3. Apply database migrations:
    ```bash
    dotnet ef database update
    ```
4. Run the application:
    ```bash
    dotnet run
    ```

## Usage

An admin user is created automatically with the following credentials:

Email: admin@example.com

Password: Admin123!
- Authentication is handled via JWT tokens.
- Manage books and categories through the API.
- User list and management is restricted to admin role.

## Screenshots

### 🔐 Login & Register

<p align="center">
  <img src="https://github.com/user-attachments/assets/30467fb9-0197-4208-aa2e-f8f9ac1e0089" width="45%" />
  <img src="https://github.com/user-attachments/assets/ff7ae997-e340-4c14-b2d2-9baa95edfe43" width="45%" />
</p>

### 🛠 Admin Panel

<p align="center">
  <img src="https://github.com/user-attachments/assets/be8eba94-4c58-4944-9fc9-6e798569d34e" width="45%" />
  <img src="https://github.com/user-attachments/assets/8d47d940-bd57-4172-bb45-e1e2b249b4b1" width="45%" />
</p>
<p align="center">
  <img src="https://github.com/user-attachments/assets/e209cc3e-2a31-40d5-8669-c4334db0c3df" width="45%" />
  <img src="https://github.com/user-attachments/assets/5064dd9d-b081-414c-837e-b21fb8ef74b7" width="45%" />
</p>

### 👤 User Pages

<p align="center">
  <img src="https://github.com/user-attachments/assets/b6b77068-493b-479b-be2c-f1668dade5bc" width="45%" />
  <img src="https://github.com/user-attachments/assets/540a4664-0b23-4e96-920c-0b578ceb0a6e" width="45%" />
</p>


## Author

Dilmurod Developer - [GitHub Profile](https://github.com/DilmurodDeveloper)
