namespace ZdflCount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "CreatorName", c => c.String());
            AddColumn("dbo.Schedules", "LastUpdatePersonID", c => c.Int(nullable: false));
            AddColumn("dbo.Schedules", "LastUpdatePersonName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "LastUpdatePersonName");
            DropColumn("dbo.Schedules", "LastUpdatePersonID");
            DropColumn("dbo.Schedules", "CreatorName");
        }
    }
}
