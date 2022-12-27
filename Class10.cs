using System.Reflection.Metadata.Ecma335;
using static ConsoleApp2.Program;

namespace ConsoleApp2
{
    internal class Kassir : Sclad
    {
        private int ALotOfProduct;
        public void DrewInterface(AccountPost Account, List<Product> list)
        {
            // Acoount - аккаунт под которыим пользовотиль вошел в систему 
            // list - массив всех аккаунтов, которые может просматривать администратор
            while (start)
            {
                StartMenu(Account, list);
                if (DopMenu == false) // проверка  зашел ли пользователь в додполнительное меню
                {
                    PosStartCursor = 2;
                    PosEndCursor = list.Count + 1;
                }
                List<dynamic> GetElement = ArrowMenu(PosStartCursor, PosEndCursor);
                if (DopMenu == true)
                {
                    AddALotOfBuy(GetElement);
                    Console.Clear();
                }
                // GetElement[0] - кнопка на которую нажали
                // GetElement[1] - позиция курсора во время нажатия на кнопку

                if (GetElement[0] == (ConsoleKey)HotKey.Выход) // Проврею хочет ли пользователь выйти из меню
                {
                    if (DopMenu == true) // проверяю хочет ли пользователь выйти из допольнительного меню
                    {
                        DopMenu = false;
                    }
                    else
                    {
                        start = false;
                        Console.Clear();
                    }
                }
                else if (GetElement[0] == (ConsoleKey)HotKey.Выбрать && list.Count != 0) // Проверяю хочет ли пользователь выбрать что-то в меню
                {
                    if (DopMenu == false) // проверка  зашел ли пользователь в додполнительное меню
                    {
                        DopMenu = true;
                        worker = list[GetElement[1] - 2];
                        ALotOfProduct = worker.ALotOf;
                        PosStartCursor = GetElement[1];
                        PosEndCursor = GetElement[1];
                    }
                }
                else if (GetElement[0] == (ConsoleKey)HotKey.Сохранить && list.Count != 0)
                {
                    if (worker.ALotOf == 0)
                    {
                        list.Remove(worker);
                    }
                    DopMenu = false;
                    JsonDS.Serialize(list, "Product.json");
                    var list2 = JsonDS.Deserialize<List<Recording>>("Recording.json");
                    Recording rec = new Recording();
                    rec.ID = 0;
                    foreach (var element in list2)
                    {
                        if (element.ID == rec.ID)
                        {
                            rec.ID++;
                        }
                    }
                    rec.Title = "Продажа " + worker.Title;
                    rec.Price = worker.Price * worker.ALotOfBuy;
                    rec.DateTime = DateTime.Now;
                    rec.Add = true;
                    list2.Add(rec);
                    JsonDS.Serialize(list2, "Recording.json");
                }
            }
        }
        protected override void StartMenu(AccountPost Account, List<Product> list)
        {
            double a = 0;
            DrewInfoAccount(Account);
            foreach (var element in list) // Отрисока информации всех аккаунтов в list 
            {
                Console.WriteLine("  ID: " + element.ID + "\t" + "Название: " + element.Title + "\t" + "Цена: " + element.Price + "\t" + "Колличество: " + element.ALotOf + "\t" + "Купленно: " + element.ALotOfBuy);
            }
            Console.SetCursorPosition(0, list.Count + 3);
            foreach (var element in list)
            {
                if (element.ALotOfBuy > 0)
                {
                    a = element.ALotOfBuy * element.Price;
                }
            }
            Console.WriteLine("Итог: " + a);
        }
        private void AddALotOfBuy(dynamic GetElement)
        {
            if (GetElement[0] == (ConsoleKey)HotKey.Увеличить && worker.ALotOfBuy < ALotOfProduct)
            {
                worker.ALotOfBuy++;
                worker.ALotOf--;
            }
            else if (GetElement[0] == (ConsoleKey)HotKey.Убавить && worker.ALotOfBuy > 0)
            {
                worker.ALotOfBuy--;
                worker.ALotOf++;
            }
        }
    }
}