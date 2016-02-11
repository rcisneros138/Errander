namespace Errander.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Errands", "CompletionTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Errands", "CompletionTime", c => c.DateTime(nullable: false));
        }
    }
}
