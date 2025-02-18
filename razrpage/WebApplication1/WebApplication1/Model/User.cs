using System.ComponentModel.DataAnnotations;
namespace RazorPagesExample.Models
{
    public class User
    {
        [Required]
        [Display(Name = "You name")]
        public string? UserName { get; set; }
    }
}