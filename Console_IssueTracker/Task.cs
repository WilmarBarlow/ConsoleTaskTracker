﻿
using System.Globalization;

namespace Console_IssueTracker
{
    public class Task
    {
        // Поля
        private string _title;
        private string _description;
        private int? _priority = null;
        private DateTime _deadline = DateTime.MinValue;

        // Свойства
        public string Title
        {
            get => _title;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Ошибка: Заголовок задачи не может быть пустым!!!");
                _title = value;
            }
        }
        public string Description
        {
            get => _description;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Ошибка: Описание задачи не может быть пустым!!!");
                _description = value;
            }
        }
        public int? Priority
        {
            get => _priority;
            private set
            {
                if (value < 1 || value > 3)
                    throw new ArgumentException("Ошибка: приоритет не может быть меньше единицы или больше 3.");
                _priority = value;
            }
        }
        public DateTime Deadline
        {
            get => _deadline;
            set
            {
                if (value ==  DateTime.MinValue)
                {
                    // сброс дедлайна
                    _deadline = DateTime.MinValue;
                    return;
                }

                if (value > DateTime.Now)
                {
                    _deadline = value;
                }
                else
                {
                    Console.WriteLine("Предупреждение: нельзя установить прошедшую дату как дедлайн\"");
                }

            }
        }
        public bool IsCompleted { get; set; }

        // Конструктор
        public Task(string title)
        {
            Title = title;
            Description = "Описание отсутствует!";
            IsCompleted = false;
        }
        public Task(string title, string description)
        {
            Title = title;
            Description = description;
            IsCompleted = false;
        }
        public Task(string title, string description, int priority)
        {
            Title = title;
            Description = description;
            Priority = priority;
            IsCompleted = false;
        }
        public Task(string title, int priority)
        {
            Title = title;
            Description = "Описание отсутствует!";
            Priority = priority;
            IsCompleted = false;
        }

        // Методы
        public void ChangeTitleTask(string title)
        {
            Title = title;
        }
        public void ChangeDescriptionTask(string description)
        {
            Description = description;
        }
        public void ChangePriorityTask(int priority)
        {
            Priority = priority;
        }
        public void ChangeDeadlineTask(DateTime deadline)
        {
            Deadline = deadline;
        }

        public override string ToString()
        {
            return $"[{(IsCompleted ? "X" : " ")}] Задача: {_title}" +
                $"\n\tОписание задачи: {_description}" +
                $"\n\tПриоритет задачи: {(_priority >= 1 && _priority <= 3 ? _priority : "-" )} | Срок до: {(_deadline == DateTime.MinValue ? "-" : (_deadline > DateTime.Now ? _deadline : "просрочено"))}";
        }

        public string ToFileString()
        {
            return $"{Title}|{Description}|{IsCompleted}|{(_priority.HasValue ? _priority.ToString() : "")}|{Deadline:dd.MM.yyyy HH:mm}"; // проверить работает ли без часов
        }
        public static Task Parse(string line)
        {
            var parts = line.Split('|');
            if (parts.Length != 5)
                throw new FormatException("Неверный формат строки задачи");

            int? priority = null;
            if (!string.IsNullOrEmpty(parts[3]) && int.TryParse(parts[3], out int parsedPriority))
            {
                priority = parsedPriority;
            }

            DateTime deadline;
            if (!DateTime.TryParseExact(parts[4],
                new[] { "dd.MM.yyyy HH:mm", "dd.MM.yyyy" },
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out deadline))
            {
                throw new FormatException("Неверный формат даты. Используйте dd.MM.yyyy HH:mm или dd.MM.yyyy");
            }

            if (!parts[4].Contains(":"))
            {
                deadline = deadline.Date.AddHours(23).AddMinutes(59);
            }

            return new Task(parts[0])
            {
                Description = parts[1],
                IsCompleted = bool.Parse(parts[2]),
                Priority = priority,
                Deadline = deadline
            };
        }

    }
}
