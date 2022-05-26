namespace Papers.Common.Exceptions
{
    using System;

    public class PapersModelException : Exception
    {
        private PapersModelException()
        {
        }

        public PapersModelException(string message) : base(message)
        {
        }
    }
}
