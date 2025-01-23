namespace BigBlog.Exceptions
{
    public class ErrorException : Exception
    {
        public ErrorException(string ex) : base($"Возникла ошибка: {ex}") { }
    }
}
