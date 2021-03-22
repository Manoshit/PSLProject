namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModelsForCheckoutFunctionality : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Registerations");
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Order_id = c.Int(),
                        Pizza_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Orders", t => t.Order_id)
                .ForeignKey("dbo.Pizzas", t => t.Pizza_id)
                .Index(t => t.Order_id)
                .Index(t => t.Pizza_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        customerId = c.String(maxLength: 128),
                        status = c.String(),
                        totalAmount = c.Double(nullable: false),
                        feedback = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Registerations", t => t.customerId)
                .Index(t => t.customerId);
            
            AlterColumn("dbo.Registerations", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Registerations", "Email", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Registerations", "Email");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Pizza_id", "dbo.Pizzas");
            DropForeignKey("dbo.OrderDetails", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "customerId", "dbo.Registerations");
            DropIndex("dbo.Orders", new[] { "customerId" });
            DropIndex("dbo.OrderDetails", new[] { "Pizza_id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_id" });
            DropPrimaryKey("dbo.Registerations");
            AlterColumn("dbo.Registerations", "Email", c => c.String());
            AlterColumn("dbo.Registerations", "UserId", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            AddPrimaryKey("dbo.Registerations", "UserId");
        }
    }
}
