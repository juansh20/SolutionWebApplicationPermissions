using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApplicationPermissions.Interfaces;

namespace WebApplicationPermissions.Models
{
    [DataContract]
    public class Permission : IHasIntId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermiso { get; set; }
        public DateTime FechaPermiso { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
