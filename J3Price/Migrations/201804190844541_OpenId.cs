namespace WebApi_Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpenId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OpenId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "OpenId");
        }
    }
}
