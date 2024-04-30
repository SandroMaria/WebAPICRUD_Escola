using System.ComponentModel.DataAnnotations;

namespace WebAPICRUD_Escola.Model
{
    public class EscolaModel
    {
        [Key]
        public int id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Provincia { get; set; }
        public int NumSalas { get; set;}
    }
}
