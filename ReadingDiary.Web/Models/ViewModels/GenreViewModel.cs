using System.ComponentModel.DataAnnotations;

namespace ReadingDiary.Web.Models.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Název žánru je povinný.")]
        [StringLength(50, ErrorMessage = "Maximální délka názvu je 50 znaků.")]
        [Display(Name = "Název žánru")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Popis může mít maximálně 200 znaků.")]
        [Display(Name = "Popis žánru")]
        public string? Description { get; set; }
    }
}
