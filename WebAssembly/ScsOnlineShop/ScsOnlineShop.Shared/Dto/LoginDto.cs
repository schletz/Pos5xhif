using System.ComponentModel.DataAnnotations;

namespace ScsOnlineShop.Shared.Dto
{
    public record LoginDto(
        [property: Required(ErrorMessage = "Der Benutzername wurde nicht angegeben.")]
        [property: StringLength(255, MinimumLength = 2, ErrorMessage ="Der Benutzername ist ungültig.")]
        string Username,
        [property: Required(ErrorMessage = "Das Passwort wurde nicht angegeben.")]
        [property: StringLength(255, MinimumLength = 3, ErrorMessage ="Das Passwort ist ungültig.")]
        string Password);
}
