using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnnuaireModel;

public class Employee
{
   [Key] public int Id { get; set; }
    public string LastName { get; set; }
    [Required] public string FirstName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CellPhone { get; set; }
    [ForeignKey("Site")] public int SiteId { get; set; }
    public Site Site { get; set; }
    
    [ForeignKey ("Service")] public int ServiceId { get; set; }
    public Service Service { get; set; }

    public Employee()
    {
    }
    
    public Employee(int id, string lastName, string firsName, string email, string phone, string cellPhone, int serviceId,
        int siteId)
    {
        Id = id;
        LastName = lastName;
        FirstName = firsName;
        Email = email;
        Phone = phone;
        CellPhone = cellPhone;
        ServiceId = serviceId;
        SiteId = siteId;
    }
}