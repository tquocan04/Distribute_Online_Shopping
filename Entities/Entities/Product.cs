using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        public bool IsHidden { get; set; }
        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        [JsonIgnore]
        public ICollection<BranchProduct>? BranchProducts { get; set; }
        [JsonIgnore]
        public ICollection<Item>? Items { get; set; }

        public Product() 
        {
            this.Id = Guid.NewGuid();
            this.IsHidden = false;
        }

        public void ChangeStatus()
        {
            if (this.IsHidden)
                this.IsHidden = false;
            else
                this.IsHidden = true;
        }
    }
}
