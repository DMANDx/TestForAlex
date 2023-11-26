using System.ComponentModel.DataAnnotations;

namespace Avto1Test.Models
{
    public class Url
    {
        public int Id { get; set; }
        
        [Required]
        [Url(ErrorMessage = "Некорректный URL")]        
        [Display(Name = "URL")]
        [StringLength(255, ErrorMessage = "Прквышена длина")]
        public string? MainURL { get; set; }

        [StringLength(255, MinimumLength = 9, ErrorMessage = "Минимум 9 символов")]
        [Display(Name = "сокращенный URL")]
        public string? TinyURL { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата создания")]
        public DateTime DateCreate { get; set; }

        [Display(Name = "кол-во кликов")]
        public int NumOfCall { get; set; }
    }
}