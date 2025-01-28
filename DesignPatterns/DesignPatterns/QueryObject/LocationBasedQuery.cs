namespace DesignPatterns.QueryObject;

public class LocationBasedQuery : ICustomerSurveyQuery
{
    private readonly string _location;

    public LocationBasedQuery(string location)
    {
        _location = location;
    }

    public IEnumerable<CustomerSurvey> Execute(IEnumerable<CustomerSurvey> surveys)
    {
        return surveys.Where(s => s.CustomerLocation == _location);
    }
}
