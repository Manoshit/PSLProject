namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedDataTypeOfCustomerId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Carts", "customerId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carts", "customerId", c => c.Int(nullable: false));
        }
    }
}
