using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string NomeUsuario { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;
    }
}
