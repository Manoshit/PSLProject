namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class didSomething : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "customerId", "dbo.Registerations");
            DropIndex("dbo.Orders", new[] { "customerId" });
            RenameColumn(table: "dbo.Orders", name: "customerId", newName: "customer_UserId");
            AlterColumn("dbo.Orders", "customer_UserId", c => c.Int());
            CreateIndex("dbo.Orders", "customer_UserId");
            AddForeignKey("dbo.Orders", "customer_UserId", "dbo.Registerations", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "customer_UserId", "dbo.Registerations");
            DropIndex("dbo.Orders", new[] { "customer_UserId" });
            AlterColumn("dbo.Orders", "customer_UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "customer_UserId", newName: "customerId");
            CreateIndex("dbo.Orders", "customerId");
            AddForeignKey("dbo.Orders", "customerId", "dbo.Registerations", "UserId", cascadeDelete: true);
        }
    }
}
