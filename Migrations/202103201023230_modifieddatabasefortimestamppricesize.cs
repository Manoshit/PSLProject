namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifieddatabasefortimestamppricesize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "quantity", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "pizzaSize", c => c.String());
            AddColumn("dbo.Orders", "timeStamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "timeStamp");
            DropColumn("dbo.OrderDetails", "pizzaSize");
            DropColumn("dbo.OrderDetails", "quantity");
        }
    }
}
