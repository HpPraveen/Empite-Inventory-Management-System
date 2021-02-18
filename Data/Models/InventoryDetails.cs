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

        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemQty { get; set; }

        public long MerchantId { get; set; }

        [ForeignKey("MerchantId")]
        public virtual MerchantDetails MerchantDetails { get; set; }
    }
}