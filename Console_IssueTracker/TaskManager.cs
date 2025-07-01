using System.Globalization;
using System.Net.NetworkInformation;

namespace Console_IssueTracker
{
    public class TaskManager
    {
        private List<Task> _tasks = new List<Task>();

        public void AddTask(string title)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                    throw new ArgumentException("Заголовок задачи не может быть пустым!");

                var newTask = new Task(title);
                _tasks.Add(newTask);

                Console.WriteLine($"Задача добавлена: {newTask}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
            }
            Console.ReadLine();
        }
        public void RemoveTask()
        {
            try
            {
                Console.Clear();
                PrintTasks(_tasks);

                Console.WriteLine("Введите номер задачи, которую хотите удалить: ");
                if (!int.TryParse(Console.ReadLine(), out int taskNumber))
                {
                    throw new ArgumentException("Требуется ввести число!");
                }
                if (taskNumber < 1 || taskNumber > _tasks.Count)
                {
                    throw new IndexOutOfRangeException("Такого номера задачи не существует!");
                }

                var taskToRemove = _tasks[taskNumber - 1];
                _tasks.RemoveAt(taskNumber - 1);

                Console.WriteLine($"Удалена задача: {taskToRemove}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении: {ex.Message}");
            }
            Console.ReadLine();
        }
        public void ToggleTaskCompletion(int taskNumber)
        {
            try
            {
                if (taskNumber < 1 || taskNumber > _tasks.Count)
                    throw new IndexOutOfRangeException("Неверный номер задачи!");

                var task = _tasks[taskNumber - 1];
                task.IsCompleted = !task.IsCompleted;

                Console.WriteLine($"Задача обновлена: \n{task}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при изменение статуса: {ex.Message}");
            }
            Console.ReadLine();
        }
        public void PrintTasks(IEnumerable<Task> tasks)
        {
            int number = 1;
            foreach (var task in tasks)
            {
                Console.WriteLine($"\t{number}. {task}");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                number++;
            }
        }
        public void PrintOnTypeTasks()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nВыберите какие задачи вы хотите увидеть на экране: " +
                "\n\t1. Вывод актуальных (невыполненных) задач." +
                "\n\t2. Вывод всех задач." +
                "\n\t3. Вывод только выполненных заданий." +
                "\n\t4. Назад");

                Console.WriteLine("Ваш выбор: ");
                if (!int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    throw new ArgumentException("Ошибка: нужно ввести число!");
                }
                if (userChoice < 1 || userChoice > 4)
                {
                    throw new IndexOutOfRangeException("Ошибка: нужно ввести число из списка!");
                }

                switch (userChoice)
                {
                    case 1:
                        IEnumerable<Task> actTasks = _tasks.Where(p => !p.IsCompleted);

                        Console.WriteLine("Актуальные (невыполненные) задачи: ");
                        PrintTasks(actTasks);
                        Console.ReadLine();
                        break;
                    case 2:
                        IEnumerable<Task> allTasks = _tasks.OrderBy(p => p.IsCompleted);

                        Console.WriteLine("Все задачи: ");
                        PrintTasks(allTasks);
                        Console.ReadLine();
                        break;
                    case 3:
                        IEnumerable<Task> finishTasks = _tasks.Where(p => p.IsCompleted);

                        Console.WriteLine("Выполненные задачи: ");
                        PrintTasks(finishTasks);
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Выполняем переход обратно...");
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда!");
                        Console.ReadLine();
                        break;
                }
            }
        }
        public void SortTasks()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите по какому параметру вы хотите отсортировать задачи: " +
                    "\n\t1. По дате выполнения" +
                    "\n\t2. По приоритету" +
                    "\n\t3. Выход");

                if (!int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    throw new ArgumentException("Ошибка: нужно ввести число!");
                }
                if (userChoice < 1 || userChoice > 3)
                {
                    throw new IndexOutOfRangeException("Ошибка: нужно ввести число из списка!");
                }

                switch (userChoice)
                {
                    case 1:
                        IEnumerable<Task> sortDateTask = _tasks.OrderBy(p => p.Deadline).ToList();

                        Console.WriteLine("Вывод списка задач, отсортированных по дате выполнения: ");
                        PrintTasks(sortDateTask);
                        Console.ReadLine();
                        return;
                    case 2:
                        IEnumerable<Task> sortPriorityTask = _tasks.OrderBy(p => p.Priority).ToList();

                        Console.WriteLine("Вывод списка задач, отсортированных по приоритету: ");
                        PrintTasks(sortPriorityTask);
                        Console.ReadLine();
                        return;
                    case 3:
                        Console.WriteLine("Выполняем переход обратно...");
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда!");
                        Console.ReadLine();
                        break;
                }
                Console.ReadLine();
            }
        }
        public void ChangeTask()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Выберите параметр, который хотите изменить: " +
                    "\n\t1. Заголовок" +
                    "\n\t2. Статус задачи ([ ] - невыполнена, [X} - выполнена" +
                    "\n\t3. Описание задачи" +
                    "\n\t4. Приоритет задачи" +
                    "\n\t5. Срок задачи" +
                    "\n\t6. Выход");
                if (int.TryParse(Console.ReadLine(), out int userChoice) && userChoice == 6)
                {
                    Console.WriteLine("Возвращаемся обратно...");
                    return;
                }

                PrintTasks(_tasks);

                Console.WriteLine("Введите номер задачи, которую хотите изменить: ");
                if (!int.TryParse(Console.ReadLine(), out int number) && (number < 1 || number > _tasks.Count))
                {
                    Console.WriteLine("Ошибка: Неверный номер задачи!");
                    Console.ReadLine();
                    ChangeTask();
                }

                switch (userChoice)
                {
                    case 1:
                        ChangeTitle(number);
                        break;
                    case 2:
                        ToggleTaskCompletion(number);
                        break;
                    case 3:
                        ChangeDescription(number);
                        break;
                    case 4:
                        ChangePriority(number);
                        break;
                    case 5:
                        ChangeDeadline(number);
                        break;
                    case 6:
                        Console.WriteLine("Выполняем выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда!");
                        Console.ReadLine();
                        break;
                }
                //PrintTasks(_tasks);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Ошибка: {ex.Message}");
            }

        }

        // Методы изменения параметра таски
        public void ChangeTitle(int i)
        {
            Console.WriteLine("Введите новый заголовок: ");
            string title = Console.ReadLine();

            try
            {
                _tasks[i - 1].ChangeTitleTask(title);
                Console.WriteLine("Заголовок обновлён!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadLine();
        }
        public void ChangeDescription(int i)
        {
            Console.WriteLine("Введите новое описание: ");
            string description = Console.ReadLine();

            try
            {
                _tasks[i - 1].ChangeDescriptionTask(description);
                Console.WriteLine("Описание обновлено!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadLine();
        }
        public void ChangePriority(int i)
        {
            Console.WriteLine("Введите цифрой новый приоритет (от 1 до 3): ");
            if (!int.TryParse(Console.ReadLine(), out int priority) && (priority < 1 || priority > 3))
            {
                Console.WriteLine("Ошибка: приоритет не может быть меньше 1 и больше 3");
                Console.ReadLine();
                return;
            }

            try
            {
                _tasks[i - 1].ChangePriorityTask(priority);
                Console.WriteLine("Описание обновлено!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            Console.ReadLine();
        }
        public void ChangeDeadline(int i)
        {
            Console.WriteLine("Введите новый дедлайн в формате ДД.ММ.ГГГГ ЧЧ:мм или ДД.ММ.ГГГГ (например 29.06.2025 13:00 или 29.06.2025): ");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input,
                new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" }, // Поддерживаем 2 формата
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime deadline))
            {
                // Проверка, что дедлайн не в прошлом (с учётом времени)
                if (deadline >= DateTime.Now) // Теперь сравниваем с Now, а не Today
                {
                    _tasks[i - 1].ChangeDeadlineTask(deadline);
                    Console.WriteLine("Дедлайн изменён!");
                }
                else
                {
                    Console.WriteLine("Дедлайн не может быть в прошлом!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка формата! Используйте ДД.ММ.ГГГГ ЧЧ:мм или ДД.ММ.ГГГГ");
            }
            Console.ReadLine();
        }

        // Методы для работы с файлами
        public void LoadTasksFromFile()
        {
            try
            {
                if (!File.Exists("tasks.txt"))
                {
                    File.WriteAllText("tasks.txt", string.Empty);
                    return;
                }

                string[] lines = File.ReadAllLines("tasks.txt");
                _tasks.Clear();

                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    try
                    {
                        Task task = Task.Parse(line);
                        _tasks.Add(task);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при чтении задачи: {ex.Message}");
                    }
                }
                Console.WriteLine($"Загружено {_tasks.Count} задач из файла.");
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Ошибка при загрузке задач: {ex.Message}");
            }
        }
        public void SaveTasksToFile()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (Task task in _tasks)
                {
                    lines.Add(task.ToFileString());
                }

                File.WriteAllLines("tasks.txt", lines);
                Console.WriteLine($"Сохранено {_tasks.Count} задач в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении задач: {ex.Message}");
            }
        }
    }
}
