namespace Application.Common.ResultTypes
{
    public class FResult
    {
        public static Result Success()
        {
            return new Result { Success = true };
        }
        public static Result Failure(params string[] message)
        {
            return new Result { Success = false, Errors = message };
        }
        public static Result NotFound(string id, string nameEntityNotFound = "entity")
        {
            var result = new List<string>
            {
                $"{nameEntityNotFound} has {id} not found"
            };
            return new Result { Success = false, StatusCode = "400", Errors = result};

        }
    }
}
