namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedDataTypeForTimeStamp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "timeStamp", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "timeStamp", c => c.DateTime(nullable: false));
        }
    }
}
