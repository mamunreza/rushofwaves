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
            var person = new Person("Rahi");
            var rand = new Random();

            person.StepChanged += OnStepChanging;
            person.StepChanged += OnStepChanged;
            person.StepChanged += AfterStepChaned;

            person.Step = 1;
            Console.WriteLine("\n");

            for (var i = 0; i < 4; i++)
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
            Console.WriteLine(oldStep < newStep 
                ? $"Moved {newStep - oldStep} steps forward..." 
                : $"Moved {oldStep - newStep} steps backward...");
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
        private string name;
        private int step;
        public StepChangedDelegate StepChanged;
        public Person(string name)
        {
            this.name = name;
            this.step = 0;
            Console.WriteLine($"{name} is starting the journey...\n");
        }

        public int Step
        {
            get { return this.step; }

            set
            {
                if (value != this.step)
                {
                    this.StepChanged(this.step, value);
                }

                this.step = value;
            }
        }

        public string Name { get; set; }
    }
}
