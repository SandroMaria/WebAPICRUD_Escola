using Microsoft.EntityFrameworkCore;
using WebAPICRUD_Escola.DataContext;
using WebAPICRUD_Escola.Model;
using OfficeOpenXml;


namespace WebAPICRUD_Escola.Services.Escola
{
    public class EscolaService : IEscolaInterface
    {

        private readonly ApplicationDbContaxt _context;

        public EscolaService(ApplicationDbContaxt context)
        {
            _context = context;
        }


        public async Task<ServiceResponce<List<EscolaModel>>> CreateEscola(EscolaModel novaEscola)
        {
            ServiceResponce<List<EscolaModel>> serviceResponse = new ServiceResponce<List<EscolaModel>>();

            try
            {
                if (novaEscola  == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }
                _context.Add(novaEscola);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Escolas.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public Task<ServiceResponce<List<EscolaModel>>> DeleteEscola(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponce<List<EscolaModel>>> DeleteEscolaModel(int id)
        {
            ServiceResponce<List<EscolaModel>> serviceResponse = new ServiceResponce<List<EscolaModel>>();

            try
            {
                EscolaModel escola = _context.Escolas.FirstOrDefault(x => x.id == id);

                if (escola == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Escola não localizado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }


                _context.Escolas.Remove(escola);
                await _context.SaveChangesAsync();


                serviceResponse.Dados = _context.Escolas.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponce<EscolaModel>> GetEscolaById(int id)
        {
            ServiceResponce<EscolaModel> serviceResponse = new ServiceResponce<EscolaModel>();

            try
            {
                EscolaModel escola = _context.Escolas.FirstOrDefault(x => x.id == id);

                if (escola == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Escola não localizado!";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = escola;

            }
            catch (Exception ex)
            {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public  async Task<ServiceResponce<List<EscolaModel>>> GetEscolas()
        {
            ServiceResponce<List<EscolaModel>> serviceResponse = new ServiceResponce<List<EscolaModel>>();

            try
            {
                serviceResponse.Dados = _context.Escolas.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                }
            }
            catch (Exception ex)
            {

                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

  


        public async Task ProcessExcelFileAsync(Stream fileStream)
        {

            fileStream = fileStream;
            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets[0];


            int rowCount = worksheet.Dimension.Rows;


            for (int row = 2; row <= rowCount; row++) // Ignorando a primeira linha de cabeçalho
            {
                // Ler os dados de cada linha e inserir na base de dados
                var nome = worksheet.Cells[row, 1].GetValue<string>();
                var email = worksheet.Cells[row, 2].GetValue<string>();
                var numeroSalas = worksheet.Cells[row, 3].GetValue<int>();
                var provincia = worksheet.Cells[row, 4].GetValue<string>();

                var escola = new EscolaModel
                {
                    Nome = nome,
                    Email = email,
                    NumSalas = numeroSalas,
                    Provincia = provincia
                };

                _context.Escolas.Add(escola);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ServiceResponce<List<EscolaModel>>> UpdateEscola(EscolaModel editadoEscola)
        {
            ServiceResponce<List<EscolaModel>> serviceResponse = new ServiceResponce<List<EscolaModel>>();

            try
            {
                EscolaModel escola = _context.Escolas.AsNoTracking().FirstOrDefault(x => x.id == editadoEscola.id);

                if (escola == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Escola não localizado!";
                    serviceResponse.Sucesso = false;
                }

                _context.Escolas.Update(editadoEscola);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Escolas.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<bool> UploadExcel(Stream fileStream)
        {
            try
            {
                using var package = new ExcelPackage(fileStream);
                var worksheet = package.Workbook.Worksheets.FirstOrDefault(); // Obtenha a primeira planilha
                if (worksheet == null)
                    throw new Exception("Nenhuma planilha encontrada no arquivo.");

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Ignora o cabeçalho na primeira linha
                {
                    var escola = new EscolaModel
                    {
                        Nome = worksheet.Cells[row, 1].Value?.ToString(),
                        Email = worksheet.Cells[row, 2].Value?.ToString(),
                        Provincia = worksheet.Cells[row, 3].Value?.ToString(),
                        NumSalas = Convert.ToInt32(worksheet.Cells[row, 4].Value)
                    };

                    _context.Escolas.Add(escola);
                }

                await _context.SaveChangesAsync();

                return true; // Retorna verdadeiro se o carregamento for bem-sucedido
            }
            catch (Exception ex)
            {
                // Logue o erro ou trate conforme necessário
                return false; // Retorna falso se ocorrer um erro durante o carregamento
            }
        }
    }
}
