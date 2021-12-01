namespace Papers.Common.Contract.Exceptions
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
