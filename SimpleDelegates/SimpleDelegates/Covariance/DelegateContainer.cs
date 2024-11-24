namespace SimpleDelegates.Covariance;

public class DelegateContainer
{
    public delegate Food FoodFactoryDel(int id, string name);

    public delegate void LogBreadDetailsDel(Bread food);
    public delegate void LogButterDetailsDel(Butter food);
}
