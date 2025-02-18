using System.ComponentModel.DataAnnotations;

namespace FirstApp.Model
{
    public class Fruit
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Store { get; set; }

        public Fruit(string? name, int store)
        {
            Name = name;
            Store = store;
        }
    }
}
