using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api")]
[Authorize]
public class AnimeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AnimeController> _logger;

    public AnimeController(ApplicationDbContext context, ILogger<AnimeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Pegar Animes
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="diretor"></param>
    /// <param name="nome"></param>
    /// <param name="palavrasChave"></param>
    /// <returns>Apresenta animes</returns>
    [HttpGet("animes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string diretor = null,
        [FromQuery] string nome = null,
        [FromQuery] string palavrasChave = null)
    {
        try
        {
            IQueryable<Anime> query = _context.Animes.AsNoTracking();

            if (!string.IsNullOrEmpty(diretor))
            {
                query = query.Where(a => a.Diretor != null && a.Diretor.Contains(diretor));
            }

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(a => a.Name != null && a.Name.Contains(nome));
            }

            if (!string.IsNullOrEmpty(palavrasChave))
            {
                query = query.Where(a => a.Resumo != null && a.Resumo.Contains(palavrasChave));
            }

            var totalItems = await query.CountAsync();

            var animes = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var response = new
            {
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Animes = animes
            };

            _logger.LogInformation("Consulta de animes bem-sucedida");

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar animes");
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }

    }
    /// <summary>
    /// Pegar anime por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Apresentar anime por id</returns>
    [HttpGet("animes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var anime = await _context.Animes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (anime == null)
        {
            _logger.LogInformation("Consulta por Id não encontrada: {AnimeId}", id);
            return NotFound();
        }
        else
        {
            _logger.LogInformation("Consulta por Id bem-sucedida: {AnimeId}", id);
            return Ok(anime);
        }
    }

    /// <summary>
    /// Criar Anime
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Anime Criado</returns>
    [HttpPost("criarAnime")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] Anime model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var anime = new Anime
        {
            Name = model.Name,
            Resumo = model.Resumo,
            Diretor = model.Diretor,
        };

        try
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Anime criado com sucesso: {AnimeId}", anime.Id);
            
            return Created($"api/CriarAnime/{anime.Id}", anime);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Erro ao criar anime");

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Editar Anime
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns>Anime editado</returns>
    [HttpPut("editarAnime/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int id, [FromBody] Anime model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var anime = await _context.Animes.FirstOrDefaultAsync(x => x.Id == id);

        if (anime == null)
            return NotFound();

        try
        {
            anime.Name = model.Name;
            anime.Resumo = model.Resumo;
            anime.Diretor = model.Diretor;

            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Anime editado com sucesso: {AnimeId}", id);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao editar anime: {AnimeId}", id);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Deletar anime
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Anime deletado</returns>
    [HttpDelete("deletarAnimes/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var anime = await _context.Animes.FirstOrDefaultAsync(x => x.Id == id);

        if (anime == null)
            return NotFound();

        try
        {
            _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Anime deletado com sucesso: {AnimeId}", id);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar anime: {AnimeId}", id);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}


