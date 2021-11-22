namespace Papers.Common.Contract.Filters
{
    using System;
    using System.Collections.Generic;

    public class MessageFilter
    {
        public string TextContaining { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public IEnumerable<long> SenderIds { get; set; }
    }
}
