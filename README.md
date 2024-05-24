# Address Book Application

## Setting up PostgreSQL Connection

1. Open the `appsettings.json` file located in the `ExpertFreezerWebAPI` directory.

2. Add the PostgreSQL connection string to your Postres DB, as the first entry under the `"ConnectionStrings"` section. Example:

   ```json
   {
    "ConnectionStrings": {
    "WebApiDatabase": "Host=*; Database=*; Username=*; Password=*" 
    },
     // Other configurations...
   } 

# Running the Application

## Frontend (Angular)

3. Navigate to the `front` directory in terminal. run these commands to start Angular Live Development Server
(`ng build`, `ng serve`). You can launch the application from provided link in terminal.

## Backend (ASP.NET Core Web API)

4. Navigate to the `ExpertFreezerWebAPI` directory in second terminal.

This may help if you need to make the table manually for your DB:

5.       CREATE TABLE "expertFreezerProfiles" (
            "Id" BIGINT PRIMARY KEY,
            "UserName" TEXT NOT NULL,
            "Password" TEXT NOT NULL,
            "ConfirmPassword" TEXT NOT NULL,
            "CompanyName" TEXT,
            "ProfilePic" TEXT,
            "ExtraPics" TEXT[],
            "ExtraPicsDesc" TEXT[],
            "CompanyDescription" TEXT,
            "Services" TEXT,
            "Address" TEXT,
            "Pricing" TEXT,
            "IsComplete" BOOLEAN NOT NULL,
            "Secret" TEXT
         );

6. Run command in terminal using entity framework: `dotnet ef migrations add <MigrationName>`, `dotnet ef database update`.

7. Run the following commands to build and run the ASP.NET Core Web API:(`dotnet build`, `dotnet run`)
