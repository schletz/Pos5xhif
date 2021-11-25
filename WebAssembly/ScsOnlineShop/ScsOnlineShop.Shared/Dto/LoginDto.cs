using System.ComponentModel.DataAnnotations;

namespace ScsOnlineShop.Shared.Dto
{
    public class LoginDto
    {
        public LoginDto() : this(string.Empty, string.Empty) { }
        public LoginDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Required(ErrorMessage = "Der Benutzername wurde nicht angegeben.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Der Benutzername ist ungültig.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Das Passwort wurde nicht angegeben.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Das Passwort ist ungültig.")]
        public string Password { get; set; }

    }
}
