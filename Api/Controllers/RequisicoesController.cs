using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TechChallengeGSG.Service;

namespace Api.Controllers
{
    [Route("api/abre-conexaoBd")]
    [ApiController]
    public class RequisicoesController : ControllerBase
    {
        [HttpGet]
        public IActionResult AbreConexao()
        {
            SqlConnection connection = AzureConnection.OpenConnectionSql();

            if (connection != null)
            {
                return Ok("Conexão Realizada");
            }
            else
            {
                return Ok("Erro");
            }
        }
    }
}
