using static SimpleDelegates.Covariance.DelegateContainer;

namespace SimpleDelegates.Covariance;

public class CovarianceHelper
{
    public static void ExecuteCovarianceWork()
    {
        FoodFactoryDel breadFactoryDel = FoodFactory.ReturnBread;
        Food whiteBread = breadFactoryDel(1, "White bread");
        LogBreadDetailsDel logBreadDetailsDel = LogFoodDetails;
        logBreadDetailsDel(whiteBread as Bread);

        FoodFactoryDel butterFactoryDel = FoodFactory.ReturnButter;
        Food unsaltedButter = butterFactoryDel(2, "Unsalted butter");
        LogButterDetailsDel logButterDetailsDel = LogFoodDetails;
        logButterDetailsDel(unsaltedButter as Butter);

        Console.ReadKey();
    }

    internal static void LogFoodDetails(Food food)
    {
        if (food is Bread)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Food.txt"), true))
            {
                sw.WriteLine($"Object Type: {food.GetType()}");
                sw.WriteLine($"Food Details: {food.GetFoodDetails()}");
            };

        }
        else if (food is Butter)
        {
            Console.WriteLine($"Object Type: {food.GetType()}");
            Console.WriteLine($"Food Details: {food.GetFoodDetails()}");
        }
        else
        {
            throw new ArgumentException();
        }
    }
}
