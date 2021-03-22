namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPizzaPriceForOrderDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "price");
        }
    }
}
