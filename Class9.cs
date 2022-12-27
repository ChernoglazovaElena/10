using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp2.Program;

namespace ConsoleApp2
{
    internal class Sclad : AccountPost
    {
        public virtual void DrewInterface(AccountPost Account, List<Product> list)
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
                    DrewInfoAboutProduct(worker);
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
        protected virtual void StartMenu(AccountPost Account, List<Product> list) // функция отрисовки основоного меню
        {
            // Account - аккаунт под которыим пользовотиль вошел в систему 
            // list - массив всех аккаунтов, которые может просматривать администратор
            DrewInfoAccount(Account);
            foreach (var element in list) // Отрисока информации всех аккаунтов в list 
            {
                Console.WriteLine("  ID: " + element.ID + "\t" + "Название: " + element.Title + "\t" + "Цена: " + element.Price + "\t" + "Колличество: " + element.ALotOf + "\t");
            }
        }
        protected virtual void DrewInfoAboutProduct(Product product)
        {
            Console.WriteLine("  ID: " + product.ID);
            Console.WriteLine("  Title: " + product.Title);
            Console.WriteLine("  Price: " + product.Price);
            Console.WriteLine("  A lot of: " + product.ALotOf);
        }
        private List<Product> Read(List<Product> list, List<dynamic> GetElement)
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
        private List<Product> Delete(List<Product> list, List<dynamic> GetElement)
        {
            list.RemoveAt(GetElement[1] - 2);
            JsonDS.Serialize(list, "Product.json");
            Console.Clear();
            return list;
        }
        private List<Product> Create(List<Product> list)
        {
            Product NewProduct = new Product();
            start2 = true;
            Console.CursorVisible = true;
            Console.Clear();
            while (start2)
            {
                Console.WriteLine("Введите Название товара :");
                dynamic Get = CreateNewElementInfo((int)Type.String, true, true);
                if (Get != null)
                {
                    Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        NewProduct.Title = Get;
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
                Console.WriteLine("Введите цену товара :");
                dynamic Get = CreateNewElementInfo((int)Type.Double);
                if (Get >= 0)
                {
                    Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        NewProduct.Price = Get;
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
                Console.WriteLine("Введите колличество товара :");
                dynamic Get = CreateNewElementInfo((int)Type.Int);
                if (Get > 0)
                {
                    Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                    if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                    {
                        NewProduct.ALotOf = Get;
                        start2 = false;
                    }
                }
                else if (Get <= 0)
                {
                    Console.WriteLine("Ошибка: Колличество товара не может быть отрицательной или равна 0");
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
                Console.WriteLine("Введите ID товара :");
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
                            NewProduct.ID = Get;
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
            list.Add(NewProduct);
            JsonDS.Serialize(list, "Product.json");
            return list;
        }
        private List<Product> Update(int PosY, List<Product> list)
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
                    Console.WriteLine("Введите Название товара :");
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
                    Console.WriteLine("Введите цену товара :");
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
                else if (PosY == (int)DPR.AlotOf)
                {
                    Console.WriteLine("Введите колличество товара :");
                    dynamic Get = CreateNewElementInfo((int)Type.Int);
                    if (Get > 0)
                    {
                        Console.WriteLine("Чтобы подтердить ввод нажмите Enter");
                        if (Console.ReadKey().Key == (ConsoleKey)HotKey.Выбрать)
                        {
                            worker.ALotOf = Get;
                            start2 = false;
                        }
                    }
                    else if (Get <= 0)
                    {
                        Console.WriteLine("Ошибка: Колличество товара не может быть отрицательной или равна 0");
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
            JsonDS.Serialize(list, "Product.json");
            return list;
        }
    }
}
