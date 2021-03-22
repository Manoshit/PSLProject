namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCartModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        customerId = c.Int(nullable: false),
                        pizzaId = c.Int(nullable: false),
                        pizzaName = c.String(),
                        pizzaPrice = c.Double(nullable: false),
                        pizzaSize = c.String(),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Carts");
        }
    }
}
