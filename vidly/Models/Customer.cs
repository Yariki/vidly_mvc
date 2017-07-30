using System;
using System.ComponentModel.DataAnnotations;
using vidly.Validation;

namespace vidly.Models
{
  public class Customer
  {
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public bool IsSubscribedToNewsLetter { get; set; }

    public MembershipType MembershipType { get; set; }
    
    [Display(Name = "Membership Type")]
    [Required]
    public byte MembershipTypeId { get; set; }

    [Display(Name = "Date of birth")]
    [Min18YearsIfAMember]
    public DateTime? Birthday { get; set; }
    
  }
}