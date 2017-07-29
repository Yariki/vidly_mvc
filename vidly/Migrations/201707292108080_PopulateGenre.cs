namespace vidly.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class PopulateGenre : DbMigration
  {
    public override void Up()
    {
      Sql("INSERT INTO Genres(Name) VALUES('Comedy')");
      Sql("INSERT INTO Genres(Name) VALUES('Action')");
      Sql("INSERT INTO Genres(Name) VALUES('Drama')");
      Sql("INSERT INTO Genres(Name) VALUES('Fantasy')");
    }

    public override void Down()
    {
    }
  }
}
