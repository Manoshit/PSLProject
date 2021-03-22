namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedModelsForDeliveryExecAndSeparateTableForMaintainingQuantity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PizzaStocks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pizzaId = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Orders", "DeliveryExecutive_UserId", c => c.Int());
            AlterColumn("dbo.Registerations", "IsActive", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Orders", "DeliveryExecutive_UserId");
            AddForeignKey("dbo.Orders", "DeliveryExecutive_UserId", "dbo.Registerations", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DeliveryExecutive_UserId", "dbo.Registerations");
            DropIndex("dbo.Orders", new[] { "DeliveryExecutive_UserId" });
            AlterColumn("dbo.Registerations", "IsActive", c => c.String());
            DropColumn("dbo.Orders", "DeliveryExecutive_UserId");
            DropTable("dbo.PizzaStocks");
        }
    }
}
