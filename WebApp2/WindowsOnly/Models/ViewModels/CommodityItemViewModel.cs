using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WindowsOnly.Models.ViewModels
{
    public class CommodityItemViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "shoppingCartId")]
        public int ShoppingCartId { get; set; }

        [JsonProperty(PropertyName = "bookId")]
        public int BookId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "book")]
        public virtual BookViewModel Book { get; set; }
    }
}