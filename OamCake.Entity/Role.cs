﻿namespace OamCake.Entity
{
    public class Role : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }
    }
}
