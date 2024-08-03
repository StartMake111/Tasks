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
                Console.WriteLine("3. Изменить день рождения");
                Console.WriteLine("4. Показать все дни рождения");
                Console.WriteLine("5. Сохранить и выйти");

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
                        ChangeBirthDay(manager);
                        break;
                    case "4":
                        DisplayAllBirthdays(manager);
                        break;
                    case "5":
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
            int num = 0;
            foreach (var b in birthdays)
            {
                num++;
                Console.WriteLine($"{num}:{b}");
            }
        }
        static void ChangeBirthDay(BirthdayManager manager)
        {
            Console.Clear();
            DisplayAllBirthdays(manager);
            Console.WriteLine("Выберите номер");
            string num = Console.ReadLine();
            var birthdays = manager.DisplayAllBirthdays();
            while (int.Parse(num) > birthdays.Count || int.Parse(num) < 0)
            {
                Console.WriteLine("Такого номера нет");
            }
            ;
            Console.Clear();
            var birthday = birthdays.Skip(int.Parse(num) - 1).FirstOrDefault();
            Console.WriteLine($"День рождения пользователя {birthday}");
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите дату рождения (гггг-мм-дд): ");
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.Write("Неправильный формат даты. Попробуйте снова: ");
            }
            manager.change(int.Parse(num), name, date);
            Console.Clear();
        }
    }
}