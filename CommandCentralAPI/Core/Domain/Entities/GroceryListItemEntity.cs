// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// namespace Domain.Entities;
// [Table("grocerylist_item")]
// public class GroceryListItemEntity
// {
//     [Key]
//     [Column("grocerylistitem_id")]
//     public int GroceryListItemID { get; set; }
//     
//     [Required]
//     [Column("grocerylist_id")]
//     [ForeignKey("grocerylist_id")]
//     public int GroceryListID { get; set; }
//     
//     [Required]
//     [Column("item_name")]
//     public string ItemName { get; set; }
//     
//     [Required]
//     [Column("item_amount")]
//     public int ItemAmount { get; set; }
// }