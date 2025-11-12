
Municipal Services App

A civic-grade ASP.NET Core MVC application designed to empower residents by streamlining issue reporting, event discovery, and service request tracking. Built with professional styling, advanced data structures, and clean architecture for academic submission.

Purpose of the Application

The Municipal Services App enables users to:
- Report infrastructure issues (e.g., potholes, broken lights, burst pipes)
- View and search local community events
- Track the status and priority of submitted service requests

This promotes transparency, civic engagement, and efficient municipal communication.

Application Architecture

- **MVC Pattern**: Separation of concerns via Models, Views, and Controllers
- **In-Memory Data Service**: Uses advanced data structures (Queue, Stack, Dictionary, PriorityQueue)
- **Razor Views**: Dynamic, responsive UI with Bootstrap and custom CSS
- **Routing**: Clean URL structure with controller/action mapping
- **Styling**: Fixed top taskbar, cityscape background, and semantic layout

---

Technologies Used

| Technology        | Purpose                          |
|------------------|----------------------------------|
| ASP.NET Core MVC | Web framework                    |
| C# (.NET 6+)      | Backend logic                    |
| Razor Pages      | Dynamic HTML rendering           |
| Bootstrap 5      | Responsive UI styling            |
| Custom CSS       | Branding and layout              |
| Visual Studio 2022 | Development environment        |


How to Run the Application

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/municipal-services-app.git
```

### 2. Open in Visual Studio 2022

- Open the `.sln` file
- Ensure target framework is `.NET 6.0` or higher

### 3. Restore NuGet Packages

- Right-click the solution → `Restore NuGet Packages`

### 4. Register the Data Service

In `Program.cs`, ensure this line is present:

```csharp
builder.Services.AddSingleton<IDataService, InMemoryDataService>();
```

### 5. Run the App

- Press `F5` or click `Start`
- Navigate to:  
  ```
  https://localhost:7060
  ```

---

Features Implemented

- ✅ Issue Reporting with timestamp and ID
- ✅ Event search by keyword, category, and date
- ✅ Service Request creation, priority queueing, and dependency tracking
- ✅ Top priority request display
- ✅ Search by Request ID
- ✅ Clean UI with fixed top navigation

---

Design Considerations

- Fixed top taskbar for consistent navigation
- Cityscape background for civic branding
- Responsive layout with Bootstrap and custom CSS
- Semantic HTML for accessibility
- Modular Razor views for maintainability

Demo Video

https://youtu.be/Y7eElKOlf3A
---

AI Tools Used

| Tool               | Usage Description                                      |
|-------------------|--------------------------------------------------------|
| Microsoft Copilot | Code generation, UI layout, documentation              |
| GitHub Copilot    | Autocomplete and syntax suggestions                    |
| Visual Studio IntelliCode | Smart recommendations during development     |

---

GitHub Usage Summary

- Branching used for feature isolation
- Commit messages are descriptive and rubric-aligned
- README and documentation maintained in root
- Screenshots and video embedded for clarity
