using System.ComponentModel.DataAnnotations;

namespace AnnuaireModel;

public class Service
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }

    public Service(int id, string name)
    {
        Id = id;
        Name = name;
        
    }
}