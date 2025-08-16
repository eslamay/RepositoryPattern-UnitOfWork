# 📌 Repository Pattern & Unit of Work Project

## 📖 Project Description
This project is a small demo implementation of **Repository Pattern** and **Unit of Work Pattern** in .NET.  
The main goal is to demonstrate how these patterns can improve code organization, maintainability, and testability when working with databases.  

By applying these patterns:
- Data access logic is separated from business logic.
- The code becomes more **reusable**, **clean**, and **easy to maintain**.
- Database transactions can be managed in a more controlled way.

---

## 🛠️ Technologies Used
- **C# / .NET**
- **Entity Framework Core**
- **SQL Server**
- **Repository Pattern**
- **Unit of Work Pattern**

---

## 📂 Project Structure
- **Models** → Represent the database entities.  
- **Repositories** → Contain all the data access logic for each entity.  
- **Unit of Work** → Acts as a single point of coordination for multiple repositories and database transactions.  
- **Controllers / Services** → Contain the business logic and communicate with repositories through the Unit of Work.  

---

## 🔑 Repository Pattern Explained
The **Repository Pattern** is a design pattern that abstracts data access logic into a separate layer.  
Instead of writing raw queries in controllers or services, we use repositories to interact with the database.  

**Advantages:**
- Separation of concerns.  
- Cleaner and more testable code.  
- Centralized data access logic.

---

# 🔑 Unit of Work Pattern (Detailed)

### 📘 What is Unit of Work?
The **Unit of Work** pattern coordinates the work of multiple repositories by creating a single transaction.  
It ensures that all database operations either **succeed together** or **fail together**.

---

### ✅ Key Benefits
- Centralized **transaction management**.  
- Keeps data **consistent**.  
- Works as a **single entry point** to multiple repositories.  
- Helps write **clean and maintainable code**.  

---

**Example:**
```csharp
public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
}

---
public interface IUnitOfWork : IDisposable
{
    IBaseRepository Authors { get; }
    IBaseRepository Books { get; }
    int Complete();
}

