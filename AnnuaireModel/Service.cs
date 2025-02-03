using System.ComponentModel.DataAnnotations;

namespace AnnuaireModel
{

    public class Service
    {
        [Key] public int Id { get; set; }
        [Required] public string ServiceName { get; set; }

        public Service(int id, string serviceName)
        {
            Id = id;
            ServiceName = serviceName;

        }
    }
}