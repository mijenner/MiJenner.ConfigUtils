using System;

namespace MSTestProject
{
    public class Settings2
    {
        public int KeyOne { get; set; } = 0;
        public bool KeyTwo { get; set; } = true; 
        public NestedSettings KeyThree { get; set; } = new NestedSettings();

        public void Print()
        {
            Console.WriteLine("KeyOne is " + KeyOne);
            Console.WriteLine("KeyTwo is " + KeyTwo);
            if (KeyThree != null)
            {
                KeyThree.Print();
            }
            else
            {
                Console.WriteLine("KeyThree is null");
            }
        }
    }
}
