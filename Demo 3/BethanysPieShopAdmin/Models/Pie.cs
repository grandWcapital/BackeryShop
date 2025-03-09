using System.ComponentModel.DataAnnotations;

namespace BethanysPieShopAdmin.Models
{
    public class Pie
    {
        public int PieId { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name= "Shoer Description")]
        public string? ShortDescription { get; set; }

        [StringLength(1000)]
        [Display(Name = "Long Description")]
        public string? LongDescription { get; set; }

        [StringLength(1000)]
        [Display(Name = "Allergy Information")]
        public string? AllergyInformation { get; set; }

        [Display(Name= "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Image Thumbnail URL")]
        public string? ImageThumbnailUrl { get; set; }

        [Display(Name = "Is Pie Of The Week")]
        public bool IsPieOfTheWeek { get; set; }

        [Display(Name = "Price")]
        public bool InStock { get; set; }

        [Display(Name = "Price")]
        public int CategoryId { get; set; }

        [Display(Name = "Price")]
        public Category? Category { get; set; }

        [Display(Name = "Price")]
        public ICollection<Ingredient>? Ingredients { get; set; }
    }
}
