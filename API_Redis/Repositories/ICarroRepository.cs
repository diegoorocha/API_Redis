using API_Redis.Models;

namespace API_Redis.Repositories
{
    public interface ICarroRepository
    {
        Task<IEnumerable<Carro>> GetAllAsync(); // Método para obter todos os carros.
        Task<Carro> GetByIdAsync(int id); // Método para obter um carro pelo ID.
        Task AddAsync(Carro carro); // Método para adicionar um novo carro.
        Task UpdateAsync(Carro carro); // Método para atualizar um carro existente.
        Task DeleteAsync(int id); // Método para deletar um carro.
    }
}
