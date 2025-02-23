using System.ComponentModel.DataAnnotations;

namespace AnnuaireModel
{

    public class Site
    {
        [Key] public int Id { get; set; }
        [Required] public string Ville { get; set; }
        public string Type { get; set; }

        public Site(int id, string ville, string type)
        {
            Id = id;
            Ville = ville;
            Type = type;
        }
        public Site(){}

    }
}