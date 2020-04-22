using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MobieStoreWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(256, ErrorMessage = "{0} must be less than {1} character.")]
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public short CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Display(Name = "Brand")]
        public short BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        [Display(Name = "Decription")]
        public string Decription { get; set; }
        [Range(0, double.PositiveInfinity, ErrorMessage = "{0} must be greater than or equals {1}.")]
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Status")]
        public ProductStatus Status { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
    }

    public enum ProductStatus:byte {
        OutOfSock,
        Available,
        CommingSoon,
    }
}
