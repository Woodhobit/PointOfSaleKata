namespace POS.Application.Common
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string Error { get; }

        public Result(string error)
        {
            this.IsSuccess = false;
            this.Value = default;
            this.Error = error;
        }

        public Result(T value)
        {
            this.IsSuccess = true;
            this.Value = value;
            this.Error = null;
        }
    }
}
