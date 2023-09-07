using System;

namespace MSTestProject
{
    public class NestedSettings
    {
        public string Message { get; set; } = string.Empty;

        public void Print()
        {
            if (!string.IsNullOrEmpty(Message))
            {
                Console.WriteLine("In NestedSettings, Message is " + Message);
            }
            else
            {
                Console.WriteLine("In NestedSettings, Message is null or empty");
            }
        }
    }
}
