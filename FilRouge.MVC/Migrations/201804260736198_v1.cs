namespace FilRouge.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Difficulties",
                c => new
                    {
                        DifficultyId = c.Int(nullable: false, identity: true),
                        DifficultyName = c.String(),
                    })
                .PrimaryKey(t => t.DifficultyId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Commentaire = c.String(),
                        Active = c.Boolean(nullable: false),
                        QuestionTypeId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                        DifficultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Difficulties", t => t.DifficultyId)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId)
                .ForeignKey("dbo.TypeQuestions", t => t.QuestionTypeId)
                .Index(t => t.QuestionTypeId)
                .Index(t => t.TechnologyId)
                .Index(t => t.DifficultyId);
            
            CreateTable(
                "dbo.Quizzs",
                c => new
                    {
                        QuizzId = c.Int(nullable: false, identity: true),
                        Timer = c.Int(nullable: false),
                        EtatQuizz = c.Int(nullable: false),
                        NomUser = c.String(),
                        PrenomUser = c.String(),
                        QuestionLibre = c.Boolean(nullable: false),
                        NombreQuestion = c.Int(nullable: false),
                        DifficultyId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                        ContactId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.QuizzId)
                .ForeignKey("dbo.AspNetUsers", t => t.ContactId)
                .ForeignKey("dbo.Difficulties", t => t.DifficultyId)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId)
                .Index(t => t.DifficultyId)
                .Index(t => t.TechnologyId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnoId = c.Int(nullable: false, identity: true),
                        TechnoName = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TechnoId);
            
            CreateTable(
                "dbo.Reponses",
                c => new
                    {
                        ReponseId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        TrueReponse = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReponseId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.TypeQuestions",
                c => new
                    {
                        TypeQuestionId = c.Int(nullable: false, identity: true),
                        NameType = c.String(),
                    })
                .PrimaryKey(t => t.TypeQuestionId);
            
            CreateTable(
                "dbo.DifficultyRates",
                c => new
                    {
                        DifficultyMasterId = c.Int(nullable: false),
                        DifficultyId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.DifficultyMasterId, t.DifficultyId })
                .ForeignKey("dbo.Difficulties", t => t.DifficultyId)
                .ForeignKey("dbo.Difficulties", t => t.DifficultyMasterId)
                .Index(t => t.DifficultyMasterId)
                .Index(t => t.DifficultyId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserReponses",
                c => new
                    {
                        QuizzId = c.Int(nullable: false),
                        ReponseId = c.Int(nullable: false),
                        Valeur = c.String(),
                    })
                .PrimaryKey(t => new { t.QuizzId, t.ReponseId })
                .ForeignKey("dbo.Quizzs", t => t.QuizzId)
                .ForeignKey("dbo.Reponses", t => t.ReponseId)
                .Index(t => t.QuizzId)
                .Index(t => t.ReponseId);
            
            CreateTable(
                "dbo.QuizzQuestions",
                c => new
                    {
                        Quizz_QuizzId = c.Int(nullable: false),
                        Questions_QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quizz_QuizzId, t.Questions_QuestionId })
                .ForeignKey("dbo.Quizzs", t => t.Quizz_QuizzId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Questions_QuestionId, cascadeDelete: true)
                .Index(t => t.Quizz_QuizzId)
                .Index(t => t.Questions_QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserReponses", "ReponseId", "dbo.Reponses");
            DropForeignKey("dbo.UserReponses", "QuizzId", "dbo.Quizzs");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DifficultyRates", "DifficultyMasterId", "dbo.Difficulties");
            DropForeignKey("dbo.DifficultyRates", "DifficultyId", "dbo.Difficulties");
            DropForeignKey("dbo.Questions", "QuestionTypeId", "dbo.TypeQuestions");
            DropForeignKey("dbo.Questions", "TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.Reponses", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Quizzs", "TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.QuizzQuestions", "Questions_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuizzQuestions", "Quizz_QuizzId", "dbo.Quizzs");
            DropForeignKey("dbo.Quizzs", "DifficultyId", "dbo.Difficulties");
            DropForeignKey("dbo.Quizzs", "ContactId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "DifficultyId", "dbo.Difficulties");
            DropIndex("dbo.QuizzQuestions", new[] { "Questions_QuestionId" });
            DropIndex("dbo.QuizzQuestions", new[] { "Quizz_QuizzId" });
            DropIndex("dbo.UserReponses", new[] { "ReponseId" });
            DropIndex("dbo.UserReponses", new[] { "QuizzId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.DifficultyRates", new[] { "DifficultyId" });
            DropIndex("dbo.DifficultyRates", new[] { "DifficultyMasterId" });
            DropIndex("dbo.Reponses", new[] { "QuestionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Quizzs", new[] { "ContactId" });
            DropIndex("dbo.Quizzs", new[] { "TechnologyId" });
            DropIndex("dbo.Quizzs", new[] { "DifficultyId" });
            DropIndex("dbo.Questions", new[] { "DifficultyId" });
            DropIndex("dbo.Questions", new[] { "TechnologyId" });
            DropIndex("dbo.Questions", new[] { "QuestionTypeId" });
            DropTable("dbo.QuizzQuestions");
            DropTable("dbo.UserReponses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DifficultyRates");
            DropTable("dbo.TypeQuestions");
            DropTable("dbo.Reponses");
            DropTable("dbo.Technologies");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Quizzs");
            DropTable("dbo.Questions");
            DropTable("dbo.Difficulties");
        }
    }
}
