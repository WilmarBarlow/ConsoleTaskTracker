using Console_IssueTracker;

class Program
{
    static void Main()
    {
        var manager = new TaskManager();

        ShowStartScreen();
        Console.ReadLine();

        bool isRunnung = true;
        while (isRunnung)
        {
            Console.Clear();
            Console.WriteLine("\nВыберите действие: " +
                "\n\t1. Добавить задачу" +
                "\n\t2. Посмотреть список задач" +
                "\n\t3. Редактировать задачу" +
                "\n\t4. Удалить задачу" +
                "\n\t5. Отсортировать задачи" +
                "\n\t6. Выход из программы");
            
            if (!int.TryParse(Console.ReadLine(), out int userChoice))
            {
                Console.WriteLine("Ошибка: нужно ввести число!");
                Console.ReadLine();
                continue;
            }
            if (userChoice < 1 || userChoice > 6)
            {
                Console.WriteLine("Ошибка: выберите действие от 1 до 6!");
                Console.ReadLine();
                continue;
            }
            if (userChoice == 6)
            {
                Console.Clear();
                Console.WriteLine("Программа завершает свою работу!");
                break;
            }

            switch (userChoice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Введите заголовок задачи, которую хотите добавить: ");
                    string title = Console.ReadLine();

                    manager.AddTask(title);
                    break;
                case 2:
                    manager.PrintOnTypeTasks();
                    break;
                case 3:
                    manager.ChangeTask();
                    break;
                case 4:
                    manager.RemoveTask();
                    break;
                case 5:
                    manager.SortTasks();
                    break;
                default:
                    Console.WriteLine("Неизвестная команда!");
                    break;
            }
        }

        Console.ReadLine();
    }

    static void ShowStartScreen()
    {
        Console.Title = "Task Tracker v0.1";

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(@"  
  _______         _     _______              _             
 |__   __|       | |   |__   __|            | |            
    | | __ _ ___ | | __   | | _ __  __ _ ___| | _____ _ __ 
    | |/ _` / __|| |/ /   | || '__|/ _` / _/| |/ / _ \ '__|
    | | (_| \__ \|   <    | || |    (_| \ (_|   <  __/ |   
    |_|\__,_|___/|_|\_\   |_||_|   \__,_|\__|_|\_\___|_|   
                                                        ");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  Добро пожаловать в Task Tracker v0.1!\n\t    By Wilmar Barlow");
        Console.WriteLine("  -----------------------------------");
        Console.WriteLine("Нажмите любую клавишу для продолжения");
    }
}