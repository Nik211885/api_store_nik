using Application.Common.ResultType;

namespace Application.Common.ResultTypes
{
    public class Result
    {
        private bool _success;
        private List<ResultError>? _error;
        public IEnumerable<ResultError>? Errors => _error;
        public Result(bool success, params ResultError[] errors)
        {
            _success = success;
            if(errors is not null)
            {
                _error = [];
                _error.AddRange(errors);
            }
        }
        //Failure if have any error
        public bool IsSuccess => _success; 
    }
}
