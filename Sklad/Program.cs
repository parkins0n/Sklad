using System;
using System.Data.SqlClient;

class Program
{
    static string serverConnectionString = "Server=parkinson\\STEP;Trusted_Connection=True;";
    static string databaseConnectionString = "Server=parkinson\\STEP;Database=Sklad;Trusted_Connection=True;";

    static void Main(string[] args)
    {
        CreateDatabase();

        using (SqlConnection connection = new SqlConnection(databaseConnectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Успішне підключення до бази даних 'Склад'.\n");

                CreateTable(connection);
                InsertTestData(connection);

                while (true)
                {
                    Console.WriteLine("Оберіть дію:");
                    Console.WriteLine("1. Показати всі товари");
                    Console.WriteLine("2. Показати всі типи товарів");
                    Console.WriteLine("3. Показати всіх постачальників");
                    Console.WriteLine("4. Показати товар з максимальною кількістю");
                    Console.WriteLine("5. Показати товар з мінімальною кількістю");
                    Console.WriteLine("6. Показати товар з мінімальною собівартістю");
                    Console.WriteLine("7. Показати товар з максимальною собівартістю");
                    Console.WriteLine("8. Показати товари за категорією");
                    Console.WriteLine("9. Показати товари за постачальником");
                    Console.WriteLine("10. Показати товар, який знаходиться на складі найдовше");
                    Console.WriteLine("11. Показати середню кількість товарів за кожним типом");
                    Console.WriteLine("12. Додати новий товар");
                    Console.WriteLine("13. Додати новий тип товару");
                    Console.WriteLine("14. Додати нового постачальника");
                    Console.WriteLine("15. Оновити товар");
                    Console.WriteLine("16. Оновити постачальника");
                    Console.WriteLine("17. Оновити тип товару");
                    Console.WriteLine("18. Видалити товар");
                    Console.WriteLine("19. Видалити постачальника");
                    Console.WriteLine("20. Видалити тип товару");
                    Console.WriteLine("21. Показати постачальника з найбільшою кількістю товарів");
                    Console.WriteLine("22. Показати постачальника з найменшою кількістю товарів");
                    Console.WriteLine("23. Показати тип товару з найбільшою кількістю одиниць");
                    Console.WriteLine("24. Показати тип товару з найменшою кількістю одиниць");
                    Console.WriteLine("0. Вийти");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            DisplayAllItems(connection);
                            break;
                        case "2":
                            DisplayAllTypes(connection);
                            break;
                        case "3":
                            DisplayAllSuppliers(connection);
                            break;
                        case "4":
                            DisplayItemWithMaxQuantity(connection);
                            break;
                        case "5":
                            DisplayItemWithMinQuantity(connection);
                            break;
                        case "6":
                            DisplayItemWithMinCost(connection);
                            break;
                        case "7":
                            DisplayItemWithMaxCost(connection);
                            break;
                        case "8":
                            Console.Write("Введіть категорію: ");
                            string category = Console.ReadLine();
                            DisplayItemsByCategory(connection, category);
                            break;
                        case "9":
                            Console.Write("Введіть постачальника: ");
                            string supplier = Console.ReadLine();
                            DisplayItemsBySupplier(connection, supplier);
                            break;
                        case "10":
                            DisplayOldestItem(connection);
                            break;
                        case "11":
                            DisplayAvgQuantityByType(connection);
                            break;
                        case "12":
                            Console.Write("Введіть назву товару: ");
                            string name = Console.ReadLine();
                            Console.Write("Введіть тип товару: ");
                            string type = Console.ReadLine();
                            Console.Write("Введіть постачальника: ");
                            string newSupplier = Console.ReadLine();
                            Console.Write("Введіть кількість: ");
                            int quantity = int.Parse(Console.ReadLine());
                            Console.Write("Введіть собівартість: ");
                            decimal cost = decimal.Parse(Console.ReadLine());
                            Console.Write("Введіть дату постачання (yyyy-MM-dd): ");
                            DateTime supplyDate = DateTime.Parse(Console.ReadLine());
                            InsertNewItem(connection, name, type, newSupplier, quantity, cost, supplyDate);
                            break;
                        case "13":
                            Console.Write("Введіть новий тип товару: ");
                            string newType = Console.ReadLine();
                            InsertNewItemType(connection, newType);
                            break;
                        case "14":
                            Console.Write("Введіть нового постачальника: ");
                            string newSupplierName = Console.ReadLine();
                            InsertNewSupplier(connection, newSupplierName);
                            break;
                        case "15":
                            Console.Write("Введіть ID товару для оновлення: ");
                            int itemId = int.Parse(Console.ReadLine());
                            Console.Write("Введіть нову назву товару: ");
                            string updatedName = Console.ReadLine();
                            Console.Write("Введіть новий тип товару: ");
                            string updatedType = Console.ReadLine();
                            Console.Write("Введіть нового постачальника: ");
                            string updatedSupplier = Console.ReadLine();
                            Console.Write("Введіть нову кількість: ");
                            int updatedQuantity = int.Parse(Console.ReadLine());
                            Console.Write("Введіть нову собівартість: ");
                            decimal updatedCost = decimal.Parse(Console.ReadLine());
                            Console.Write("Введіть нову дату постачання (yyyy-MM-dd): ");
                            DateTime updatedSupplyDate = DateTime.Parse(Console.ReadLine());
                            UpdateItem(connection, itemId, updatedName, updatedType, updatedSupplier, updatedQuantity, updatedCost, updatedSupplyDate);
                            break;
                        case "16":
                            Console.Write("Введіть ID постачальника для оновлення: ");
                            int supplierId = int.Parse(Console.ReadLine());
                            Console.Write("Введіть нове ім'я постачальника: ");
                            string updatedSupplierName = Console.ReadLine();
                            UpdateSupplier(connection, supplierId, updatedSupplierName);
                            break;
                        case "17":
                            Console.Write("Введіть ID типу товару для оновлення: ");
                            int typeId = int.Parse(Console.ReadLine());
                            Console.Write("Введіть нову назву типу товару: ");
                            string updatedTypeName = Console.ReadLine();
                            UpdateItemType(connection, typeId, updatedTypeName);
                            break;
                        case "18":
                            Console.Write("Введіть ID товару для видалення: ");
                            int deleteItemId = int.Parse(Console.ReadLine());
                            DeleteItem(connection, deleteItemId);
                            break;
                        case "19":
                            Console.Write("Введіть ID постачальника для видалення: ");
                            int deleteSupplierId = int.Parse(Console.ReadLine());
                            DeleteSupplier(connection, deleteSupplierId);
                            break;
                        case "20":
                            Console.Write("Введіть ID типу товару для видалення: ");
                            int deleteTypeId = int.Parse(Console.ReadLine());
                            DeleteItemType(connection, deleteTypeId);
                            break;
                        case "21":
                            ShowSupplierWithMostItems(connection);
                            break;
                        case "22":
                            ShowSupplierWithLeastItems(connection);
                            break;
                        case "23":
                            ShowItemTypeWithMostItems(connection);
                            break;
                        case "24":
                            ShowItemTypeWithLeastItems(connection);
                            break;
                        case "0":
                            Console.WriteLine("Вихід з програми.");
                            return;
                        default:
                            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                            break;
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка підключення до бази даних: {ex.Message}");
            }
        }
    }

    static void CreateDatabase()
    {
        using (SqlConnection connection = new SqlConnection(serverConnectionString))
        {
            try
            {
                connection.Open();
                string createDbQuery = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Sklad') CREATE DATABASE Sklad";
                using (SqlCommand command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Базу даних 'Sklad' створено або вона вже існує.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка створення бази даних: {ex.Message}");
            }
        }
    }

    static void CreateTable(SqlConnection connection)
    {
        string createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Items') AND type in (N'U'))
            CREATE TABLE Items (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Name NVARCHAR(50) NOT NULL,
                Type NVARCHAR(50) NOT NULL,
                Supplier NVARCHAR(50) NOT NULL,
                Quantity INT NOT NULL,
                Cost DECIMAL(18, 2) NOT NULL,
                SupplyDate DATE NOT NULL
            )";
        using (SqlCommand command = new SqlCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Таблицю 'Items' створено або вона вже існує.\n");
        }
    }

    static void InsertTestData(SqlConnection connection)
    {
        var items = new[]
        {
            new { Name = "Apple", Type = "фрукт", Supplier = "Supplier1", Quantity = 100, Cost = 52.00m, SupplyDate = new DateTime(2024, 1, 10) },
            new { Name = "Banana", Type = "фрукт", Supplier = "Supplier2", Quantity = 150, Cost = 89.00m, SupplyDate = new DateTime(2024, 2, 15) },
            new { Name = "Carrot", Type = "овоч", Supplier = "Supplier3", Quantity = 200, Cost = 41.00m, SupplyDate = new DateTime(2024, 3, 20) },
            new { Name = "Lettuce", Type = "овоч", Supplier = "Supplier4", Quantity = 250, Cost = 15.00m, SupplyDate = new DateTime(2024, 4, 25) },
            new { Name = "Strawberry", Type = "фрукт", Supplier = "Supplier5", Quantity = 50, Cost = 32.00m, SupplyDate = new DateTime(2024, 5, 30) },
            new { Name = "Tomato", Type = "овоч", Supplier = "Supplier6", Quantity = 80, Cost = 18.00m, SupplyDate = new DateTime(2024, 6, 10) },
            new { Name = "Blueberry", Type = "фрукт", Supplier = "Supplier7", Quantity = 60, Cost = 57.00m, SupplyDate = new DateTime(2024, 7, 15) },
            new { Name = "Pumpkin", Type = "овоч", Supplier = "Supplier8", Quantity = 90, Cost = 26.00m, SupplyDate = new DateTime(2024, 8, 20) },
            new { Name = "Cucumber", Type = "овоч", Supplier = "Supplier9", Quantity = 70, Cost = 16.00m, SupplyDate = new DateTime(2024, 9, 25) },
            new { Name = "Grapefruit", Type = "фрукт", Supplier = "Supplier10", Quantity = 40, Cost = 42.00m, SupplyDate = new DateTime(2024, 10, 30) }
        };

        foreach (var item in items)
        {
            string checkQuery = @"
            SELECT COUNT(*)
            FROM Items
            WHERE Name = @Name AND Type = @Type AND Supplier = @Supplier
                AND Quantity = @Quantity AND Cost = @Cost AND SupplyDate = @SupplyDate";

            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Name", item.Name);
                checkCommand.Parameters.AddWithValue("@Type", item.Type);
                checkCommand.Parameters.AddWithValue("@Supplier", item.Supplier);
                checkCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                checkCommand.Parameters.AddWithValue("@Cost", item.Cost);
                checkCommand.Parameters.AddWithValue("@SupplyDate", item.SupplyDate);

                int count = (int)checkCommand.ExecuteScalar();
                if (count == 0)
                {
                    string insertQuery = @"
                    INSERT INTO Items (Name, Type, Supplier, Quantity, Cost, SupplyDate)
                    VALUES (@Name, @Type, @Supplier, @Quantity, @Cost, @SupplyDate)";
                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Name", item.Name);
                        insertCommand.Parameters.AddWithValue("@Type", item.Type);
                        insertCommand.Parameters.AddWithValue("@Supplier", item.Supplier);
                        insertCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                        insertCommand.Parameters.AddWithValue("@Cost", item.Cost);
                        insertCommand.Parameters.AddWithValue("@SupplyDate", item.SupplyDate);

                        insertCommand.ExecuteNonQuery();
                    }
                    Console.WriteLine($"Вставлено новий запис: {item.Name}");
                }
                else
                {
                    Console.WriteLine($"Запис з назвою '{item.Name}' вже існує.");
                }
            }
        }
        Console.WriteLine("Тестові дані перевірено і вставлено у таблицю 'Items'.\n");
    }

    static void DisplayAllItems(SqlConnection connection)
    {
        Console.WriteLine("Вся інформація про товар:");
        string query = "SELECT * FROM Items";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayAllTypes(SqlConnection connection)
    {
        Console.WriteLine("Всі типи товарів:");
        string query = "SELECT DISTINCT Type FROM Items";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Type"]);
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayAllSuppliers(SqlConnection connection)
    {
        Console.WriteLine("Всі постачальники:");
        string query = "SELECT DISTINCT Supplier FROM Items";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Supplier"]);
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayItemWithMaxQuantity(SqlConnection connection)
    {
        Console.WriteLine("Товар з максимальною кількістю:");
        string query = "SELECT TOP 1 * FROM Items ORDER BY Quantity DESC";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayItemWithMinQuantity(SqlConnection connection)
    {
        Console.WriteLine("Товар з мінімальною кількістю:");
        string query = "SELECT TOP 1 * FROM Items ORDER BY Quantity ASC";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayItemWithMinCost(SqlConnection connection)
    {
        Console.WriteLine("Товар з мінімальною собівартістю:");
        string query = "SELECT TOP 1 * FROM Items ORDER BY Cost ASC";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayItemWithMaxCost(SqlConnection connection)
    {
        Console.WriteLine("Товар з максимальною собівартістю:");
        string query = "SELECT TOP 1 * FROM Items ORDER BY Cost DESC";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayItemsByCategory(SqlConnection connection, string category)
    {
        Console.WriteLine($"Товари категорії {category}:");
        string query = "SELECT * FROM Items WHERE Type = @category";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@category", category);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayItemsBySupplier(SqlConnection connection, string supplier)
    {
        Console.WriteLine($"Товари постачальника {supplier}:");
        string query = "SELECT * FROM Items WHERE Supplier = @supplier";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@supplier", supplier);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void DisplayOldestItem(SqlConnection connection)
    {
        Console.WriteLine("Товар, який знаходиться на складі найдовше:");
        string query = "SELECT TOP 1 * FROM Items ORDER BY SupplyDate ASC";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Supplier"]}, {reader["Quantity"]}, {reader["Cost"]}, {reader["SupplyDate"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }
    
    static void DisplayAvgQuantityByType(SqlConnection connection)
    {
        Console.WriteLine("Середня кількість товарів за кожним типом:");
        string query = "SELECT Type, AVG(Quantity) AS AvgQuantity FROM Items GROUP BY Type";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"{reader["Type"]}: {reader["AvgQuantity"]}");
            }
            reader.Close();
        }
        Console.WriteLine();
    }

    static void InsertNewItem(SqlConnection connection, string name, string type, string supplier, int quantity, decimal cost, DateTime supplyDate)
    {
        string query = @"
        INSERT INTO Items (Name, Type, Supplier, Quantity, Cost, SupplyDate)
        VALUES (@Name, @Type, @Supplier, @Quantity, @Cost, @SupplyDate)";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@Supplier", supplier);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@Cost", cost);
            command.Parameters.AddWithValue("@SupplyDate", supplyDate);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Товар '{name}' успішно додано.");
    }

    static void InsertNewItemType(SqlConnection connection, string type)
    {
        string query = @"
        INSERT INTO ItemTypes (TypeName)
        VALUES (@TypeName)";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@TypeName", type);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Тип товару '{type}' успішно додано.");
    }

    static void InsertNewSupplier(SqlConnection connection, string supplier)
    {
        string query = @"
        INSERT INTO Suppliers (SupplierName)
        VALUES (@SupplierName)";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@SupplierName", supplier);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Постачальник '{supplier}' успішно додано.");
    }

    static void UpdateItem(SqlConnection connection, int itemId, string name, string type, string supplier, int quantity, decimal cost, DateTime supplyDate)
    {
        string query = @"
        UPDATE Items
        SET Name = @Name, Type = @Type, Supplier = @Supplier, Quantity = @Quantity, Cost = @Cost, SupplyDate = @SupplyDate
        WHERE ItemID = @ItemID";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ItemID", itemId);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@Supplier", supplier);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@Cost", cost);
            command.Parameters.AddWithValue("@SupplyDate", supplyDate);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Товар з ID {itemId} успішно оновлено.");
    }

    static void UpdateSupplier(SqlConnection connection, int supplierId, string newName)
    {
        string query = @"
        UPDATE Suppliers
        SET SupplierName = @SupplierName
        WHERE SupplierID = @SupplierID";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@SupplierID", supplierId);
            command.Parameters.AddWithValue("@SupplierName", newName);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Постачальник з ID {supplierId} успішно оновлено.");
    }

    static void UpdateItemType(SqlConnection connection, int typeId, string newType)
    {
        string query = @"
        UPDATE ItemTypes
        SET TypeName = @TypeName
        WHERE TypeID = @TypeID";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@TypeID", typeId);
            command.Parameters.AddWithValue("@TypeName", newType);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Тип товару з ID {typeId} успішно оновлено.");
    }
    
    static void DeleteItem(SqlConnection connection, int itemId)
    {
        string query = @"
        DELETE FROM Items
        WHERE ItemID = @ItemID";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ItemID", itemId);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Товар з ID {itemId} успішно видалено.");
    }

    static void DeleteSupplier(SqlConnection connection, int supplierId)
    {
        string query = @"
        DELETE FROM Suppliers
        WHERE SupplierID = @SupplierID";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@SupplierID", supplierId);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Постачальник з ID {supplierId} успішно видалено.");
    }

    static void DeleteItemType(SqlConnection connection, int typeId)
    {
        string query = @"
        DELETE FROM ItemTypes
        WHERE TypeID = @TypeID";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@TypeID", typeId);

            command.ExecuteNonQuery();
        }
        Console.WriteLine($"Тип товару з ID {typeId} успішно видалено.");
    }

    static void ShowSupplierWithMostItems(SqlConnection connection)
    {
        string query = @"
        SELECT TOP 1 Supplier, SUM(Quantity) AS TotalQuantity
        FROM Items
        GROUP BY Supplier
        ORDER BY TotalQuantity DESC";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string supplier = reader["Supplier"].ToString();
                    int totalQuantity = Convert.ToInt32(reader["TotalQuantity"]);
                    Console.WriteLine($"Постачальник з найбільшою кількістю товарів: {supplier} (Кількість: {totalQuantity})");
                }
            }
        }
    }

    static void ShowSupplierWithLeastItems(SqlConnection connection)
    {
        string query = @"
        SELECT TOP 1 Supplier, SUM(Quantity) AS TotalQuantity
        FROM Items
        GROUP BY Supplier
        ORDER BY TotalQuantity ASC";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string supplier = reader["Supplier"].ToString();
                    int totalQuantity = Convert.ToInt32(reader["TotalQuantity"]);
                    Console.WriteLine($"Постачальник з найменшою кількістю товарів: {supplier} (Кількість: {totalQuantity})");
                }
            }
        }
    }

    static void ShowItemTypeWithMostItems(SqlConnection connection)
    {
        string query = @"
        SELECT TOP 1 Type, SUM(Quantity) AS TotalQuantity
        FROM Items
        GROUP BY Type
        ORDER BY TotalQuantity DESC";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string type = reader["Type"].ToString();
                    int totalQuantity = Convert.ToInt32(reader["TotalQuantity"]);
                    Console.WriteLine($"Тип товару з найбільшою кількістю одиниць: {type} (Кількість: {totalQuantity})");
                }
            }
        }
    }

    static void ShowItemTypeWithLeastItems(SqlConnection connection)
    {
        string query = @"
        SELECT TOP 1 Type, SUM(Quantity) AS TotalQuantity
        FROM Items
        GROUP BY Type
        ORDER BY TotalQuantity ASC";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    string type = reader["Type"].ToString();
                    int totalQuantity = Convert.ToInt32(reader["TotalQuantity"]);
                    Console.WriteLine($"Тип товару з найменшою кількістю одиниць: {type} (Кількість: {totalQuantity})");
                }
            }
        }
    }
}