﻿namespace Papers.Data.Contract.Models.Content
{
    public abstract class Content
    {
        public abstract long Id { get; set; }

        public abstract byte Type { get; set; }
    }
}
