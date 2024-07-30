namespace Application.Common.ResultType
{
    public class ResultError
    {
        //Name Error is unique
        public string NameError { get; }
        public string DescriptionError { get; }
        public ResultError(string nameError, string descriptionError)
        {
            NameError = nameError;
            DescriptionError = descriptionError;
        }
    }
}
