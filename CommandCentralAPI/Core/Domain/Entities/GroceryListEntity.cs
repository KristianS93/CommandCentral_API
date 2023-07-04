// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
//
// namespace Domain.Entities;
//
// [Table("grocerylist")]
// public class GroceryListEntity
// {
//     [Key] 
//     [Column("grocerylist_id")]
//     public int GroceryListID { get; set; }
//     
//     [Required]
//     
//     [Column("household_id")]
//     [ForeignKey("household_id")]
//     public int HouseholdID { get; set; }
// }