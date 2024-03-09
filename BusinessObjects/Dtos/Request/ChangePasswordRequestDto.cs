using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Dtos.Request;

public class ChangePasswordRequestDto
{
   public Guid UserId { get; set; }
   
   [Required]
   [DataType(DataType.Password)]
   public string OldPassword { get; set; } 
   
   [Required]
   [DataType(DataType.Password)]
   public string NewPassword { get; set; }
   
   [Required]
   [DataType(DataType.Password)]
   [Compare("NewPassword")] public string ConfirmPassword { get; set; }
}