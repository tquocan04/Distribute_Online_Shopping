﻿namespace Entities.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }

        public Category() 
        {
            this.Id = Guid.NewGuid();
        }
    }
}