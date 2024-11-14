using SimpleDelegates.Domains;
using System;

namespace SimpleDelegates.Services
{
    public static class PersonHelper
    {
        public static void TakeAWalk()
        {
            var person = new Person("Person");
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

        private static void AfterStepChaned(int oldStep, int newStep)
        {
            if (newStep > 0)
                Console.WriteLine("Keep up the good work!");
            else if (newStep < 0)
                Console.WriteLine("Things are going wrong!");
            else
                Console.WriteLine("New beginning...");
        }

        private static void OnStepChanged(int oldStep, int newStep)
        {
            Console.WriteLine(oldStep < newStep
                ? $"Moved {newStep - oldStep} steps forward..."
                : $"Moved {oldStep - newStep} steps backward...");
        }

        private static void OnStepChanging(int oldStep, int newStep)
        {
            Console.WriteLine($"Steps changing from {oldStep} to {newStep}");
        }
    }
}