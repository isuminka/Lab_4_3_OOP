using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab_4_3_OOP
{
    class Library
    {
        private int InvNumber;
        private string Author;
        private string Name;
        private int AmountPages;
        private int Year;
        public Library() { }
        public Library(int invnumber, string author, string name, int amountpages, int year)
        {
            InvNumber = invnumber;
            Author = author;
            Name = name;
            AmountPages = amountpages;
            Year = year;
        }
        public int invnumber
        {
            get
            {
                return InvNumber;
            }
            set
            {
                InvNumber = value;
            }
        }
        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
        public string author
        {
            get
            {
                return Author;
            }
            set
            {
                Author = value;
            }
        }
        public int amountpages
        {
            get
            {
                return AmountPages;
            }
            set
            {
                AmountPages = value;
            }
        }
        public int year
        {
            get
            {
                return Year;
            }
            set
            {
                Year = value;
            }
        }
    }

    class Program
    {
        static void WriteDB(List<Library> books)
        {
            string textRow;
            StreamWriter newFile = new StreamWriter("output.txt");
            foreach (Library rr in books)
            {
                textRow = Convert.ToString(rr.invnumber) + ";" + rr.author + ";" + rr.name + ";" +
                    Convert.ToString(rr.amountpages) + ";" + Convert.ToString(rr.year);
                newFile.WriteLine(textRow);
            }
            newFile.Close();
        }

        static List<Library> ReadBD()
        {
            string textRow;
            string sInvNumber, pAuthor, pName, sAmountPages, sYear;
            int pInvNumber, pAmountPages, pYear;
            int i, ip;

            List<Library> books = new List<Library>();

            StreamReader file = new StreamReader("output.txt");

            while (file.Peek() >= 0)
            {
                sInvNumber = ""; pAuthor = ""; pName = ""; sAmountPages = ""; sYear = "";
                textRow = file.ReadLine();
                i = textRow.IndexOf(';') - 1;
                for (int j = 0; j <= i; j++) sInvNumber = sInvNumber + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pAuthor = pAuthor + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pName = pName + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) sAmountPages = sAmountPages + textRow[j];
                ip = i + 2;
                for (int j = ip; j <= textRow.Length - 1; j++) sYear = sYear + textRow[j];

                pInvNumber = Convert.ToInt32(sInvNumber);
                pAmountPages = Convert.ToInt32(sAmountPages);
                pYear = Convert.ToInt32(sYear);
                books.Add(new Library(pInvNumber, pAuthor, pName, pAmountPages, pYear));
            }
            file.Close();
            return books;

        }

        static void AddBook(List<Library> books)
        {
            Console.Write("Iнвертарний номер: ");
            int invNum = Convert.ToInt32(Console.ReadLine());
            Console.Write("Автор: ");
            string author = Console.ReadLine();
            Console.Write("Назва: ");
            string name = Console.ReadLine();
            Console.Write("Кiлькiсть сторiнок: ");
            int amountPages = Convert.ToInt32(Console.ReadLine());
            Console.Write("Рiк видання: ");
            int year = Convert.ToInt32(Console.ReadLine());

            books.Add(new Library(invNum, author, name, amountPages, year));
            WriteDB(books);
        }

        static void EditBook(List<Library> books)
        {
            Console.Write("Введiть iнвертарний номер книги, яку бажаєте редагувати: ");
            int invNum = Convert.ToInt32(Console.ReadLine());

            if (books.All(b => b.invnumber != invNum))
            {
                Console.WriteLine("Книги з таким iнвертарним номером не iснує!");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Оберiть параметр, який бажаєте редагувати: ");
            Console.WriteLine("Iнвертарний номер - 1");
            Console.WriteLine("Автор - 2");
            Console.WriteLine("Назва - 3");
            Console.WriteLine("Кiлькiсть сторiнок - 4");
            Console.WriteLine("Рiк видання - 5");
            Console.WriteLine("Назад - 0");

            Console.Write("Ваш вибiр: ");
            int k = Convert.ToInt32(Console.ReadLine());

            if (k == 1)
            {
                Console.Write("Новий iнвертарний номер: ");
                int newInvNum = Convert.ToInt32(Console.ReadLine());
                books.FindAll(s => s.invnumber == invNum).ForEach(x => x.invnumber = newInvNum);
                WriteDB(books);
            }
            else if (k == 2)
            {
                Console.Write("Новий автор: ");
                string newAuthor = Console.ReadLine();
                books.FindAll(s => s.invnumber == invNum).ForEach(x => x.author = newAuthor);
                WriteDB(books);
            }
            else if (k == 3)
            {
                Console.Write("Нова назва книги: ");
                string newName = Console.ReadLine();
                books.FindAll(s => s.invnumber == invNum).ForEach(x => x.name = newName);
                WriteDB(books);
            }
            else if (k == 4)
            {
                Console.Write("Нова кiлькiсть сторiнок: ");
                int newAmountPages = Convert.ToInt32(Console.ReadLine());
                books.FindAll(s => s.invnumber == invNum).ForEach(x => x.amountpages = newAmountPages);
                WriteDB(books);
            }
            else if (k == 5)
            {
                Console.Write("Новий рiк видання: ");
                int newYear = Convert.ToInt32(Console.ReadLine());
                books.FindAll(s => s.invnumber == invNum).ForEach(x => x.year = newYear);
                WriteDB(books);
            }
            else if (k == 0) return;
        }

        static void RemoveBook(List<Library> books)
        {
            Console.Write("Введiть iнвертарний номер книги, яку бажаєте видалити: ");
            int invNum = Convert.ToInt32(Console.ReadLine());
            if (books.All(b => b.invnumber != invNum))
            {
                Console.WriteLine("Книги з таким iнвертарним номером не iснує!");
                return;
            }
            var itemToDelete = books.Where(x => x.invnumber == invNum).Select(x => x).First();
            books.Remove(itemToDelete);
            WriteDB(books);
        }

        static void ShowBooks(List<Library> books)
        {
            Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
            Console.WriteLine("| Iнвертарний номер  |         Автор        |                   Назва                  | Кiлькiсть сторiнок | Рiк  |");
            Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
            foreach (Library item in books)
            {
                Console.WriteLine(String.Format("| {0,-18} | {1,-20} | {2,-40} | {3,-18} | {4,-4} |", item.invnumber, item.author, item.name, item.amountpages, item.year));
            }
            Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
        }

        static void SearchBook(List<Library> books)
        {
            Console.WriteLine("");
            Console.WriteLine("Оберiть параметр, за яким бажаєте знайти книгу: ");
            Console.WriteLine("Iнвертарний номер - 1");
            Console.WriteLine("Автор - 2");
            Console.WriteLine("Назва - 3");
            Console.WriteLine("Кiлькiсть сторiнок - 4");
            Console.WriteLine("Рiк видання - 5");
            Console.WriteLine("Назад - 0");

            Console.Write("Ваш вибiр: ");
            int k = Convert.ToInt32(Console.ReadLine());

            Library found;

            if (k == 1)
            {
                Console.Write("Iнвертарний номер: ");
                int invNum = Convert.ToInt32(Console.ReadLine());
                found = books.Find(item => item.invnumber == invNum);
                Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
                Console.WriteLine(String.Format("| {0,-18} | {1,-20} | {2,-40} | {3,-18} | {4,-4} |", found.invnumber, found.author, found.name, found.amountpages, found.year));
            }
            else if (k == 2)
            {
                Console.Write("Автор: ");
                string author = Console.ReadLine();
                found = books.Find(item => item.author == author);
                Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
                Console.WriteLine(String.Format("| {0,-18} | {1,-20} | {2,-40} | {3,-18} | {4,-4} |", found.invnumber, found.author, found.name, found.amountpages, found.year));
            }
            else if (k == 3)
            {
                Console.Write("Назва книги: ");
                string name = Console.ReadLine();
                found = books.Find(item => item.name == name);
                Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
                Console.WriteLine(String.Format("| {0,-18} | {1,-20} | {2,-40} | {3,-18} | {4,-4} |", found.invnumber, found.author, found.name, found.amountpages, found.year));
            }
            else if (k == 4)
            {
                Console.Write("Кiлькiсть сторiнок: ");
                int amountPages = Convert.ToInt32(Console.ReadLine());
                found = books.Find(item => item.amountpages == amountPages);
                Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
                Console.WriteLine(String.Format("| {0,-18} | {1,-20} | {2,-40} | {3,-18} | {4,-4} |", found.invnumber, found.author, found.name, found.amountpages, found.year));
            }
            else if (k == 5)
            {
                Console.Write("Рiк видання: ");
                int year = Convert.ToInt32(Console.ReadLine());
                found = books.Find(item => item.year == year);
                Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
                Console.WriteLine(String.Format("| {0,-18} | {1,-20} | {2,-40} | {3,-18} | {4,-4} |", found.invnumber, found.author, found.name, found.amountpages, found.year));
            }
            else if (k == 0) return;
            Console.WriteLine("+--------------------+----------------------+------------------------------------------+--------------------+------+");
        }

        static void SortBooks(List<Library> books)
        {
            Console.WriteLine("");
            Console.WriteLine("Оберiть критерiй сортування: ");
            Console.WriteLine("Iнвертарний номер - 1");
            Console.WriteLine("Автор - 2");
            Console.WriteLine("Назва - 3");
            Console.WriteLine("Кiлькiсть сторiнок - 4");
            Console.WriteLine("Рiк видання - 5");
            Console.WriteLine("Назад - 0");

            Console.Write("Ваш вибiр: ");
            int k = Convert.ToInt32(Console.ReadLine());

            if (k == 1) books.Sort((a, b) => a.invnumber.CompareTo(b.invnumber));
            else if (k == 2) books.Sort((a, b) => a.author.CompareTo(b.author));
            else if (k == 3) books.Sort((a, b) => a.name.CompareTo(b.name));
            else if (k == 4) books.Sort((a, b) => a.amountpages.CompareTo(b.amountpages));
            else if (k == 5) books.Sort((a, b) => a.year.CompareTo(b.year));
            else if (k == 0) return;

        }

        static void Main(string[] args)
        {
            List<Library> books = ReadBD();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Оберiть: ");
                Console.WriteLine("Додати запис - 1");
                Console.WriteLine("Редагувати запис - 2");
                Console.WriteLine("Видалити запис - 3");
                Console.WriteLine("Показати таблицю - 4");
                Console.WriteLine("Пошук - 5");
                Console.WriteLine("Сортування - 6");
                Console.WriteLine("Вийти - 0");

                Console.Write("Ваш вибiр: ");
                int k = Convert.ToInt32(Console.ReadLine());


                if (k == 1) AddBook(books);
                else if (k == 2) EditBook(books);
                else if (k == 3) RemoveBook(books);
                else if (k == 4) ShowBooks(books);
                else if (k == 5) SearchBook(books);
                else if (k == 6) SortBooks(books);
                else if (k == 0) break;
            }

        }
    }
}
