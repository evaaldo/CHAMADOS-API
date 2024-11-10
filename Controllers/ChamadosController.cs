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
                _logger.LogError($"ERRO AO CONSULTAR TODOS OS CHAMADOS: {ex.Message}");
                return StatusCode(500, $"ERRO AO CONSULTAR TODOS OS CHAMADOS: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateChamado([FromBody] Chamado chamado)
        {
            try
            {
                var sql = "INSERT INTO CHAMADOS (TITULO, AREA, SITUACAO, DATAABERTURA) VALUES (@TITULO, @AREA, @SITUACAO, @DATAABERTURA)";

                await _connection.ExecuteAsync(sql, new
                {
                    TITULO = chamado.TITULO,
                    AREA = chamado.AREA,
                    SITUACAO = chamado.SITUACAO,
                    DATAABERTURA = chamado.DATAABERTURA
                });

                _logger.LogInformation($"CHAMADO ABERTO COM SUCESSO: {chamado.TITULO}");

                return Ok("CHAMADO ABERTO COM SUCESSO");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERRO AO ABRIR O CHAMADO: {ex.Message}");
                return StatusCode(500, $"ERRO AO ABRIR O CHAMADO: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChamado(int id, Chamado chamado)
        {
            {
                try
                {
                    var sql = "UPDATE CHAMADOS SET TITULO = @TITULO, AREA = @AREA, SITUACAO = @SITUACAO, DATAABERTURA = @DATAABERTURA WHERE ID = @ID";

                    await _connection.ExecuteAsync(sql, new
                    {
                        ID = id,
                        TITULO = chamado.TITULO,
                        AREA = chamado.AREA,
                        SITUACAO = chamado.SITUACAO,
                        DATAABERTURA = chamado.DATAABERTURA
                    }
                    );

                    _logger.LogInformation("CHAMADO ATUALIZADO COM SUCESSO");
                    return Ok("CHAMADO ATUALIZADO COM SUCESSO");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERRO AO ATUALIZAR O CHAMADO: {ex.Message}");
                    return StatusCode(500, $"ERRO AO ATUALIZAR O CHAMADO: {ex.Message}");
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChamado(int id)
        {
            try
            {
                var sql = "DELETE FROM CHAMADOS WHERE ID = @ID";

                await _connection.ExecuteAsync(sql, new
                    {
                        ID = id,
                    }
                );

                _logger.LogInformation("CHAMADO APAGADO COM SUCESSO");
                return Ok("CHAMADO APAGADO COM SUCESSO");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERRO AO APAGAR O CHAMADO: {ex.Message}");
                return StatusCode(500, $"ERRO AO APAGAR O CHAMADO: {ex.Message}");
            }
        }
    }
}
