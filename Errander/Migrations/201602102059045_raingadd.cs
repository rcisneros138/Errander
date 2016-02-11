namespace Errander.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class raingadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Errands", "Rating", c => c.Double(nullable: false));
            AddColumn("dbo.Errands", "CompletionTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Errands", "CompletionTime");
            DropColumn("dbo.Errands", "Rating");
        }
    }
}
