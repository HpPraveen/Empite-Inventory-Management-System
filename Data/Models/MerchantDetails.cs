using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Data.Models
{
    public class MerchantDetails
    {
        [Key]
        public long MerchantId { get; set; }

        public string MerchantName { get; set; }
        public string MerchantEmail { get; set; }
        public string MerchantAddress { get; set; }

        public ICollection<InventoryDetails> InventoryDetails { get; set; }
    }
}