namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullvalue11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "NumberOfEmployees", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "NumberOfEmployees", c => c.Int(nullable: false));
        }
    }
}
