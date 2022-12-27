using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.Program;

namespace ConsoleApp2
{
    internal class Log_In
    {
        private static string Login;
        private static string Password;
        public static string[] Log_in()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Добро пожаловать, чтобы продолжить войдите в аккаунт");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Логин : ");
            Console.WriteLine("Пароль : ");
            Console.SetCursorPosition(8, 2);
            Login = Console.ReadLine();
            Password = ReadPassword(9, 3);
            string[] text = new string[2] { Login, Password };
            return text;
        }
        private static string ReadPassword(int x, int y)
        {
            List<char> PasswordList = new List<char>();
            string Password = "";
            bool start = true;
            while (start)
            {
                Console.SetCursorPosition(x, y);
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == (ConsoleKey)HotKey.Выбрать)
                {
                    start = false;
                }
                else if (key.Key == (ConsoleKey)HotKey.Стереть)
                {
                    if (PasswordList.Count > 0)
                    {
                        PasswordList.RemoveAt(x - 10);
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(x + 1, y);
                        x--;
                    }
                }
                else
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write("*");
                    PasswordList.Add(key.KeyChar);
                    x++;
                }
            }
            foreach (var element in PasswordList)
            {
                Password += element;
            }
            return Password;
        }
    }
}
