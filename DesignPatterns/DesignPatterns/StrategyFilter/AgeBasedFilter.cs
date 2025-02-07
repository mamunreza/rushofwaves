namespace DesignPatterns.Strategy;

public class AgeBasedFilter : ICustomerSurveyFilter
{
    private readonly int _minAge;

    public AgeBasedFilter(int minAge)
    {
        _minAge = minAge;
    }

    public bool IsSatisfied(CustomerSurvey survey)
    {
        return survey.CustomerAge >= _minAge;
    }
}
