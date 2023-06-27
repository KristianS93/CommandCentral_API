using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandCentralAPI.dbmodels;

[Table("household")]
public class DbHousehold
{
    [Key, Required]
    public int id { get; set; }
}