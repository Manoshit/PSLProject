namespace Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
       INSERT INTO [dbo].[AspNetUsers] ([Id], [firstName], [lastName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) 
             VALUES (N'94941f89-7b5a-4554-b232-8e99b093043b', N'Manoshit', N'Pandita', N'admin@gmail.com', 0, N'ADyCLh6A3J7cg6vaF+K7Qtu2KZ3xHlmZ0biG9Tdp1aB3enARhr/As2WssCgVFLQsVA==', N'33c2bbe3-514e-488d-9a29-31aa1fbd8feb', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')

         INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ee8e0f64-e11e-48e2-a66b-034465fbe570', N'admin')
         INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'94941f89-7b5a-4554-b232-8e99b093043b', N'ee8e0f64-e11e-48e2-a66b-034465fbe570')

             ");
        }
        
        public override void Down()
        {
        }
    }
}
