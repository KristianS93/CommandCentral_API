using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommandCentralAPI.dbmodels;

[Table("grocery_list")]
public class DbGroceryList
{
    [Key, Required] public int id { get; set; }
    public DbHousehold household_ { get; set; }
}
