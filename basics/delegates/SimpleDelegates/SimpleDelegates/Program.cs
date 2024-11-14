using SimpleDelegates.Services;

namespace SimpleDelegates
{
    public delegate void StepChangedDelegate(int currentStep, int nextStep);
    class Program
    {
        static void Main(string[] args)
        {
            PersonHelper.TakeAWalk();
        }
    }
}
