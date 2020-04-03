using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WindowsOnly.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "commodityItems")]
        public virtual ICollection<CommodityItemViewModel> CommodityItems { get; set; }
    }
}