# InventoryManagerAPI
InventoryManagerAPI is a implementation of a RESTful API that was tested on over 1.5 million records in all of the data sources it handled. It encompasses the processing of CSV files that vary in their configurations. It then uses performance oriented function for bulk inserting those records to database. The project employs CLEAN architecture, and it also adheres to SOLID principles, enabling easy extension of the codebase while maintaining low coupling between components.

# Technologies Used

- .NET 7
- C#
- Dapper (ORM)
- Serilog
- SQL

# Architecture

The project follows a CLEAN architecture pattern, ensuring separation of concerns and maintaining a clear structure between the presentation, application, and data layers. It emphasizes modularity and testability by decoupling components.

## Layers:
![image](https://github.com/zoneel/InventoryManagerAPI/assets/40122657/5e47dcaa-4f50-44df-a1af-2e3d60715010)


# Database Overview
![image](https://github.com/zoneel/InventoryManagerAPI/assets/40122657/b9d62080-da38-43b6-a3f8-aa378c189c74)

The SQL database stores information extracted from the provided CSV files:
- Products: Stores product data meeting specific criteria (non-cable products sent within 24 hours).
- Inventory: Records product stock levels for items sent within 24 hours.
- Prices: Contains pricing details for products, considering logistical units.

# API Endpoints
![image](https://github.com/zoneel/InventoryManagerAPI/assets/40122657/6f4b2836-d34d-4f8c-ae0c-bd699ad4225f)

1. **Endpoint 1**: Allows user to upload CSV files and then processes it in parallel and stores relevant data in the database.
   - Reads Products.csv and saves qualifying products to the SQL table.
   - Reads Inventory.csv and records stock levels for specific products in the SQL table.
   - Reads Prices.csv and stores product pricing data in the SQL table.
  
     Benchmarking this endpoint showed that 

2. **Endpoint 2**: Accepts a product SKU as a parameter and returns the following information:
   - Product Name
   - EAN (European Article Number)
   - Manufacturer Name
   - Category
   - Product Image URL
   - Stock Level
   - Logistic Unit
   - Net Purchase Price
   - Delivery Cost

# Performance
Given the volume of records this application manages, optimizing performance has been a primary focus.
To achieve this, I've implemented strategies to enhance its efficiency. By employing parallel execution of tasks, each request now completes approximately ~9 seconds faster, significantly improving overall execution time.

Furthermore, the utilization of Dapper Pro and its BulkInsert function has significantly contributed to performance enhancements. 
This feature enables a single bulk insert operation into the database, replacing individual, separate insertions. This approach notably elevates the application's performance by streamlining the data insertion process.

After conducting benchmarks, I can confirm that the execution time for the first endpoint, handling 1522472 test data records, averages around 18 seconds from start to finish.
![image](https://github.com/zoneel/InventoryManagerAPI/assets/40122657/77e2d16f-1818-4a93-a188-6468b86af333)


# Launch Instructions

To launch the project:
1. Clone this repository.
2. Install necessary dependencies.
3. Create database using databaseCreation.sql script in sqlScripts folder.
4. Configure database connection settings in appsettings.json
5. Run the application.
