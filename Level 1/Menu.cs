namespace Level_1
{
    public class Menu
    {
        public void Start()
        {
            BirthdayManager manager = new BirthdayManager();
            string filePath = "birthdays.json";
            manager.LoadFromFile(filePath);

            DisplayTodayAndUpcomingBirthdays(manager);

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить день рождения");
                Console.WriteLine("2. Удалить день рождения");
                Console.WriteLine("3. Показать все дни рождения");
                Console.WriteLine("4. Сохранить и выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBirthday(manager);
                        break;
                    case "2":
                        RemoveBirthday(manager);
                        break;
                    case "3":
                        DisplayAllBirthdays(manager);
                        break;
                    case "4":
                        manager.SaveToFile(filePath);
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        static void DisplayTodayAndUpcomingBirthdays(BirthdayManager manager)
        {
            Console.Clear();
            var birthdays = manager.GetTodayAndUpcomingBirthdays();
            Console.WriteLine("Сегодняшние и ближайшие дни рождения:");
            foreach (var b in birthdays)
            {
                Console.WriteLine(b);
            }
        }

        static void AddBirthday(BirthdayManager manager)
        {

            Console.Clear();
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите дату рождения (гггг-мм-дд): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Неправильный формат даты. Попробуйте снова: ");
            }
            manager.AddBirthday(new Person { Name = name, Birthday = date });
        }

        static void RemoveBirthday(BirthdayManager manager)
        {
            Console.Clear();
            DisplayAllBirthdays(manager);
            Console.Write("Введите имя для удаления: ");
            string name = Console.ReadLine();
            manager.RemoveBirthday(name);
        }

        static void DisplayAllBirthdays(BirthdayManager manager)
        {
            Console.Clear();
            var birthdays = manager.DisplayAllBirthdays();
            Console.WriteLine("Все дни рождения:");
            foreach (var b in birthdays)
            {
                Console.WriteLine(b);
            }
        }
    }
}