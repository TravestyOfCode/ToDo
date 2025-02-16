namespace ToDo.Data.Services;

public class Result
{
    public int StatusCode { get; set; }
    public bool WasSuccessful => StatusCode >= 200 && StatusCode <= 299;

    public Result()
    {
    }

    public Result(int statusCode) : this()
    {
        StatusCode = statusCode;
    }

    public static Result Ok() => new Result(200);
    public static BadRequestResult BadRequest() => new BadRequestResult();
    public static NotFoundResult NotFound() => new NotFoundResult();
    public static ForbiddenResult Forbidden() => new ForbiddenResult();
    public static ServerErrorResult ServerError() => new ServerErrorResult();
}

public class Result<T> : Result
{
    public T? Value { get; set; }

    public Result() : base()
    {

    }

    public static implicit operator Result<T>(T value) => new Result<T>() { StatusCode = 200, Value = value };
    public static implicit operator Result<T>(BadRequestResult result) => new Result<T> { StatusCode = 400 };
    public static implicit operator Result<T>(NotFoundResult result) => new Result<T> { StatusCode = 404 };
    public static implicit operator Result<T>(ForbiddenResult result) => new Result<T> { StatusCode = 403 };
    public static implicit operator Result<T>(ServerErrorResult result) => new Result<T> { StatusCode = 500 };
}

public class BadRequestResult : Result
{
    public BadRequestResult() : base(400) { }
}
public class NotFoundResult : Result
{
    public NotFoundResult() : base(404) { }
}
public class ForbiddenResult : Result
{
    public ForbiddenResult() : base(403) { }
}
public class ServerErrorResult : Result
{
    public ServerErrorResult() : base(500) { }
}
