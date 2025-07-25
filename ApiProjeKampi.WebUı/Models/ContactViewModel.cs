using System.ComponentModel.DataAnnotations;

namespace ApiProjeKampi.WebUı.Models
{
    public class ContactViewModel
    {

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu alanı zorunludur.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj alanı zorunludur.")]
        public string Message { get; set; }
    }
}
