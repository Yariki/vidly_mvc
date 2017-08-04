namespace vidly.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  public partial class SeedUsers : DbMigration
  {
    public override void Up()
    {
      Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c0083a4f-26cc-461a-84db-5ab917a6291c', N'iyariki.ya@gmail.com', 0, N'AH4r01f0E9h5oIRD0HI/ZJ2wd2p/YayCg66ohx62M5VgOQjcu1GnSs+0fvfhEHrhZg==', N'267e8377-6bad-42f3-8ee3-967e4a55a9a0', NULL, 0, 0, NULL, 1, 0, N'iyariki.ya@gmail.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'18c426a7-e63d-4c71-bff3-6a377534b194', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c0083a4f-26cc-461a-84db-5ab917a6291c', N'18c426a7-e63d-4c71-bff3-6a377534b194')


");
    }

    public override void Down()
    {
    }
  }
}
