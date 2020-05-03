using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobieStoreWeb.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Custommer")]
        public string CustummerId { get; set; }

        public virtual ApplicationUser Custummer { get; set; }      

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Shipping Name")]
        public string ShippingName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Shipping Address")]

        public string ShippingAddress { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Status")]
        public OrderStatus Status { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


    }
    public enum OrderStatus
    {
        Processed = 1,
        Cancelled = 0,
        Continue =2,
        Paid=100,
        Transfered=10,
    }
}
