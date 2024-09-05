namespace Benchmark.Calculator.Application.Features
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }

        public string? Errors { get; set; }
    }
}
