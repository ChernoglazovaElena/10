using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.Program;

namespace ConsoleApp2
{
    internal enum Type
    {
        Int,
        Double,
        String
    }
    internal class AccountPost
    {

        protected bool start2 = true; // Переменная для возможности повтороного ввода 
        protected dynamic worker; // Переменная в которой хранится выбранный сотрудник
        protected int PosStartCursor; // верхняя позиция курсора для передачи в стрелочное меню
        protected int PosEndCursor; // нижняя позиция курсора для передачи в стрелочное меню
        protected int PosCursorInMainMenu; // позиция курсора в основном меню 
        protected bool DopMenu = false; // проверка какое меню отрисовывать
        protected bool start = true; // переменная для старта цикла
        protected bool SerchMenu = false; // переменная для отрисовки меню поиска
        protected List<dynamic> SerchList;

        private ConsoleKeyInfo ReturnKey;

        public int ID;
        public string Nickname;
        public string Login;
        public string Password;
        public int Post;
        public bool Connection;

        protected dynamic CreateNewElementInfo(int x, bool NumberInString = false, bool SpacesInString = false) // функция для создания или обновления аккаунта
        {
            Console.CursorVisible = true;
            bool Error = false;
            dynamic Give = x;
            if (x != (int)Type.String)
            {
                try
                {
                    if (x == (int)Type.Int)
                    {
                        Give = Convert.ToInt32(Console.ReadLine());
                    }
                    else if (x == (int)Type.Double)
                    {
                        Give = Convert.ToDouble(Console.ReadLine());
                    }
                }
                catch
                {
                    Error = true;
                    Console.WriteLine("Ошибка: ввод любых символов, кроме чисел запрещен");
                }
                finally
                {
                    if (Error == true)
                    {
                        Give = null;
                    }
                }
            }
            else
            {
                Give = Console.ReadLine();
                if (Give.Trim() != "") // проверка на пустую строку
                {
                    if (!SpacesInString && !NumberInString)
                    {
                        foreach (char element in Give)
                        {
                            if (!char.IsLetter(element) || char.IsWhiteSpace(element))
                            {
                                Console.WriteLine("Ошибка: строка может состоять только из букв");
                                Error = true;
                                break;
                            }
                        }
                    }
                    else if (SpacesInString && !NumberInString)
                    {
                        foreach (char element in Give)
                        {
                            if (char.IsDigit(element))
                            {
                                Console.WriteLine("Ошибка: строка может состоять только из букв и пробелов");
                                Error = true;
                                break;
                            }
                        }
                    }
                    else if (!SpacesInString && NumberInString)
                    {
                        foreach (char element in Give)
                        {
                            if (char.IsWhiteSpace(element))
                            {
                                Console.WriteLine("Ошибка: строка может состоять только из букв и цифр");
                                Error = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Give = null;
                    Console.WriteLine("Ошибка: строка не должны быть пустой");
                }
            }
            return Give;
        }
        protected void DrewInfoAccount(AccountPost Account)
        {
            string Nick = "";
            if (Account.Nickname != null)
            {
                Nick = Account.Nickname;
            }
            else
            {
                Nick = Account.Login;
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Добро пожаловть, " + Nick + "\t" + "Ваша роль : " + DrewPostWorker(Account.Post));
            Console.SetCursorPosition(0, 2);
        }

        protected string DrewPostWorker(int x)
        {
            string a;
            if (x == (int)RW.Администратор)
            {
                a = Convert.ToString(RW.Администратор);
            }
            else if (x == (int)RW.Кассир)
            {
                a = Convert.ToString(RW.Кассир);
            }
            else if (x == (int)RW.Склад)
            {
                a = Convert.ToString(RW.Склад);
            }
            else if (x == (int)RW.Менеджер)
            {
                a = Convert.ToString(RW.Менеджер);
            }
            else if (x == (int)RW.Бухгалтер)
            {
                a = Convert.ToString(RW.Бухгалтер);
            }
            else
            {
                a = Convert.ToString("null");
            }
            return a;
        }

        protected List<dynamic> ArrowMenu(int StartPositionCursor, int EndPositionCursor) // Стрелочное меню
        {
            // StartPositionCursor - начальная позиция курсора
            // EndPositionCursor - конченая позиция курсора
            Console.CursorVisible = false;
            if (EndPositionCursor < StartPositionCursor)
            {
                EndPositionCursor = StartPositionCursor;
            }
            int PostPosition = 0;
            int PosCursor = StartPositionCursor;
            bool start = true;
            while (start)
            {
                Console.SetCursorPosition(0, PosCursor);
                Console.Write("->");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == (ConsoleKey)HotKey.СтрелкаВверх)
                {
                    PostPosition = PosCursor--;
                    if (PosCursor < StartPositionCursor)
                    {
                        PosCursor = EndPositionCursor;
                    }
                }
                else if (key.Key == (ConsoleKey)HotKey.СтрелкаВниз)
                {
                    PostPosition = PosCursor++;
                    if (PosCursor > EndPositionCursor)
                    {
                        PosCursor = StartPositionCursor;
                    }
                }
                else
                {
                    start = false;
                    ReturnKey = key;
                }
                Console.SetCursorPosition(0, PostPosition);
                Console.Write("  ");
            }
            List<dynamic> ReturnElemen = new List<dynamic>
            {
                ReturnKey.Key,
                PosCursor
            };
            PosCursor = 0;
            return ReturnElemen;
        }
    }
}