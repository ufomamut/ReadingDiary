using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ReadingDiary.Web.Models.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Zadejte název knihy.")]
        [Display(Name = "Název knihy")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zadejte autora.")]
        [Display(Name = "Autor")]
        public string Author { get; set; } = string.Empty;

        [Display(Name = "Rok vydání")]
        public int Year { get; set; }

        [Display(Name = "Popis")]
        public string? Description { get; set; }

        [Display(Name = "Recenze knihovníka")]
        public string? Review { get; set; }

        [Display(Name = "Cesta k obrázku obálky")]
        public string? CoverImagePath { get; set; }

        [Display(Name = "Vložte ISBN knihy")]
        public string? Isbn { get; set; }

        [Display(Name = "Vložte obrázek")]
        public IFormFile? CoverImageFile { get; set; }

        [Display(Name = "Žánry")]
        public List<int> SelectedGenreIds { get; set; } = new();

        public IEnumerable<SelectListItem>? AvailableGenres { get; set; }

        [Display(Name = "Datum založení")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Datum poslední úpravy")]
        public DateTime? UpdatedAt { get; set; }

        public string? CreatedAtFormatted => CreatedAt?.ToLocalTime().ToString("d.M.yyyy HH:mm");
        public string? UpdatedAtFormatted => UpdatedAt?.ToLocalTime().ToString("d.M.yyyy HH:mm");
    }
}
