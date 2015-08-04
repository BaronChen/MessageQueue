namespace MessageQueue.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompletedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "CompletedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "CompletedDateTime");
        }
    }
}
