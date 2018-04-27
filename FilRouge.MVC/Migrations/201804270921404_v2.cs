namespace FilRouge.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserReponses", "Commentaire", c => c.String());
            AddColumn("dbo.UserReponses", "EstRepondu", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserReponses", "EstRepondu");
            DropColumn("dbo.UserReponses", "Commentaire");
        }
    }
}
