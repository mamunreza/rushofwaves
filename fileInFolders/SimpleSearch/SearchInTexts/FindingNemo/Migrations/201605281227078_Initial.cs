namespace FindingNemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        LinkId = c.Int(nullable: false, identity: true),
                        LinkText = c.String(nullable: false, maxLength: 500),
                        MemoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LinkId)
                .ForeignKey("dbo.Memories", t => t.MemoryId)
                .Index(t => t.MemoryId);
            
            CreateTable(
                "dbo.Memories",
                c => new
                    {
                        MemoryId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Note = c.String(nullable: false, maxLength: 255),
                        TagId = c.Int(nullable: false),
                        ParentId = c.Int(),
                        CreateData = c.DateTime(nullable: false),
                        EditDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemoryId)
                .ForeignKey("dbo.Memories", t => t.ParentId)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.TagId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "MemoryId", "dbo.Memories");
            DropForeignKey("dbo.Memories", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Memories", "ParentId", "dbo.Memories");
            DropIndex("dbo.Memories", new[] { "ParentId" });
            DropIndex("dbo.Memories", new[] { "TagId" });
            DropIndex("dbo.Links", new[] { "MemoryId" });
            DropTable("dbo.Tags");
            DropTable("dbo.Memories");
            DropTable("dbo.Links");
        }
    }
}
