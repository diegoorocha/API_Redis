using API_Redis.Models;
using API_Redis.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Redis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarroController(ICarroService carroService) : ControllerBase
    {
        private readonly ICarroService _carroService = carroService;

        // Método GET para obter todos os carros
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carros = await _carroService.GetAllCarrosAsync();
            return Ok(carros); // Retorna a lista de carros
        }

        // Método GET para obter um carro específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var carro = await _carroService.GetCarroByIdAsync(id);
            if (carro == null)
            {
                return NotFound(); // Retorna 404 se não encontrado
            }
            return Ok(carro); // Retorna 200 com os dados do carro
        }

        // Método POST para adicionar um novo carro
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Carro carro)
        {
            // Verifica se o modelo é válido
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Validações de modelo

            // Verifica se a MarcaId está preenchida
            if (carro.MarcaId <= 0)
            {
                ModelState.AddModelError("Marca", "O campo MarcaId é obrigatório.");
                return BadRequest(ModelState);
            }

            // Adiciona o carro no serviço
            await _carroService.AddCarroAsync(carro);

            // Retorna o status 201 com a URL do novo recurso
            return CreatedAtAction(nameof(GetById), new { id = carro.Id }, carro);
        }

        // Método PUT para atualizar um carro existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Carro carro)
        {
            if (id != carro.Id)
                return BadRequest(); // Verifica se o ID do carro corresponde ao ID fornecido na URL

            await _carroService.UpdateCarroAsync(carro);
            return NoContent(); // Retorna 204 No Content
        }

        // Método DELETE para remover um carro por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _carroService.DeleteCarroAsync(id);
            return NoContent(); // Retorna 204 No Content após a remoção
        }
    }
}
