using API_Redis.Models;

namespace API_Redis.Services
{
    public interface ICarroService
    {
        Task<IEnumerable<Carro>> GetAllCarrosAsync(); // Obter todos os carros.
        Task<Carro> GetCarroByIdAsync(int id); // Obter um carro pelo ID.
        Task AddCarroAsync(Carro carro); // Adicionar um novo carro.
        Task UpdateCarroAsync(Carro carro); // Atualizar um carro existente.
        Task DeleteCarroAsync(int id); // Deletar um carro.
    }
}
