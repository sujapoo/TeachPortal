TeachPortal is a .NET 8-based Web API application designed to efficiently manage and process teacher and student data. This documentation provides detailed instructions on setting up, running, and enhancing the application, including an overview of the key design patterns implemented and the associated API specifications.
## How to Run the Application
### 1. Clone the Repository
```bash
git clone https://github.com/sujapoo/TeachPortal.git
cd TeachPortal

2. Install Dependencies
Install the necessary dependencies using the NuGet package manager.
3. Set Up Database
•	Ensure your database (SQL Server) is running and configured.
•	Update the connection string in appsettings.json.
•	Use the provided database scripts (DBScript.sql and create_dev_login.sql) to set up the database.
4. Start the Application
•	Open the project in Visual Studio.
•	Build and run the application.
5. Access the API Documentation
•	Open the Swagger UI at http://localhost:<your_port_number>/swagger or the endpoint defined in your application.
•	Explore and test the API endpoints through the Swagger interface.

Design Patterns Used
1.	Repository Pattern
o	AppDbContext abstracts database access for better maintainability and testing.
o	Facilitates clean and modular data entity management.
2.	Service Layer Pattern
o	Business logic is implemented in service layers (AuthService, TeacherService, StudentService), keeping the controller layer focused on HTTP request handling.
3.	JWT Authentication
o	Secures the API with JSON Web Tokens. Tokens are generated on login and used for authenticating subsequent requests.
4.	Factory Method
o	Used for validating input data in the SignupForm on the frontend.
5.	DTO (Data Transfer Object)
o	Ensures data consistency between layers (e.g., LoginRequest, Teacher, Student).
Assumptions and Enhancements
Assumptions
•	On-premises setup with optional cloud integrations.
•	SQL Server is installed and configured.
•	JWT tokens are securely stored in the client-side storage (e.g., localStorage).
Planned Enhancements
1.	Database Connection Security
o	Implement SSL/TLS encryption for secure communication.
2.	Role-Based Authorization
o	Introduce Admin, Teacher, and Student roles for enhanced access control.
3.	Improved Validation
o	Add password strength validation and advanced email uniqueness checks.
4.	Advanced Error Handling
o	Implement detailed error messages and validations in both frontend and backend.
5.	Rate Limiting
o	Add rate limiting and security measures to prevent brute-force attacks.
6.	UI Improvements
o	Enhance frontend UI with better styling and responsiveness.

Unit Testing: This enhancement plan integrates unit testing for each security and functionality improvement to ensure that all enhancements are validated and verified systematically.
Unit Testing for SSL/TLS Database Connection:
Unit Testing for JWT Authentication:
Unit Testing for Password Strength
Unit Testing for UI Improvements
Unit Testing for Error Handling

7.	API Endpoints
8.	1. POST /api/auth/signup
9.	Registers a new teacher. Request Body:
json
{
  "userName": "string",
  "email": "string",
  "passwordHash": "string",
  "firstName": "string",
  "lastName": "string",
  "students": []
}
Response: 200 OK - Teacher successfully registered.
10.	2. POST /api/auth/login
Authenticates a teacher and returns a JWT token. Request Body:
json
{
  "username": "string",
  "password": "string"
}
Response: 200 OK - Returns a JWT token.
11.	3. POST /api/students
Adds a new student. Request Body:
json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "teacherId": 1,
  "teacher": null
}
Response: 200 OK - Student successfully added.
12.	4. GET /api/students
Retrieves a list of all students. Response: 200 OK - Returns an array of students.
13.	5. GET /api/teacher
14.	Retrieves a list of all teachers with an overview (name and student count). Response: 200 OK - Returns an array of teacher overviews.

