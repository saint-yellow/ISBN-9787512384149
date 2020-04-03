using System.Collections.Generic;

namespace WindowsOnly.Models.DataModels
{
    public class Category
    {
        public Category()
        {

        }

        public Category(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}