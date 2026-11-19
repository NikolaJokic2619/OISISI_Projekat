# Foundations of Information Systems and Software Engineering — Course Project

## Team Members
1. Nikola Jokić
2. Danilo Obradović

## Project Description
A C# course project for managing records at a book fair. The console client uses a shared Core project for models, data access, and file storage.

## Project Features

- List, add, edit, and remove addresses, authors, books, visitors, publishers, and purchases.
- Changes are saved automatically and loaded when the application restarts.

## Task allocation

Nikola Jokić:
- Added & implemented the logic of conversion from and to csv files in all base classes
- Implemented Serialization of all the classes
- Implemented Menu classes for visual representation of the data
- Implemented Utility class
- Implemented Controller classes: BookController.cs, AuthirController.cs & AddressController

Danilo Obradović:
- Added following classes: Visitor.cs, Book.cs, Shooping.cs, Author.cs, Publisher.cs & Author.cs
- Implemented classes for Data Access Objects (DAO)
- Implemented the Storage.cs class
- Implemented Controller classes: VisitorController.cs, ShoppingController.cs & PublisherController

## Run the Console

Install the **.NET 10 SDK**.

### Linux (VS Code)

Open the repository folder in VS Code and run this from its terminal:

```bash
dotnet run --project BookFair/ConsoleClient
```

### Windows (Visual Studio)

Open BookFair\BookFair.slnx then in Solution Explorer, right-click ConsoleClient, choose Set as Startup Project, and press Ctrl+F5.


## Use the Console

Choose an option from the menu and follow the prompts.
