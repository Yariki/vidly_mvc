using System;
using System.ComponentModel.DataAnnotations;
using vidly.Models;

namespace vidly.Validation
{
  public class Min18YearsIfAMember : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      var customer = validationContext.ObjectInstance as Customer;
      if (customer == null)
      {
        return new ValidationResult("Customer is required.");
      }
      if (customer.MembershipTypeId == MembershipType.Unknown
          || customer.MembershipTypeId == MembershipType.PayAsYouGo)
      {
        return ValidationResult.Success;
      }
      if (customer.Birthday == null)
      {
        return new ValidationResult("Birthday is required.");
      }

      var age = DateTime.Now.Year - customer.Birthday.Value.Year;
      return age >= 18
        ? ValidationResult.Success
        : new ValidationResult("Customer should be at least 18 years old to a membership.");
    }
  }
}