using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using WebApplicationPermissions.Interfaces;

namespace WebApplicationPermissions.Models
{
    [DataContract]
    public class PermissionType : IHasIntId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
