using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleApp2.Program;

namespace ConsoleApp2
{
    internal class Admin : AccountPost/*, Interface*/
    {
        public virtual void DrewInterface(AccountPost Account, List<AccountPost> list)
        {
            // Acoount - аккаунт под которыим пользовотиль вошел в систему 
            // list - массив всех аккаунтов, которые может просматривать администратор
            while (start)
            {
                if (DopMenu == false) // проверка  зашел ли пользователь в додполнительное меню
                {
                    StartMenu(Account, list);
                    PosStartCursor = 2;
                    PosEndCursor = list.Count + 1;
                }
                else
                {
                    DrewInfoAboutWorker(worker);
                    PosStartCursor = (int)PD.ID;
                    PosEndCursor = (int)PD.Post;
                }
                List<dynamic> GetElement = ArrowMenu(PosStartCursor, PosEndCursor);
                // GetElement[0] - кнопка на которую нажали
                // GetElement[1] - позиция курсора во время нажатия на кнопку

                if (GetElement[0] == (ConsoleKey)HotKey.Выход) // Проврею хочет ли пользователь выйти из меню
                {
                    Console.Clear();
                    if (DopMenu == true) // проверяю хочет ли пользователь выйти из допольнительного меню
                    {
                        DopMenu = false;
                    }
                    else
                    {
                        start = false;
                    }
                }
                else if (GetElement[0] == (ConsoleKey)HotKey.Выбрать && list.Count != 0) // Проверяю хочет ли пользователь выбрать что-то в меню
                {
                    list = Read(list, GetElement);
                }
                else if (GetElement[0] == (ConsoleKey)HotKey.Создать && DopMenu == false) // Создание аккаунта нового работника
                {
                    list = Create/*<List<AccountPost>>*/(list);
                }
                else if (GetElement[0] == (ConsoleKey)HotKey.Удалить && DopMenu == false && list.Count != 0) // Удаление аккаунта
                {
                    list = Delete/*<List<AccountPost>>*/(list, GetElement);
                }
            }
        }
        private void StartMenu(AccountPost Account, List<AccountPost> list) // функция отрисовки основоного меню
        {
            // Account - аккаунт под которыим пользовотиль вошел в систему 
            // list - массив всех аккаунтов, которые может просматривать администратор
            DrewInfoAccount(Account);
            foreach (var element in list) // Отрисока информации всех аккаунтов в list 
            {
                Console.WriteLine("  ID: " + element.ID + "\t" + "Login: " + element.Login + "\t" + "Password: " + element.Password + "\t" + "Post: " + DrewPostWorker(element.Post) + "\t");
            }
        }
        /*        private T SearchElement<T>(T list)
                {
                    return T;
                }*/
        public List<AccountPost> Create(List<AccountPost> list) // Создвание нового аккаунта
        {
            AccountPost NewAccount = new AccountPost();
            start2 = true;
            Console.Clear();
            while (start2)
            {
                Console.WriteLine("Введите логин");
                var Get = CreateNewElementInfo((int)Type.String, true);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        start2 = false;
                        NewAccount.Login = Get;
                    }
                }
                else
                {
                    Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                    Console.ReadKey();
                }
                Console.Clear();
            }
            start2 = true;
            while (start2)
            {
                Console.WriteLine("Введите пароль");
                var Get = CreateNewElementInfo((int)Type.String, true);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        NewAccount.Password = Get;
                    }
                }
                else
                {
                    Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                    Console.ReadKey();
                }
                Console.Clear();
            }
            start2 = true;
            while (start2)
            {
                Console.WriteLine("Выберите роль пользователя: \n 1 - Администратор \n 2 - Кассир \n 3 - Кадровик \n 4 - Менеджер \n 5 - Бухгалтер");
                var Get = CreateNewElementInfo((int)Type.Int);
                if (Get > 0 && Get < 6)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        NewAccount.Post = Get;
                    }
                }
                else
                {
                    Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                    Console.ReadKey();
                }
                Console.Clear();
            }
            start2 = true;
            while (start2)
            {
                Console.WriteLine("Введите ID");
                dynamic Get = CreateNewElementInfo((int)Type.Int);
                if (Get >= 0)
                {
                    foreach (var element in list)
                    {
                        if (element.ID == Get)
                        {
                            Get = null;
                            break;
                        }
                    }
                    if (Get == null)
                    {
                        Console.WriteLine("Ошибка: Аккаунт с таки ID уже сущеcтвует");
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            NewAccount.ID = Get;
                        }
                    }
                }
                else if (Get < 0)
                {
                    Console.WriteLine("Ошибка: ID не может быть отрицательным");
                    Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                    Console.ReadKey();
                }
                Console.Clear();
            }
            list.Add(NewAccount);
            JsonDS.Serialize(list, "AccountPost.json");
            return list;
        }
        private AccountPost DrewInfoAboutWorker(AccountPost Account) // отрисовка дополинельной информации выбранного пользователя
        {
            // Account - аккаунт который выбрал пользователь длял орисовки подробной информации
            Console.WriteLine("  ID: " + Account.ID);
            Console.WriteLine("  Login: " + Account.Login);
            Console.WriteLine("  Password: " + Account.Password);
            Console.WriteLine("  Post: " + DrewPostWorker(Account.Post));
            Console.WriteLine("  Connection: " + Account.Connection);
            return Account;
        }

        public List<AccountPost> Delete(List<AccountPost> list, List<dynamic> GetElement)
        {
            if (list[GetElement[1] - 2].Connection == true)
            {
                List<AccountUsers> newList = JsonDS.Deserialize<List<AccountUsers>>("AccountUsers.json");
                foreach (var element in newList)
                {
                    if (Convert.ToInt32(element.ID) == list[GetElement[1] - 2].ID)
                    {
                        element.ID = null;
                        element.Post = null;
                        break;
                    }
                }
                JsonDS.Serialize(newList, "AccountUsers.json");
            }
            Console.Clear();
            list.RemoveAt(GetElement[1] - 2);
            JsonDS.Serialize(list, "AccountPost.json");
            return list;
        }

        public List<AccountPost> Read(List<AccountPost> list, List<dynamic> GetElement) // Чтение данных работника
        {
            Console.Clear();
            if (DopMenu == false) // проверяю хочет ли пользователь выбрать элемент в основном меню
            {
                DopMenu = true;
                worker = list[GetElement[1] - 2];
                PosCursorInMainMenu = GetElement[1] - 2;
            }
            else
            {
                list = Update/*<List<AccountPost>>*/(GetElement[1], list);
            }
            return list;
        }

        public List<AccountPost> Update(int PosY, List<AccountPost> list) // функция обновление данных работника
        {
            //PosY - позиция стрелки в меню
            //list - массив всех акканутов
            Console.Clear();
            Console.CursorVisible = true;
            start2 = true;
            while (start2)
            {
                if (PosY == (int)PD.ID)
                {
                    Console.WriteLine("Ввелите ID");
                    dynamic Get = CreateNewElementInfo((int)Type.Int);
                    if (Get >= 0)
                    {
                        foreach (var element in list)
                        {
                            if (element.ID == Get && element != worker)
                            {
                                Get = null;
                                break;
                            }
                        }
                        if (Get == null)
                        {
                            Console.WriteLine("Ошибка: Аккаунт с таки ID уже сущемтвует");
                            Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                            Console.ReadKey();
                        }
                        else if (worker.Connection == true)
                        {
                            List<AccountUsers> newworkers = JsonDS.Deserialize<List<AccountUsers>>("AccountUsers.json");
                            foreach (var element in newworkers)
                            {
                                if (Convert.ToInt32(element.ID) == worker.ID)
                                {
                                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                                    {
                                        start2 = false;
                                        worker.ID = Get;
                                        element.ID = Convert.ToString(Get);
                                        break;
                                    }
                                }
                            }
                            JsonDS.Serialize(newworkers, "AccountUsers.json");
                        }
                        else
                        {
                            Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                start2 = false;
                                worker.ID = Get;
                            }
                        }
                    }
                    else if (Get < 0)
                    {
                        Console.WriteLine("Ошибка: ID не может быть отрицательным");
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD.Login)
                {
                    Console.WriteLine("Введите Login акканута");
                    dynamic Get = CreateNewElementInfo((int)Type.String, true);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            worker.Login = Get;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD.Password)
                {
                    Console.WriteLine("Введите Пароль акканута");
                    dynamic Get = CreateNewElementInfo((int)Type.String, true);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            worker.Password = Get;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD.Post)
                {
                    Console.WriteLine("Выберите роль пользователя: \n 1 - Администратор \n 2 - Кассир \n 3 - Кадровик \n 4 - Менеджер \n 5 - Бухгалтер");
                    var Get = CreateNewElementInfo((int)Type.Int);
                    if (Get > 1 && Get < 6)
                    {
                        if (worker.Connection == true)
                        {
                            List<AccountUsers> newworkers = JsonDS.Deserialize<List<AccountUsers>>("AccountUsers.json");
                            foreach (var element in newworkers)
                            {
                                if (Convert.ToInt32(element.ID) == worker.ID)
                                {
                                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                                    {
                                        start2 = false;
                                        worker.Post = Get;
                                        element.Post = Convert.ToInt32(Get);
                                        break;
                                    }
                                }
                            }
                            JsonDS.Serialize(newworkers, "AccountUsers.json");
                        }
                        else
                        {
                            Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                            if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                            {
                                start2 = false;
                                worker.Post = Get;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
            }
            JsonDS.Serialize(list, "AccountPost.json");
            return list;
        }
    }
}