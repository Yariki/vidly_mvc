using System.ComponentModel.DataAnnotations;

namespace vidly.Models
{
  public class MembershipType
  {
    public byte Id { get; set; }

    public short SignUpFee { get; set; }

    public byte DescriptionMounths { get; set; }

    public byte Discount { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public static readonly byte Unknown = 0;
    public static readonly byte PayAsYouGo = 1;
  }
}