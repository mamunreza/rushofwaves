using System;

namespace SimpleDelegates.Domains
{
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
