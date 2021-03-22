namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droppedPrimaryKeyFromEmailToId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "customerId", "dbo.Registerations");
            DropIndex("dbo.Orders", new[] { "customerId" });
            DropPrimaryKey("dbo.Registerations");
            AlterColumn("dbo.Orders", "customerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Registerations", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Registerations", "UserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Registerations", "UserId");
            CreateIndex("dbo.Orders", "customerId");
            AddForeignKey("dbo.Orders", "customerId", "dbo.Registerations", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "customerId", "dbo.Registerations");
            DropIndex("dbo.Orders", new[] { "customerId" });
            DropPrimaryKey("dbo.Registerations");
            AlterColumn("dbo.Registerations", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Registerations", "Email", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Orders", "customerId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Registerations", "Email");
            CreateIndex("dbo.Orders", "customerId");
            AddForeignKey("dbo.Orders", "customerId", "dbo.Registerations", "Email");
        }
    }
}
