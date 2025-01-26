namespace DesignPatterns.Strategy;

public interface ICustomerSurveyFilter
{
    bool IsSatisfied(CustomerSurvey survey);
}
