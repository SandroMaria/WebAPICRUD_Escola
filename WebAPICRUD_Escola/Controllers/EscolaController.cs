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



        [HttpDelete]
        public async Task<ActionResult<ServiceResponce<List<EscolaModel>>>> DeleteEscola(int id)
        {
            ServiceResponce<List<EscolaModel>> serviceResponse = await _escolaInterface.DeleteEscolaModel(id);

            return Ok(serviceResponse);

        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadExcel([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            if (Path.GetExtension(file.FileName) != ".xlsx")
                return BadRequest("O arquivo deve ser do tipo Excel (.xlsx).");

            var success = await _escolaInterface.UploadExcel(file.OpenReadStream());

            if (success)
                return Ok("Dados carregados com sucesso.");
            else
                return StatusCode(500, "Erro ao carregar os dados.");
        }


    }
}
