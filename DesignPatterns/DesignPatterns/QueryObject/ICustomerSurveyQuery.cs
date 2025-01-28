namespace DesignPatterns.QueryObject;

public interface ICustomerSurveyQuery
{
    IEnumerable<CustomerSurvey> Execute(IEnumerable<CustomerSurvey> surveys);
}
