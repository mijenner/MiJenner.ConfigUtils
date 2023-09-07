using System;

namespace UsageExamples
{
    public class Settings
    {
        public string MyString { get; set; } = "DefaultName";
        public int MyInt { get; set; } = 25;
        public bool MyBool { get; set; } = false;
        public double MyDouble { get; set; } = 3.1425;

        public void Print()
        {
            Console.WriteLine("MyString is " + MyString);
            Console.WriteLine("MyInt is " + MyInt);
            Console.WriteLine("MyBool is " + MyBool);
            Console.WriteLine("MyDouble is " + MyDouble);
        }
    }

}
