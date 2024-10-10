using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using TODO_Master.Domain;
using static System.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TODO_Master
{
    class Program
    {
        static string connectionString = "Data Source =.; Initial Catalog = TODO; Integrated Security = true; Encrypt=True;Trust Server Certificate=True";

        static void Main(string[] args)
        {
            bool shouldNotExit = true;

            while (shouldNotExit)
            {
                WriteLine("1. Add task");
                WriteLine("2. List tasks");
                WriteLine("3. Delete All tasks");
                WriteLine("4. Exit");

                ConsoleKeyInfo keyPressed = ReadKey(true);

                Clear();

                switch (keyPressed.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:

                        Write("Title: ");

                        string title = ReadLine();

                        Write("Due date (yyyy-mm-dd hh:mm): ");

                        DateTime dueDate = DateTime.Parse(ReadLine());

                        CreateTask(title, dueDate);

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:

                        List<Domain.Task> taskList = FetchAllTasks();

                        WriteLine("Id  Title                                  Due Date   ");
                        WriteLine("-------------------------------------------------------");

                        foreach (Domain.Task task in taskList)
                        {
                            if (task == null) continue;

                            WriteLine($"{task.id}  {task.title}                   {task.dueDate}");
                        }

                        ReadKey(true);

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        DeleteAllTask();
                        WriteLine("Successfully deleted all tasks");  
                        Thread.Sleep(2000);

                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:

                        shouldNotExit = false;

                        break;
                }

                Clear();
            }

        }

        static void CreateTask(string title, DateTime dueDate)
        {
            string queryString = @"INSERT INTO Task (Title, DueDate)
                                   VALUES (@Title, @DueDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@DueDate", dueDate);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        static List<Domain.Task> FetchAllTasks()
        {
            string queryString = @"SELECT [Id]
                                         ,[Title]
                                         ,[DueDate]
                                     FROM [TODO].[dbo].[Task]";

            List<Domain.Task> taskList = new List<Domain.Task>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        //WriteLine(reader[1]);
                        int id = int.Parse(reader["Id"].ToString());
                        string title = reader["Title"].ToString();
                        DateTime dueDate = DateTime.Parse(reader["DueDate"].ToString());

                        Domain.Task task = new Domain.Task(id, title, dueDate);

                        taskList.Add(task);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {

                    WriteLine(ex.Message);
                }

            }

            return taskList;
        }

        static void DeleteAllTask()
        {
            string queryString = @"DELETE FROM Task";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                               
                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
