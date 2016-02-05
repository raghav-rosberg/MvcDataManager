Introduction
----------------------------------
"MVC Data Manager" a one stop solution to any MVC application to start with. Easy to setup a data layer with very less effort. It provides "Generic repository" with "Unit of Work" design patterns, enabling most of the common methods to all the database entities. It also provides fully featured "User Management" portal developed using "AspNetIdentity". Below are the main features of "MVC Data Manager"

- Data Store
  - Generic Repository
  - Unit of Work
  - Common C# helpers

- Option to include IOC (dependency injection) in web layer

- User Management
  - Login Control
  - Forgot Password Control
  - Reset Password Control
  - Register User
  - User Management for admin (upcoming feature)
    - User CRUD operations
    - User role CRUD operations

How to use
------------------------------
1. Create a data layer project (class library) e.g. "MvcUserManagement.data" and download "MVC Data Manager" from Nuget Package Manager using the command "Install-Package MvcDataManager" (website link: https://www.nuget.org/packages/MvcDataManager) 
   a. Create DbContext in the data layer project as shown in "Content\MvcDataManagerSampleCode\DBContext".
2. Create any repository extensions or repositories using "Generic repository" from "MVC Data Manager" if required.
3. Create an MVC application and add a reference to data layer and "MvcDataManager.Model" from Nuget Package Manager using the command "Install-Package MvcDataManager.Model" (website link: https://www.nuget.org/packages/MvcDataManager.Model). 
4. Get in to the folder "Content\MvcDataManagerSampleCode" and set up MVC application as shown in the sample code.
5. Refer Bootstap and Jquery in the MVC application (recommended to link Bootstrap and Jquery in "Layout" page).

Download sample project from "https://github.com/raghav-rosberg/MvcUserManagement" which provides basic architecture on how to use "MVC Data Manager"
