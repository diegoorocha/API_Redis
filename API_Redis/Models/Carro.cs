using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Redis.Models
{
    public class Carro
    {
        [Key]
        public int Id { get; set; } // Propriedade que representa o ID do carro.

        [Required] // Adicionando Required para garantir que o Nome seja obrigatório
        public string Nome { get; set; } // Propriedade que representa o nome do carro.

        [Required(ErrorMessage = "O campo MarcaId é obrigatório.")] // Adicionando Required para a validação
        public int MarcaId { get; set; } // Propriedade que representa o ID da marca associada ao carro.

        [JsonIgnore] // Ignora a propriedade Marca na serialização JSON
        public virtual Marca? Marca { get; set; } // Relacionamento com a entidade Marca.
    }

}
