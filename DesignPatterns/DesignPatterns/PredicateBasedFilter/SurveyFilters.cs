namespace DesignPatterns.PredicateBasedFilter;

public static class SurveyFilters
{
    public static bool IsFromNewYork(CustomerSurvey survey) => survey.CustomerLocation == "New York";
    public static bool IsOver18(CustomerSurvey survey) => survey.CustomerAge >= 18;
}