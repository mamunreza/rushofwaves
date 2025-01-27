namespace DesignPatterns.PredicateBasedFilter;

public class CustomerSurvey
{
    public int CustomerAge { get; set; }
    public string CustomerLocation { get; set; }

    public override string ToString()
    {
        return $"Age: {CustomerAge}, Location: {CustomerLocation}";
    }
}