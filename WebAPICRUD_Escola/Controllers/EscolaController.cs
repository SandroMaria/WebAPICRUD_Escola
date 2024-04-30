using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPICRUD_Escola.Model;
using WebAPICRUD_Escola.Services.Escola;

namespace WebAPICRUD_Escola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscolaController : ControllerBase
    {
        private readonly IEscolaInterface _escolaInterface;
        public EscolaController(IEscolaInterface escolaInterface)
        {
            _escolaInterface = escolaInterface;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponce<List<EscolaModel>>>> GetEscolas()
        {
            return Ok(await _escolaInterface.GetEscolas());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponce<EscolaModel>>> GetEscolaById(int id)
        {
            ServiceResponce<EscolaModel> serviceResponse = await _escolaInterface.GetEscolaById(id);

            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponce<List<EscolaModel>>>> CreateFuncionario(EscolaModel novaEscola)
        {
            return Ok(await _escolaInterface.CreateEscola(novaEscola));
        }


        [HttpPut]
        public async Task<ActionResult<ServiceResponce<List<EscolaModel>>>> UpdateFuncionario(EscolaModel editadoEscola)
        {
            ServiceResponce<List<EscolaModel>> serviceResponse = await _escolaInterface.UpdateEscola(editadoEscola);

            return Ok(serviceResponse);
        }

        [HttpPost("upload_Excel")]
        public async Task<IActionResult> UploadExcelFile([FromForm] IFormFile file)
        {
            
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Arquivo inválido.");
            }

            try
            {
                await _escolaInterface.ProcessExcelFileAsync(file.OpenReadStream());
                return Ok("Arquivo Excel carregado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo Excel: {ex.Message}");
            }
        }


    }
}
