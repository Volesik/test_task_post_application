## 🧠 Task

Create a .NET 6+ Console Application that:

1. Fetches data from a public REST API:
   - Users: [https://jsonplaceholder.typicode.com/users](https://jsonplaceholder.typicode.com/users)
   - Posts: [https://jsonplaceholder.typicode.com/posts](https://jsonplaceholder.typicode.com/posts)

2. Processes the data:
   - Links posts to users.
   - Filters users by cities starting with a specified letter (e.g., "S").
   - Calculates the number of posts for each matched user.

3. Displays results in the console in a formatted way

## 📄 Project Description
  This project is a .NET 6+ Console Application designed to process and display user-post data from a public API. The architecture supports clean separation of concerns and scalability. Key features include:
  -  **Data downloading using background workers** powered by Hangfire.
  -  **PostgreSQL** is used as the primary database.
  -  **Docker** is used to containerize and run PostgreSQL locally.
  -  Implements a **layered architecture** (e.g., Data, Business Logic, Application).
  -  Filtering is modular and extendable through the use of the **Specification pattern**, allowing easy addition of new filtering rules.

## ▶️ How to Run
  - **Start PostgreSQL using Docker** run.ps1 inside Docker directory
  - Run the PostApp.Workers project (Users are downloaded every 1 minute, Posts are downloaded every 2 minutes, You can manually trigger jobs via the built-in Hangfire dashboard)
  - Run the Console Application

## 💡 What Can Be Improved
  - Add unit tests to cover business logic and service behavior.
  - Move credentials and configuration values to environment variables.
  - Use a state machine to manage console input/output flow more cleanly.
  - Create a dedicated service for fetching data from APIs in real-time, with proper separation of concerns.
  - Add structured logging using a library like Serilog or built-in logging extensions.
