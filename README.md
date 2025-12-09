# Municipal Services App – PROG7312 POE

1.Overview
This ASP.NET Core MVC application empowers residents to:
- Report municipal issues (e.g. sanitation, roads, utilities)
- View local events and announcements
- Track service request statuses using advanced data structures

### YouTube Link 
https://youtu.be/4im6rbQWljs?si=CK9kAjAAZ1kGIEb3


It demonstrates professional backend architecture and integrates:
- **AVL Tree** for sorted request storage
- **MinHeap** for urgent request prioritization
- **Graph Traversal (BFS/DFS)** for request dependencies
- **Minimum Spanning Tree (MST)** for optimized service zones

2.Setup Instructions

### Prerequisites
- Visual Studio 2022+
- .NET 6 SDK
- SQL Server (if using EF Core)
- Git (optional)

Steps
1. Clone the repo:
   ```bash
   git clone https://github.com/VCCT-PROG7312-2025-G1/MunicipalService_P3-ST10030780-POE_Part3.git
   ```
2. Open the solution in Visual Studio.
3. Build the project (`Ctrl+Shift+B`).
4. Run the app (`F5`).
5. Navigate to:
   - `/Home/Index` → Welcome page
   - `/Issues/Report` → Submit new issues
   - `/Events/Index` → Browse local events
   - `/Status/Index` → Track service requests
    
3. Features

Issue Reporting
- Submit issue details with location, category, and description
- Attach files (optional)
- Feedback messages and progress indicators

Local Events
- Browse upcoming events
- Search by category or date
- Recommendation engine based on user interest

Service Request Status
- **AVL Tree**: Sorted request storage and fast lookup
- **MinHeap**: Extract most urgent request
- **Graph**: BFS/DFS traversal of request dependencies
- **MST**: Optimize service zone connections

---

4. Project Structure

```
MunicipalService_P3/
├── Controllers/
│   └── StatusController.cs
├── Models/
│   └── ServiceRequest.cs
├── DataStructures/
│   ├── AvlTree.cs
│   ├── MinHeap.cs
│   └── Graph.cs
├── Views/
│   └── Status/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       ├── Urgent.cshtml
│       ├── MST.cshtml
│       ├── DFS.cshtml
│       └── Edges.cshtml
├── Data/
│   └── AppDbContext.cs
├── Services/
│   ├── IDataService.cs
│   └── InMemoryDataService.cs

5. AI Tool Usage

AI tools were used to:
- Generate boilerplate code for AVL, MinHeap, Graph, and MST
- Draft documentation and README structure
- Suggest improvements for rubric alignment

All AI-generated content was reviewed and adapted to meet academic integrity requirements.


6. Rubric Checklist

| Criteria                     | Status |
|-----------------------------|--------|
| README File                 |  Clear, complete, well-organized |
| Software Functionality      |  Fully working |
| Application Requirements    |  All met |
| User Experience             |  Intuitive and responsive |
| Data Structures             |  AVL, Heap, Graph, MST integrated |
| Reports                     | Implementation + Completion reports submitted |
| Technology Recommendations | Included and justified |

---

7. Author

- **Student:** ST10030780 – Sidney  
- **Module:** PROG7312 – Application Development  
- **Institution:** The Independent Institute of Education (IIE), 2025
