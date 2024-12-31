using Entities.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DTOs.DTOs
{
    public class OrderCartDTO
    {
        //public Guid Id { get; set; }
        public ICollection<ItemDTO>? Items { get; set; }
        public decimal TotalPrice { get; set; } = 0;
    }
}
