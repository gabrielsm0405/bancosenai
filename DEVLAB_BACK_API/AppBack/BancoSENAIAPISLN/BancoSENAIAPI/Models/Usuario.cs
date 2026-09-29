using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
    }
}
