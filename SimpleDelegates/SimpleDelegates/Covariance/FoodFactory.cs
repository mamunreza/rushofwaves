namespace SimpleDelegates.Covariance;

public static class FoodFactory
{
    public static Bread ReturnBread(int id, string name)
    {
        return new Bread { Id = id, Name = name };
    }
    public static Butter ReturnButter(int id, string name)
    {
        return new Butter { Id = id, Name = name };
    }
}
