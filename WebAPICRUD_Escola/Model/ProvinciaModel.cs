using System.ComponentModel.DataAnnotations;

namespace WebAPICRUD_Escola.Model
{
    public class ProvinciaModel
    {
        [Key]
        public int id { get; set; }

        public int Nome { get; set; }
    }
}
