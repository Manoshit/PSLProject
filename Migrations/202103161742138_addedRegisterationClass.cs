namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRegisterationClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registerations",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserType = c.String(),
                        CreatedDate = c.DateTime(),
                        IsActive = c.String(),
                        IpAddress = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Registerations");
        }
    }
}
