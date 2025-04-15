# 💡IdeaInnova

IdeaInnova is an innovative enterprise platform designed to foster internal creativity and engagement. The system enables employees to submit ideas for workplace improvements, vote on submissions, and track their contributions through a gamified leaderboard and reward system.

## Features

- **Idea Submission:** Employees can easily submit new ideas using a user-friendly form.
- **Voting System:** Users can vote on ideas, helping to highlight the most promising innovations.
- **Leaderboard & Rewards:** Gamification elements (points, rankings) motivate active participation.
- **User Management:** Account registration, login, and role-based access control (Employee, Manager, Admin).
- **RESTful API:** A fully functional Web API exposes endpoints for idea submission, voting, and user management.
- **Swagger Documentation:** Built-in Swagger UI (available in development mode) documents and allows testing of the API.
- **Authentication:** Basic session-based authentication has been implemented (with plans to refine in future iterations).

## Tech Stack

- **Backend:** ASP.NET Core (C#)
- **Frontend:** Razor Views with HTML, CSS, and JavaScript
- **Database:** SQL Server LocalDB (configurable for production)
- **Authentication:** Session-based authentication
## Getting Started

### Prerequisites

- [.NET SDK 9.0 or later](https://dotnet.microsoft.com/download)
- Visual Studio 2022 (or any IDE that supports C#)
- SQL Server LocalDB (usually bundled with Visual Studio)

### Setup Instructions

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/RudraPatelhere/IdeaInnova.git
   cd IdeaInnova
   ```
2. **Restore Dependecies**
   ```
   dotnet restore
   ```
3. Update the Database
   ```
   dotnet ef database upadte
   ```
4. Build the Application
   ```
   dotnet build
   ```
5. Run the Application
   ```
   dotnet run
   ```




   The Frontend:
   

   1. SignUp/SignIn Page
      
      ![image](https://github.com/user-attachments/assets/76ad489f-2bb8-4f66-ad6e-40aecb120da9)

   2. Home Page
      
   ![image](https://github.com/user-attachments/assets/4a94c4df-e724-47d5-a9c9-1df7f2aadbbe)

   3. Submit Idea Page
      
   ![image](https://github.com/user-attachments/assets/ba7d52c9-2f31-4ea2-ab29-c8286aba4f49)

   4. Voting Page
      
   ![image](https://github.com/user-attachments/assets/2f124d58-51b9-4a5d-bacd-4a17c5a7b637)

   5. Leaderboard Page
      
   ![image](https://github.com/user-attachments/assets/9a737b0b-f4a5-469b-a9a9-a05fcf41b62b)

   6. Profile Page
     
     ![image](https://github.com/user-attachments/assets/89122e51-735a-4b76-8b3c-c3a7abe1e802)

    

 

      

