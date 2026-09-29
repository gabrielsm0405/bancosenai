using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Dtos
{
    public class RegisterRequestDto
    {
        [Required]
        public string NomeUsuario { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = string.Empty;
    }
}
