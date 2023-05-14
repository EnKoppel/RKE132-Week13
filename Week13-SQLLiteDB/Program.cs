using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
//RemoveCustomer(CreateConnection());
FindCustomer();

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version=3; New=True; Compress=True");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found");
    }

    return connection;
}

static void ReadData(SQLiteConnection connection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = connection.CreateCommand();
    command.CommandText = "SELECT rowid, firstName, lastName, dateOfBirth FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; Date of Birth: {readerStringDoB}");
    }

    reader.Close();
}

static void InsertCustomer(SQLiteConnection connection)
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob = Console.ReadLine();

    command = connection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES ('{fName}', '{lName}', '{dob}')";

    int rowsInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Rows inserted: {rowsInserted}");

    ReadData(connection);
}

static void RemoveCustomer(SQLiteConnection connection)
{
    SQLiteCommand command;

    Console.WriteLine("Enter the rowid of the customer to delete:");
    string idToDelete = Console.ReadLine();

    command = connection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";

    int rowsDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"Rows deleted: {rowsDeleted}");

    ReadData(connection);
}

static void FindCustomer()
{

}

