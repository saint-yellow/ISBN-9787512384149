using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsOnly.Models.DataModels
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Index(IsUnique=true)]
        [StringLength(255)]
        public string SessionId { get; set; }

        public virtual ICollection<CommodityItem> CommodityItems { get; set; }
    }
}
