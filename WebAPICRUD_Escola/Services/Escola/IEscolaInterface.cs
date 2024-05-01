using WebAPICRUD_Escola.Model;

namespace WebAPICRUD_Escola.Services.Escola
{
    public interface IEscolaInterface
    {
        Task<ServiceResponce<List<EscolaModel>>> GetEscolas();
        Task<ServiceResponce<List<EscolaModel>>> CreateEscola(EscolaModel novaEscola);
        Task<ServiceResponce<EscolaModel>> GetEscolaById(int id);
        Task<ServiceResponce<List<EscolaModel>>> UpdateEscola(EscolaModel editadoEscola);
        Task<ServiceResponce<List<EscolaModel>>> DeleteEscolaModel(int id);
        Task<bool> UploadExcel(Stream fileStream);

        ///Task<ServiceResponce<List<EscolaModel>>> InativaEscolaModel(int id);
    }
}
