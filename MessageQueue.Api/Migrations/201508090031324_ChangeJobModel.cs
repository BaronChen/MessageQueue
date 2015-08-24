namespace MessageQueue.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeJobModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "RequestDateTime", c => c.DateTime());
            AlterColumn("dbo.Jobs", "CompletedDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "CompletedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jobs", "RequestDateTime", c => c.DateTime(nullable: false));
        }
    }
}
