namespace WorldUniversityRankings.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Years", t => t.Year_Id)
                .Index(t => t.Year_Id);
            
            AddColumn("dbo.Institutions", "Location_Id", c => c.Int());
            CreateIndex("dbo.Institutions", "Location_Id");
            AddForeignKey("dbo.Institutions", "Location_Id", "dbo.Locations", "Id");
            DropColumn("dbo.Institutions", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Institutions", "Location", c => c.String());
            DropForeignKey("dbo.Locations", "Year_Id", "dbo.Years");
            DropForeignKey("dbo.Institutions", "Location_Id", "dbo.Locations");
            DropIndex("dbo.Locations", new[] { "Year_Id" });
            DropIndex("dbo.Institutions", new[] { "Location_Id" });
            DropColumn("dbo.Institutions", "Location_Id");
            DropTable("dbo.Locations");
        }
    }
}
