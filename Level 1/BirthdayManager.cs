using Newtonsoft.Json;
namespace Level_1
{
    public class BirthdayManager
    {
        private List<Person> birthdays = new List<Person>();

        public void AddBirthday(Person birthday)
        {
            birthdays.Add(birthday);
        }

        public void RemoveBirthday(string name)
        {
            birthdays.RemoveAll(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Person> GetTodayAndUpcomingBirthdays()
        {
            DateTime today = DateTime.Today;
            return birthdays.Where(b => (b.Birthday.AddYears(DateTime.Now.Year - b.Birthday.Year) - today).TotalDays < 10).ToList()
                            .OrderBy(b => b.Birthday).ToList();
        }

        public void SaveToFile(string filePath)
        {
            var json = JsonConvert.SerializeObject(birthdays);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                birthdays = JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
            }
        }

        public List<Person> DisplayAllBirthdays()
        {
            return birthdays.OrderBy(b => b.Birthday).ToList();
        }

    }
}