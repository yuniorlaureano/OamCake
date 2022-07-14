﻿namespace OamCake.Entity
{
    public class Catalog : BaseEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime PublishDate { get; set; }
    }
}