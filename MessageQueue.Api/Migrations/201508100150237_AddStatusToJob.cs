namespace MessageQueue.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ResultStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "ProcessStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "ProcessStatus");
            DropColumn("dbo.Jobs", "ResultStatus");
        }
    }
}
