namespace vidly.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class PopulateMembershipType : DbMigration
  {
    public override void Up()
    {
      Sql("INSERT INTO MembershipTypes (Id,SignUpFee,DescriptionMounths,Discount,Name) VALUES(1,0,0,0,'Pay as you GO')");
      Sql("INSERT INTO MembershipTypes (Id,SignUpFee,DescriptionMounths,Discount,Name) VALUES(2,30,1,10,'Monthly - 1')");
      Sql("INSERT INTO MembershipTypes (Id,SignUpFee,DescriptionMounths,Discount,Name) VALUES(3,90,3,15,'Monthly - 3')");
      Sql("INSERT INTO MembershipTypes (Id,SignUpFee,DescriptionMounths,Discount,Name) VALUES(4,300,12,20,'Annualy')");
    }

    public override void Down()
    {
    }
  }
}