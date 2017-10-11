namespace ZdflCount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        OrderNumber = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                        CreatorID = c.Int(nullable: false),
                        ProductCount = c.Int(nullable: false),
                        FinishCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Orders",
                c => new
                {
                    OrderId = c.Int(nullable: false, identity: true),
                    OrderNumber = c.String(),
                    Status = c.Int(nullable: false),
                    DateCreate = c.String(),
                    DateAssign = c.String(),
                    AssignedCount = c.Int(nullable: false),
                    ProductCount = c.Int(nullable: false),
                    ProductAssignedCount = c.Int(nullable: false),
                    ProductFinishedCount = c.Int(nullable: false),
                    ProductWorkingCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.OrderId);
        }
        
        public override void Down()
        {
            
            DropTable("dbo.Schedules");
        }
    }
}
