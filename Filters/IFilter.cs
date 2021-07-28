namespace Logger
{
    public interface IFilter
    {
        string executeScript(string fieldType, string fieldValue);
    }
}
