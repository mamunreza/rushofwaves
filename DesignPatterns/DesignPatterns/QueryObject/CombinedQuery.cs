namespace DesignPatterns.QueryObject;

public class CombinedQuery : ICustomerSurveyQuery
{
    private readonly ICustomerSurveyQuery _query1;
    private readonly ICustomerSurveyQuery _query2;

    public CombinedQuery(ICustomerSurveyQuery query1, ICustomerSurveyQuery query2)
    {
        _query1 = query1;
        _query2 = query2;
    }

    public IEnumerable<CustomerSurvey> Execute(IEnumerable<CustomerSurvey> surveys)
    {
        return _query1.Execute(surveys).Intersect(_query2.Execute(surveys));
    }
}
