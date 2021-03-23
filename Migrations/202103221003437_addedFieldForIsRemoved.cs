namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFieldForIsRemoved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Registerations", "IsRemoved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Registerations", "IsRemoved");
        }
    }
}
