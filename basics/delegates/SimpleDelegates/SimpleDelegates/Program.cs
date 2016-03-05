using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDelegates
{
    public delegate void StepChangedDelegate(int currentStep, int nextStep);
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Rahi");
            Random rand = new Random();

            person.StepChanged += new StepChangedDelegate(OnStepChanging);
            person.StepChanged += new StepChangedDelegate(OnStepChanged);
            person.StepChanged += new StepChangedDelegate(AfterStepChaned);

            person.Step = 1;
            Console.WriteLine("\n");

            for (int i = 0; i < 4; i++)
            {
                person.Step = rand.Next(-10, 10);
                Console.WriteLine("\n");
            }
        }

        static void OnStepChanging(int oldStep, int newStep)
        {
            Console.WriteLine($"Steps changing from {oldStep} to {newStep}");
        }
        static void OnStepChanged(int oldStep, int newStep)
        {
            if (oldStep < newStep)
                Console.WriteLine($"Moved {newStep - oldStep} steps forward...");
            else
                Console.WriteLine($"Moved {oldStep - newStep} steps backward...");
        }
        static void AfterStepChaned(int oldStep, int newStep)
        {
            if (newStep > 0)
                Console.WriteLine("Keep up the good work!");
            else if (newStep < 0)
                Console.WriteLine("Things are going wrong!");
            else
                Console.WriteLine("New beginning...");
        }
    }

    class Person
    {
        private string _name;
        private int _step;
        public StepChangedDelegate StepChanged;
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
                    StepChanged(_step, value);
                }

                _step = value;
            }
        }

        public string Name { get; set; }
    }
}
