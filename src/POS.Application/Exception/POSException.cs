namespace POS.Application.Exception
{
    public class POSException : System.Exception
    {
        public POSException()
        {
        }

        public POSException(string message)
            : base(message)
        {
        }
    }
}
