namespace CourseGuide.Objects.Contracts
{
    public class Response
    {
        public ResponseEnum Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public void SetSuccess() => Status = ResponseEnum.Success;
        public void SetInvalid() => Status = ResponseEnum.Invalid;
        public void SetNotFound() => Status = ResponseEnum.NotFound;
        public void SetConflict() => Status = ResponseEnum.Conflict;
        public void SetUnauthorized() => Status = ResponseEnum.Unauthorized;
        public void SetError() => Status = ResponseEnum.Error;
    }
}