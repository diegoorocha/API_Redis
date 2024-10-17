using System.ComponentModel.DataAnnotations;

namespace API_Redis.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; } // Propriedade que representa o ID da marca.
        public string Nome { get; set; } // Propriedade que representa o nome da marca.
        public virtual ICollection<Carro> Carros { get; set; } // Coleção de carros associados a esta marca.
    }
}
