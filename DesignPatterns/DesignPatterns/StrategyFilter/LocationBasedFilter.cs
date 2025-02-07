namespace DesignPatterns.Strategy;

public class LocationBasedFilter : ICustomerSurveyFilter
{
    private readonly string _location;

    public LocationBasedFilter(string location)
    {
        _location = location;
    }

    public bool IsSatisfied(CustomerSurvey survey)
    {
        return survey.CustomerLocation == _location;
    }
}
