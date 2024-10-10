namespace TODO_Master
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Domain.Task[] taskList = new Domain.Task[100];
            int taskCounter = 0;
            int nextTaskId = 1;


            bool shouldNotExit = true;

            while (shouldNotExit)
            {
                WriteLine("1. Add task");
                WriteLine("2. List tasks");
                WriteLine("3. Exit");

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

                        taskList[taskCounter++] = new Domain.Task(nextTaskId++, title, dueDate);

                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:

                        WriteLine("Id  Title                                  Due Date   ");
                        WriteLine("-------------------------------------------------------");

                        foreach (Domain.Task task in taskList)
                        {
                            if (task == null) continue;

                            WriteLine($"{task.id}  {task.title}                    {task.dueDate}");
                        }

                        ReadKey(true);

                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:

                        shouldNotExit = false;

                        break;
                }

                Clear();
            }

        }
    }
}
