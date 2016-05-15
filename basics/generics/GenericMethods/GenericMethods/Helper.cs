namespace GenericMethods
{
    public class Helper
    {
        public static T Add<T>(T number1, T number2)
        {
            dynamic a = number1;
            dynamic b = number2;
            return a + b;
        }
    }
}
