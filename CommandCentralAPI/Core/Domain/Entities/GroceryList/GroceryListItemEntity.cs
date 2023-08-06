using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities.GroceryList;
public class GroceryListItemEntity : BaseEntity
{
    public string ItemName { get; set; } = String.Empty;
    
    public int ItemAmount { get; set; }
    
    public int GroceryListId { get; set; }
    
    public GroceryListEntity? GroceryList { get; set; }
}