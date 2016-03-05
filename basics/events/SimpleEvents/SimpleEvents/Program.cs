using System;

namespace SimpleEvents
{
    public delegate void StepChangedDelegate(object sender, StepChangedEventArgs args);
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Adyaan");
            Random rand = new Random();

            person.StepChanged += OnStepChanging;
            person.StepChanged += OnStepChanged;
            person.StepChanged += AfterStepChaned;

            person.Step = 1;
            Console.WriteLine("\n");

            for (int i = 0; i < 4; i++)
            {
                person.Step = rand.Next(-10, 10);
                Console.WriteLine("\n");
            }
        }

        static void OnStepChanging(object sender, StepChangedEventArgs args)
        {
            Console.WriteLine($"Steps changing from {args.OldValue} to {args.NewValue}");
        }
        static void OnStepChanged(object sender, StepChangedEventArgs args)
        {
            if (args.OldValue < args.NewValue)
                Console.WriteLine($"Moved {args.NewValue - args.OldValue} steps forward...");
            else
                Console.WriteLine($"Moved {args.OldValue - args.NewValue} steps backward...");
        }
        static void AfterStepChaned(object sender, StepChangedEventArgs args)
        {
            if (args.NewValue > 0)
                Console.WriteLine("Keep up the good work!");
            else if (args.NewValue < 0)
                Console.WriteLine("Things are going wrong!");
            else
                Console.WriteLine("New beginning...");
        }
    }

    class Person
    {
        private string _name;
        private int _step;
        public event StepChangedDelegate StepChanged;
        public Person(string name)
        {
            _name = name;
            _step = 0;
            Console.WriteLine($"{name} is starting the journey...\n");
        }

        public int Step
        {
            get { return _step; }

            set
            {
                if (value != _step)
                {
                    StepChangedEventArgs args = new StepChangedEventArgs();
                    args.OldValue = _step;
                    args.NewValue = value;
                    StepChanged(this, args);
                }

                _step = value;
            }
        }

        public string Name { get; set; }
    }

    public class StepChangedEventArgs : EventArgs
    {
        public int OldValue { get; set; }
        public int NewValue { get; set; }
    }
}
