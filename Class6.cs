using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using static ConsoleApp2.Program;

namespace ConsoleApp2
{
    internal class Manager : AccountPost
    {
        public virtual void DrewInterface(AccountPost Account, List<AccountUsers> list)
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
                    PosStartCursor = (int)PD2.Name;
                    PosEndCursor = (int)PD2.Connect;
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
                else if (GetElement[0] == (ConsoleKey)HotKey.Сохранить && DopMenu == false) // Создание аккаунта нового работника
                {
                    list = Create(list);
                }
                else if (GetElement[0] == (ConsoleKey)HotKey.Удалить && DopMenu == false && list.Count != 0) // Удаление аккаунта
                {
                    list = Delete(list, GetElement);
                }
            }
        }
        private void StartMenu(AccountPost Account, List<AccountUsers> list) // функция отрисовки основоного меню
        {
            // Account - аккаунт под которыим пользовотиль вошел в систему 
            // list - массив всех аккаунтов, которые может просматривать администратор
            DrewInfoAccount(Account);
            foreach (var element in list) // Отрисока информации всех аккаунтов в list 
            {
                Console.Write(element.ID == null ? "  ID: " + "null" : "  ID: " + element.ID);
                Console.WriteLine(" Фамилия: " + element.Surname + " Имя: " + element.Name + " Отчество: " + element.Otchestvo + " Должность: " + DrewPostWorker(Convert.ToInt32(element.Post)) + "\t");
            }
        }
        private void DrewInfoAboutWorker(AccountUsers Account)
        {
            Console.WriteLine(Account.ID == null ? "  ID: " + "null" : "  ID: " + Account.ID);
            Console.WriteLine("  Имя: " + Account.Name);
            Console.WriteLine("  Фамилия: " + Account.Surname);
            Console.WriteLine("  Отчество: " + Account.Otchestvo);
            Console.WriteLine("  Серия и номер паспорта: " + Account.SeriaNomerPasspota);
            Console.WriteLine("  Дата рождения: " + Account.DateBirth);
            Console.WriteLine("  Зароботная плата: " + Account.ZP);
            Console.WriteLine("  Соединение с другим аккаунтом: " + Account.Connect);
            Console.WriteLine(Account.Post == null ? "  Post: " + "null" : "  Post: " + DrewPostWorker(Convert.ToInt32(Account.Post)));
        }
        protected List<AccountUsers> Read(List<AccountUsers> list, List<dynamic> GetElement)
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
                list = Update(GetElement[1], list);
            }
            return list;
        }
        private List<AccountUsers> Delete(List<AccountUsers> list, List<dynamic> GetElement)
        {
            if (list[GetElement[1] - 2].Connect == true)
            {
                List<AccountPost> newList = JsonDS.Deserialize<List<AccountPost>>("AccountPost.json");
                foreach (var element in newList)
                {
                    if (element.ID == Convert.ToInt32(list[GetElement[1] - 2].ID))
                    {
                        element.Connection = false;
                        element.Nickname = null;
                        break;
                    }
                }
                JsonDS.Serialize(newList, "AccountPost.json");
            }
            Console.Clear();
            list.RemoveAt(GetElement[1] - 2);
            JsonDS.Serialize(list, "AccountUsers.json");
            return list;
        }
        private List<AccountUsers> Update(int PosY, List<AccountUsers> list)
        {
            Console.Clear();
            Console.CursorVisible = true;
            start2 = true;
            while (start2)
            {
                if (PosY == (int)PD2.Name)
                {
                    Console.WriteLine("Введите имя: ");
                    dynamic Get = CreateNewElementInfo((int)Type.String);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            worker.Name = Get;
                            List<AccountPost> list13 = JsonDS.Deserialize<List<AccountPost>>("AccountPost.json");
                            foreach (var element in list13)
                            {
                                if (element.ID == Convert.ToInt32(worker.ID))
                                {
                                    element.Nickname = worker.Name;
                                    break;
                                }
                            }
                            JsonDS.Serialize(list13, "AccountPost.json");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD2.Surname)
                {
                    Console.WriteLine("Введите фамилию: ");
                    dynamic Get = CreateNewElementInfo((int)Type.String);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            worker.Surname = Get;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD2.Otchestvo)
                {
                    Console.WriteLine("Введите отчество: ");
                    dynamic Get = CreateNewElementInfo((int)Type.String);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            worker.Otchestvo = Get;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD2.SeriaNomerPasspota)
                {
                    Console.WriteLine("Введите отчество: ");
                    dynamic Get = CreateNewElementInfo((int)Type.Int);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            worker.SSeriaNomerPasspotae = Get;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD2.DateOfBirth)
                {
                    DateTime dateTime;
                    string input = "";

                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                        input = Console.ReadLine();
                    }
                    while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dateTime));

                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        worker.DateBirth = input;
                    }
                }
                else if (PosY == (int)PD2.ZP)
                {
                    Console.WriteLine("Введите заработную плату: ");
                    dynamic Get = CreateNewElementInfo((int)Type.Int);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            start2 = false;
                            worker.ZP = Get;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)PD2.Connect)
                {
                    Console.WriteLine("Чтобы отвязать аккаунт введите 0 или 1, чтобы перепривязать");
                    dynamic Get = CreateNewElementInfo((int)Type.Int);
                    if (Get == 0)
                    {
                        List<AccountPost> list13 = JsonDS.Deserialize<List<AccountPost>>("AccountPost.json");
                        foreach (var element in list13)
                        {
                            if (element.ID == Convert.ToInt32(worker.ID))
                            {
                                element.Connection = false;
                                element.Nickname = null;
                                worker.ID = null;
                                worker.Connect = false;
                                worker.Post = null;
                                start2 = false;
                                break;
                            }
                        }
                        JsonDS.Serialize(list13, "AccountPost.json");
                    }
                    else if (Get == 1)
                    {
                        List<AccountPost> list13 = JsonDS.Deserialize<List<AccountPost>>("AccountPost.json");
                        foreach (var element in list13)
                        {
                            if (element.ID == Convert.ToString(worker.ID))
                            {
                                element.Connection = false;
                                element.Nickname = null;
                                worker.ID = null;
                                worker.Connection = false;
                                worker.Post = null;
                                break;
                            }
                        }
                        Console.WriteLine("Введите ID аккаунта для подключения");
                        Get = CreateNewElementInfo((int)Type.Int);
                        if (Get >= 0)
                        {
                            foreach (var element in list13)
                            {
                                if (element.ID == Get && element.Connection == false)
                                {
                                    element.Nickname = worker.Name;
                                    element.Connection = true;
                                    worker.Connect = true;
                                    worker.ID = Convert.ToString(element.ID);
                                    worker.Post = Convert.ToString(element.Post);
                                    start2 = false;
                                    JsonDS.Serialize(list13, "AccountPost.json");
                                    break;
                                }
                            }
                            if (start2 == false)
                            {
                                Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                                if (Console.ReadKey().Key != (ConsoleKey)HotKey.Выбрать)
                                {
                                    start2 = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ошибка: Аккаунта с таким ID несуществует");
                                Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                                Console.ReadKey();
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
                Console.Clear();
            }
            JsonDS.Serialize(list, "AccountUsers.json");
            return list;
        }
        public List<AccountUsers> Create(List<AccountUsers> list)
        {
            Console.Clear();
            AccountUsers NewAccount = new AccountUsers();
            start2 = true;
            while (start2)
            {
                Console.WriteLine("Введите имя");
                var Get = CreateNewElementInfo((int)Type.String);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        NewAccount.Name = Get;
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
                Console.WriteLine("Введите Фамилию");
                var Get = CreateNewElementInfo((int)Type.String);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        NewAccount.Surname = Get;
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
                Console.WriteLine("Введите Отчество");
                var Get = CreateNewElementInfo((int)Type.String, true);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        NewAccount.Otchestvo = Get;
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
                Console.WriteLine("Введите cерию и номер паспорта");
                var Get = CreateNewElementInfo((int)Type.Int);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        NewAccount.SeriaNomerPasspota = Get;
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
                Console.WriteLine("Введите Зарплату");
                var Get = CreateNewElementInfo((int)Type.Double);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        start2 = false;
                        NewAccount.ZP = Get;
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
                DateTime dateTime;
                string input = "";

                do
                {
                    Console.Clear();
                    Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                    input = Console.ReadLine();
                }
                while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dateTime));

                Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                {
                    start2 = false;
                    NewAccount.DateBirth = input;
                }
                Console.Clear();
            }
            start2 = true;
            while (start2)
            {
                Console.WriteLine("Чтобы подключить пользователя к аккаунту введите Enter, или  Escape,чтобы выйти");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    List<AccountPost> list13 = JsonDS.Deserialize<List<AccountPost>>("AccountPost.json");
                    Console.WriteLine("Введите ID аккаунта для подключения");
                    var Get = CreateNewElementInfo((int)Type.Int);
                    if (Get >= 0)
                    {
                        foreach (var element in list13)
                        {
                            if (element.ID == Get && element.Connection == false)
                            {
                                element.Nickname = NewAccount.Name;
                                element.Connection = true;
                                NewAccount.Connect = true;
                                NewAccount.ID = Convert.ToString(element.ID);
                                NewAccount.Post = Convert.ToString(element.Post);
                                start2 = false;
                                JsonDS.Serialize(list13, "AccountPost.json");
                                break;
                            }
                        }
                        if (start2 == false)
                        {
                            Console.WriteLine("Чтобы подтвердить свой выбор нажмите Enter");
                            if (Console.ReadKey().Key != (ConsoleKey)HotKey.Выбрать)
                            {
                                start2 = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Аккаунта с таким ID несуществует");
                            Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                            Console.ReadKey();
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
                else if (key.Key == (ConsoleKey)HotKey.Выход)
                {
                    start2 = false;
                }
                Console.Clear();
            }
            list.Add(NewAccount);
            JsonDS.Serialize(list, "AccountUsers.json");
            return list;
        }
    }
}
