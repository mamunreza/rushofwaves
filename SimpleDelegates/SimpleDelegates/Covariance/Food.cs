namespace SimpleDelegates.Covariance;

public abstract class Food
{
    public required int Id { get; set; }
    public required string Name { get; set; }

    public virtual string GetFoodDetails()
    {
        return $"{Id} - {Name} ";
    }
}
public class Bread : Food
{
    public override string GetFoodDetails()
    {
        return $"{base.GetFoodDetails()} - Made with Flour";
    }
}
public class Butter : Food
{
    public override string GetFoodDetails()
    {
        return $"{base.GetFoodDetails()} - Made with Milk";
    }
}

