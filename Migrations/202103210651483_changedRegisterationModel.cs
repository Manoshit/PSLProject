namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRegisterationModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registerations", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Registerations", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Registerations", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Registerations", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Registerations", "LastName", c => c.String());
            AlterColumn("dbo.Registerations", "FirstName", c => c.String());
            AlterColumn("dbo.Registerations", "Password", c => c.String());
            DropColumn("dbo.Registerations", "Address");
        }
    }
}
