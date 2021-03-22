namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeHeftyChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        category = c.String(nullable: false),
                        imageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pizzaId = c.Int(),
                        name = c.String(),
                        price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sizes");
            DropTable("dbo.Pizzas");
        }
    }
}
