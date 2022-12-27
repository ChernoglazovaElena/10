using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.Program;

namespace ConsoleApp2
{
    internal class Buh : Sclad
    {
        public virtual void DrewInterface(AccountPost Account, List<Recording> list)
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
                    DrewInfoAboutRecording(worker);
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
                    list = Create(list);
                }
                else if (GetElement[0] == (ConsoleKey)HotKey.Удалить && DopMenu == false && list.Count != 0) // Удаление аккаунта
                {
                    list = Delete(list, GetElement);
                }
            }
        }
        private void StartMenu(AccountPost Account, List<Recording> list) // функция отрисовки основоного меню
        {
            double a = 0;
            // Account - аккаунт под которыим пользовотиль вошел в систему 
            // list - массив всех аккаунтов, которые может просматривать администратор
            DrewInfoAccount(Account);
            foreach (var element in list) // Отрисока информации всех аккаунтов в list 
            {
                Console.WriteLine("  ID: " + element.ID + "  Название: " + element.Title + "  Цена: " + element.Price + "  Время: " + element.DateTime + "  Прибавка?: " + element.Add + "\t");
            }
            foreach (var element in list)
            {
                if (element.Add == true)
                {
                    a += element.Price;
                }
                else
                {
                    a -= element.Price;
                }
            }
            Console.SetCursorPosition(0, list.Count + 3);
            Console.WriteLine("Итог:" + a);
        }
        private void DrewInfoAboutRecording(Recording product)
        {
            Console.WriteLine("  ID: " + product.ID);
            Console.WriteLine("  Title: " + product.Title);
            Console.WriteLine("  Price: " + product.Price);
            Console.WriteLine("  Add: " + product.Add);
        }
        private List<Recording> Read(List<Recording> list, List<dynamic> GetElement)
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
        private List<Recording> Delete(List<Recording> list, List<dynamic> GetElement)
        {
            list.RemoveAt(GetElement[1] - 2);
            JsonDS.Serialize(list, "Recording.json");
            Console.Clear();
            return list;
        }
        private List<Recording> Create(List<Recording> list)
        {
            Recording NewRecording = new Recording();
            NewRecording.DateTime = DateTime.Now;
            start2 = true;
            Console.CursorVisible = true;
            Console.Clear();
            while (start2)
            {
                Console.WriteLine("Введите название  :");
                dynamic Get = CreateNewElementInfo((int)Type.String, false, true);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        NewRecording.Title = Get;
                        start2 = false;
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
                Console.WriteLine("Введите цену  :");
                dynamic Get = CreateNewElementInfo((int)Type.Double);
                if (Get >= 0)
                {
                    Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        NewRecording.Price = Get;
                        start2 = false;
                    }
                }
                else if (Get < 0)
                {
                    Console.WriteLine("Ошибка: Цена не может быть отрицательной");
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
            start2 = true;
            while (start2)
            {
                Console.WriteLine("Прибавка ?(1 - да, 0 - нет) :");
                dynamic Get = CreateNewElementInfo((int)Type.Int);
                if (Get == 0)
                {
                    Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        NewRecording.Add = false;
                        start2 = false;
                    }
                }
                else if (Get == 1)
                {
                    Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        NewRecording.Add = true;
                        start2 = false;
                    }
                }
                else if (Get > 1 || Get < 0)
                {
                    Console.WriteLine("Ошибка: Введите 0 или 1");
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
            start2 = true;
            while (start2)
            {
                Console.WriteLine("Введите ID :");
                dynamic Get = CreateNewElementInfo((int)Type.Int);
                if (Get >= 0)
                {
                    foreach (var element in list)
                    {
                        if (element.ID == Get && element != worker)
                        {
                            Console.WriteLine("Ошибка: Продукт с таким ID уже существует");
                            Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                            Console.ReadKey();
                            start2 = false;
                            break;
                        }
                    }
                    if (start2)
                    {
                        Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            NewRecording.ID = Get;
                            start2 = false;
                        }
                    }
                    else
                    {
                        start2 = true;
                    }
                }
                else if (Get < 0)
                {
                    Console.WriteLine("Ошибка: ID не может быть отрицательной");
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
            list.Add(NewRecording);
            JsonDS.Serialize(list, "Recording.json");
            return list;
        }
        private List<Recording> Update(int PosY, List<Recording> list)
        {
            Console.CursorVisible = false;
            start2 = true;
            while (start2)
            {
                if (PosY == (int)DPR.ID)
                {
                    Console.WriteLine("Введите ID:");
                    dynamic Get = CreateNewElementInfo((int)Type.Int);
                    if (Get >= 0)
                    {
                        foreach (var element in list)
                        {
                            if (element.ID == Get && element != worker)
                            {
                                start2 = false;
                                break;
                            }
                        }
                        if (!start2)
                        {
                            Console.WriteLine("Ошибка: Продукт с таким ID уже существует");
                            Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                            Console.ReadKey();
                            start2 = true;
                        }
                        else
                        {
                            Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                            if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                            {
                                worker.ID = Get;
                                start2 = false;
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
                else if (PosY == (int)DPR.Title)
                {
                    Console.WriteLine("Введите название :");
                    dynamic Get = CreateNewElementInfo((int)Type.String, true, true);
                    if (Get != null)
                    {
                        Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            worker.Title = Get;
                            start2 = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)DPR.Price)
                {
                    Console.WriteLine("Введите цену :");
                    dynamic Get = CreateNewElementInfo((int)Type.Double);
                    if (Get >= 0)
                    {
                        Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            worker.Price = Get;
                            start2 = false;
                        }
                    }
                    else if (Get < 0)
                    {
                        Console.WriteLine("Ошибка: Цена не может быть отрицательной");
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Чтобы повторить ввод нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else if (PosY == (int)DPR.Add)
                {
                    Console.WriteLine("Прибавка ?(1 - да, 0 - нет) :");
                    dynamic Get = CreateNewElementInfo((int)Type.Int);
                    if (Get == 0)
                    {
                        Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            worker.Add = false;
                            start2 = false;
                        }
                    }
                    else if (Get == 1)
                    {
                        Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            worker.Add = true;
                            start2 = false;
                        }
                    }
                    else if (Get < 0 || Get > 1)
                    {
                        Console.WriteLine("Ошибка: Введите 0 или 1");
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
            JsonDS.Serialize(list, "Recording.json");
            return list;
        }
    }
}

