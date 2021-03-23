namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFieldForContactNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registerations", "ContactNumber", c => c.Long(nullable: false));
            DropColumn("dbo.Registerations", "IpAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Registerations", "IpAddress", c => c.String());
            DropColumn("dbo.Registerations", "ContactNumber");
        }
    }
}
