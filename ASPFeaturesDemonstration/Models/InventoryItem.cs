using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPFeaturesDemonstration.Data;

namespace ASPFeaturesDemonstration.Models {
    public class InventoryItem {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string[] ImageUrl { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime DateTimeAdded { get; set; }
        [Required]
        public string UserAdded { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }
    }

    // The problem with enums is that you can never change its order unless you specify its integer
    public enum Category {
        Other = 0,
        Music,
        Art,
        Clothing,
        Textile,
        Furniture,
        Ornament,
        Tickets,
        Electrical,
        Video,
        Books,
        Automotive
    }

}
