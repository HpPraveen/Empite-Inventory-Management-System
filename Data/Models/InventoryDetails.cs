using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Data.Models
{
    public class InventoryDetails
    {
        [Key]
        public long ItemId { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        [Display(Name = "Item Price")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Item Qty")]
        public long ItemQty { get; set; }

        public long MerchantId { get; set; }

        [ForeignKey("MerchantId")]
        public virtual MerchantDetails MerchantDetails { get; set; }
    }
}