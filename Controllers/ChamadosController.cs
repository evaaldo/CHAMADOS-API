using Dapper;
using IntegracaoChamados.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace IntegracaoChamados.Controllers
{
    [ApiController]
    [Route("/api/chamados")]
    public class ChamadosController : ControllerBase
    {
        private readonly SqlConnection _connection;
        private readonly ILogger<ChamadosController> _logger;

        public ChamadosController(SqlConnection connection, ILogger<ChamadosController> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chamado>>> GetAllChamados()
        {
            try
            {
                var sql = "SELECT * FROM CHAMADOS";

                var chamados = await _connection.QueryAsync(sql);

                _logger.LogInformation($"CONSULTA DE CHAMADOS REALIZADA COM SUCESSO");

                return Ok(chamados);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"ERRO AO CONSULTAR TODOS OS CHAMADOS: {ex.Message}");
                return StatusCode(500, $"ERRO AO CONSULTAR TODOS OS CHAMADOS: {ex.Message}");
            }
        }

    }
}
