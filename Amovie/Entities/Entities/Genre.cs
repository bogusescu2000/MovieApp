﻿namespace Entities.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
