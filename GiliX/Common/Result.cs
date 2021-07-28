namespace GiliX.Common
{
    public class Result : object
    {
        public Result() : base()
        {
            _errors =
                new System.Collections.Generic.List<string>();

            _successes =
                new System.Collections.Generic.List<string>();
        }

        public bool IsFailed { get; set; }

        public bool IsSuccess { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        private readonly System.Collections.Generic.List<string> _errors;

        public System.Collections.Generic.IReadOnlyList<string> Errors => _errors;

        [System.Text.Json.Serialization.JsonIgnore]
        private readonly System.Collections.Generic.List<string> _successes;

        public System.Collections.Generic.IReadOnlyList<string> Successes => _successes;

        public void AddErrorMessage(string message)
        {
            message = message.Fix();

            if (message == null)
            {
                return;
            }

            if (_errors.Contains(message))
            {
                return;
            }

            _errors.Add(message);
        }

        public void AddSuccessMessage(string message)
        {
            message =
                message.Fix();

            if (message == null)
            {
                return;
            }

            if (_successes.Contains(message))
            {
                return;
            }

            _successes.Add(message);
        }
    }

    public class Result<T> : Result
    {
        public Result() : base()
        {
        }

        public T Value { get; set; }
    }

    public static class ResultExtensions
    {
        static ResultExtensions()
        {
        }

        public static Result ConvertToDtxResult(this FluentResults.Result result)
        {
            Result dtxResult = new()
            {
                IsFailed = result.IsFailed,
                IsSuccess = result.IsSuccess
            };

            if (result.Errors != null)
            {
                foreach (var item in result.Errors)
                {
                    dtxResult.AddErrorMessage(message: item.Message);
                }
            }

            if (result.Successes != null)
            {
                foreach (var item in result.Successes)
                {
                    dtxResult.AddSuccessMessage(message: item.Message);
                }
            }

            return dtxResult;
        }

        public static Result<T> ConvertToDtxResult<T>(this FluentResults.Result<T> result)
        {
            Result<T> dtxResult = new()
            {
                IsFailed = result.IsFailed,
                IsSuccess = result.IsSuccess
            };

            if (result.IsFailed == false)
            {
                dtxResult.Value = result.Value;
            }

            if (result.Errors != null)
            {
                foreach (var item in result.Errors)
                {
                    dtxResult.AddErrorMessage(message: item.Message);
                }
            }

            if (result.Successes != null)
            {
                foreach (var item in result.Successes)
                {
                    dtxResult.AddSuccessMessage(message: item.Message);
                }
            }

            return dtxResult;
        }
    }
}
