namespace SalaryManagement.Application.Common.Exception
{
    public class NotFoundException : IOException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, IOException inner)
            : base(message, inner)
        {
        }
    }

}
