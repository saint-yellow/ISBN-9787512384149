using Newtonsoft.Json;

namespace WindowsOnly.Models.DataModels
{
    public class Book
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "authorId")]
        public int AuthorId { get; set; }

        [JsonProperty(PropertyName = "categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "isbn")]
        public string Isbn { get; set; }

        [JsonProperty(PropertyName = "synopsis")]
        public string Synopsis { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "listPrice")]
        public decimal ListPrice { get; set; }

        [JsonProperty(PropertyName = "salePrice")]
        public decimal SalePrice { get; set; }

        [JsonProperty(PropertyName = "featured")]
        public bool Featured { get; set; }

        [JsonProperty(PropertyName = "author")]
        public virtual Author Author { get; set; }

        [JsonProperty(PropertyName = "category")]
        public virtual Category Category { get; set; }
    }
}