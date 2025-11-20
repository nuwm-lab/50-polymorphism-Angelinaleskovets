using System;

///////////////////////////////////////////////////////////////
//                   КЛАС ЛЮДИНА (Віртуальний)
///////////////////////////////////////////////////////////////

class Person
{
    public string FirstName = "";
    public string LastName = "";
    public string Patronymic = "";
    public int Day, Month, Year;

    public virtual void SetData()
    {
        Console.Write("Ім'я: "); FirstName = Console.ReadLine()!;
        Console.Write("Прізвище: "); LastName = Console.ReadLine()!;
        Console.Write("По-батькові: "); Patronymic = Console.ReadLine()!;

        Year = ReadInt("Рік народження: ", 1900, DateTime.Now.Year);
        Month = ReadInt("Місяць (1–12): ", 1, 12);
        Day = ReadInt($"День (1–{DateTime.DaysInMonth(Year, Month)}): ", 1, DateTime.DaysInMonth(Year, Month));
    }

    public virtual int GetAge(DateTime today)
    {
        int age = today.Year - Year;
        if (today.Month < Month || (today.Month == Month && today.Day < Day)) age--;
        return age;
    }

    public virtual int CountLetter(char letter)
    {
        char l = char.ToLower(letter);
        int count = 0;
        foreach (char c in LastName.ToLower())
            if (c == l) count++;
        return count;
    }

    protected int ReadInt(string msg, int min, int max)
    {
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out int v) &&
                v >= min && v <= max)
                return v;
            Console.WriteLine("Помилка вводу!");
        }
    }

    public override string ToString()
        => $"{FirstName} {LastName} {Patronymic} ({Day:D2}.{Month:D2}.{Year})";
}



///////////////////////////////////////////////////////////////
//                КЛАС СТУДЕНТ (Похідний)
///////////////////////////////////////////////////////////////

class Student : Person
{
    public int AdmissionYear;
    public string Specialty = "";

    public override void SetData()
    {
        base.SetData();
        AdmissionYear = ReadInt("Рік вступу: ", 1900, DateTime.Now.Year);
        Console.Write("Спеціальність: ");
        Specialty = Console.ReadLine()!;
    }

    public override string ToString()
        => base.ToString() + $", {Specialty}, вступ {AdmissionYear}";
}



///////////////////////////////////////////////////////////////
//           ВЕРСІЯ БЕЗ virtual (для порівняння)
///////////////////////////////////////////////////////////////

class PersonNV
{
    public string FirstName = "", LastName = "", Patronymic = "";
    public int Day, Month, Year;

    public void SetData()
    {
        Console.Write("Ім'я: "); FirstName = Console.ReadLine()!;
        Console.Write("Прізвище: "); LastName = Console.ReadLine()!;
        Console.Write("По-батькові: "); Patronymic = Console.ReadLine()!;

        Year = ReadInt("Рік народження: ", 1900, DateTime.Now.Year);
        Month = ReadInt("Місяць (1–12): ", 1, 12);
        Day = ReadInt($"День (1–{DateTime.DaysInMonth(Year, Month)}): ", 1, DateTime.DaysInMonth(Year, Month));
    }

    public int GetAge(DateTime today)
    {
        int age = today.Year - Year;
        if (today.Month < Month || (today.Month == Month && today.Day < Day)) age--;
        return age;
    }

    public int CountLetter(char letter)
    {
        char l = char.ToLower(letter);
        int count = 0;
        foreach (char c in LastName.ToLower())
            if (c == l) count++;
        return count;
    }

    protected int ReadInt(string msg, int min, int max)
    {
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out int v) &&
                v >= min && v <= max)
                return v;
            Console.WriteLine("Помилка вводу!");
        }
    }

    public override string ToString()
        => $"{FirstName} {LastName} {Patronymic} ({Day:D2}.{Month:D2}.{Year})";
}


class StudentNV : PersonNV
{
    public int AdmissionYear;
    public string Specialty = "";

    public new void SetData()
    {
        base.SetData();
  AdmissionYear = ReadInt("Рік вступу: ", 1900, DateTime.Now.Year);
        Console.Write("Спеціальність: ");
        Specialty = Console.ReadLine()!;
    }

    public override string ToString()
        => base.ToString() + $", {Specialty}, вступ {AdmissionYear}";
}



///////////////////////////////////////////////////////////////
//                        MAIN
///////////////////////////////////////////////////////////////

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Введення даних (віртуальні методи) ===");
        Student st = new Student();
        st.SetData();

        Person p = st;

        Console.WriteLine("\nДані студента:");
        Console.WriteLine(p);

        Console.WriteLine("Вік: " + p.GetAge(DateTime.Now));

        Console.Write("Введіть літеру для пошуку в прізвищі: ");
        char letter = Console.ReadLine()![0];

        Console.WriteLine("Кількість входжень: " + p.CountLetter(letter));



        // Порівняння з не-віртуальними
        Console.WriteLine("\n=== Порівняння без virtual ===");

        StudentNV st2 = new StudentNV();
        st2.SetData();
        PersonNV pn = st2;

        Console.WriteLine("\nДані студента (NV):");
        Console.WriteLine(pn);
        Console.WriteLine("Вік: " + pn.GetAge(DateTime.Now));

        Console.Write("Літера: ");
        char l2 = Console.ReadLine()![0];
        Console.WriteLine("Кількість входжень: " + pn.CountLetter(l2));
    }
}      
