using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text.Json.Serialization;

namespace ConsoleApp2
{
    internal class Program
    {
        internal enum HotKey
        {
            Выбрать = ConsoleKey.Enter,
            Выход = ConsoleKey.Escape,
            Создать = ConsoleKey.F1,
            Сохранить = ConsoleKey.S,
            Удалить = ConsoleKey.Delete,
            Увеличить = ConsoleKey.OemPlus,
            Убавить = ConsoleKey.OemMinus,
            Стереть = ConsoleKey.Backspace,
            СтрелкаВверх = ConsoleKey.DownArrow,
            СтрелкаВниз = ConsoleKey.UpArrow
        }
        internal enum PD // Класс для проверки какую информацию захотел выбрать пользователь для изменения (admin)
        {
            ID,
            Login,
            Password,
            Post,
            Connetion
        }
        internal enum PD2 // Класс для проверки какую информацию захотел выбрать пользователь для изменения (manager)
        {
            ID,
            Name,
            Surname,
            Otchestvo,
            SeriaNomerPasspota,
            DateOfBirth,
            ZP,
            Connect,
            Post
        }
        internal enum DPR // Класс для поверки информации у товаров и записей (склад и менеджер) 
        {
            ID,
            Title,
            Price,
            AlotOf,
            DateTime = 4,
            Add = 3
        }
        internal enum RW // Класс для создания аккаунта нового работника
        {
            Администратор = 1,
            Кассир = 2,
            Склад = 3,
            Менеджер = 4,
            Бухгалтер = 5
        }
        static void Main(string[] args_)
        {
            JsonDS.SerchJsonFile();
            bool start = true;
            while (start)
            {
                AccountPost Account = new AccountPost();
                Console.CursorVisible = true;
                string Error = "Неверный логин или пароль";

                List<AccountPost> workers = JsonDS.Deserialize<List<AccountPost>>("AccountPost.json");
                string[] text = Log_In.Log_in();

                foreach (var element in workers)
                {
                    if (element.Password == text[1] && element.Login == text[0])
                    {
                        Error = "Добро пожаловать " + element.Login;
                        Account = element;
                        break;
                    }
                }
                Console.SetCursorPosition(0, 5);
                Console.WriteLine(Error);
                Console.WriteLine("Нажмите Escape, чтобы выйти или любую другую клавишу, чтобы продолжить");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    start = false;
                }
                Console.Clear();
                if (Account.Post == (int)RW.Администратор && Error != "Неверный логин или пароль")
                {
                    Admin ad = new Admin();
                    ad.DrewInterface(Account, workers);
                }
                else if (Account.Post == (int)RW.Менеджер && Error != "Неверный логин или пароль")
                {
                    List<AccountUsers> newlist = JsonDS.Deserialize<List<AccountUsers>>("AccountUsers.json");
                    Manager kd = new Manager();
                    kd.DrewInterface(Account, newlist);
                }
                else if (Account.Post == (int)RW.Склад && Error != "Неверный логин или пароль")
                {
                    List<Product> newlist = JsonDS.Deserialize<List<Product>>("Product.json");
                    Sclad sc = new Sclad();
                    sc.DrewInterface(Account, newlist);
                }
                else if (Account.Post == (int)RW.Бухгалтер && Error != "Неверный логин или пароль")
                {
                    List<Recording> newlist = JsonDS.Deserialize<List<Recording>>("Recording.json");
                    Buh buh = new Buh();
                    buh.DrewInterface(Account, newlist);
                }
                else if (Account.Post == (int)RW.Кассир && Error != "Неверный логин или пароль")
                {
                    List<Product> newlist = JsonDS.Deserialize<List<Product>>("Product.json");
                    Kassir kass = new Kassir();
                    kass.DrewInterface(Account, newlist);
                }
            }
        }
    }
}