namespace DesignPatterns.QueryObject;

public class AgeBasedQuery : ICustomerSurveyQuery
{
    private readonly int _minAge;

    public AgeBasedQuery(int minAge)
    {
        _minAge = minAge;
    }

    public IEnumerable<CustomerSurvey> Execute(IEnumerable<CustomerSurvey> surveys)
    {
        return surveys.Where(s => s.CustomerAge >= _minAge);
    }
}
