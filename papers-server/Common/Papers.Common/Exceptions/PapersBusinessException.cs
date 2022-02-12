namespace Papers.Common.Exceptions
{
    using System;

    public class PapersBusinessException : Exception
    {
        private PapersBusinessException()
        {
        }

        public PapersBusinessException(string message) : base(message)
        {
        }
    }
}
