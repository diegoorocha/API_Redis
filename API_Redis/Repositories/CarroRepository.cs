using API_Redis.Data;
using API_Redis.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Redis.Repositories
{
    public class CarroRepository(AppDbContext context) : ICarroRepository
    {
        private readonly AppDbContext _context = context;

        // Método para obter todos os carros do banco de dados.
        public async Task<IEnumerable<Carro>> GetAllAsync()
        {
            return await _context.Carros.Include(c => c.Marca).ToListAsync();
        }

        // Método para obter um carro pelo ID.
        public async Task<Carro> GetByIdAsync(int id)
        {
            return await _context.Carros.Include(c => c.Marca).FirstOrDefaultAsync(c => c.Id == id);
        }

        // Método para adicionar um novo carro no banco de dados.
        public async Task AddAsync(Carro carro)
        {
            _context.Carros.Add(carro);
            await _context.SaveChangesAsync();
        }

        // Método para atualizar um carro existente.
        public async Task UpdateAsync(Carro carro)
        {
            _context.Carros.Update(carro);
            await _context.SaveChangesAsync();
        }

        // Método para deletar um carro do banco de dados.
        public async Task DeleteAsync(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if (carro != null)
            {
                _context.Carros.Remove(carro);
                await _context.SaveChangesAsync();
            }
        }
    }
}
