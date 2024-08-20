using Application.Interface;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.ResultTypes
{
    public static class FResult
    {
        public static  IResult  Success(dynamic doc)
        {
            return new Result() { Success = true, AttachedIsSuccess = doc };
        }
        public static IResult Success()
        {
            return new Result { Success = true };
        }
        public static IResult Failure(ArgumentException ex)
        {
            return Failure(ex.Message);
        }
        public static IResult Failure(params string[] message)
        {
            return new Result { Success = false, Errors = message };
        }
        public static IResult Failure(IEnumerable<string>? message)
        {
            return new Result { Success = false, Errors = message };
        }
        public static IResult Failure(Exception ex)
        {
            return Failure(ex.Message);
        }
        public static IResult Failure(IEnumerable<IdentityError> error)
        {
            return new Result { Success = false, Errors = error.Select(x=>x.Description).ToList() };
        }
        public static IResult NotFound(string id, string nameEntityNotFound = "entity")
        {
            var result = new List<string>
            {
                $"{nameEntityNotFound} has {id} not found"
            };
            return new Result { Success = false, StatusCode = "400", Errors = result};

        }
    }
}
