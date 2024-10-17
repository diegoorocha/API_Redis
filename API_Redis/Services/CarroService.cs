using API_Redis.Models;
using API_Redis.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace API_Redis.Services
{
    public class CarroService(ICarroRepository repository, IConnectionMultiplexer redis) : ICarroService
    {
        private readonly ICarroRepository _repository = repository;
        private readonly IDatabase _cache = redis.GetDatabase();

        public async Task<IEnumerable<Carro>> GetAllCarrosAsync()
        {
            string cacheKey = "Carros";
            string cachedData = await _cache.StringGetAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<Carro>>(cachedData);
            }

            var carros = await _repository.GetAllAsync();
            await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(carros), TimeSpan.FromMinutes(5)); // Cache por 5 minutos.

            return carros;
        }

        public async Task<Carro> GetCarroByIdAsync(int id)
        {
            string cacheKey = $"Carro_{id}";
            string cachedData = await _cache.StringGetAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<Carro>(cachedData);
            }

            var carro = await _repository.GetByIdAsync(id);
            if (carro != null)
            {
                await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(carro), TimeSpan.FromMinutes(5)); // Cache por 5 minutos.
            }

            return carro;
        }

        public async Task AddCarroAsync(Carro carro)
        {
            await _repository.AddAsync(carro);
            await ClearCacheAsync();
        }

        public async Task UpdateCarroAsync(Carro carro)
        {
            await _repository.UpdateAsync(carro);
            await ClearCacheAsync();
        }

        public async Task DeleteCarroAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await ClearCacheAsync();
        }

        private async Task ClearCacheAsync()
        {
            await _cache.KeyDeleteAsync("Carros");
        }
    }
}
