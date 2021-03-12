namespace 海面天氣預報MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatWeater : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Date = c.DateTime(),
                        Weathers = c.String(maxLength: 50),
                        WindDir = c.String(maxLength: 50),
                        WaveType = c.String(maxLength: 50),
                        WaveHeight = c.String(maxLength: 50),
                        WindSpeed = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Weathers");
        }
    }
}
