namespace FilRouge.MVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FilRouge.MVC.Entities;
    using FilRouge.MVC.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<FilRouge.MVC.Entities.FilRougeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FilRouge.MVC.Entities.FilRougeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //insertion des roles
            InsertRole(context);

            //insertion des types de questions
            InsertTypeQuestion(context);
        }

        private void InsertRole(FilRougeDBContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole()
            {
                Name = "Admin"
            });

            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole()
            {
                Name = "Agent"
            });
        }

        private void InsertTypeQuestion(FilRougeDBContext context)
        {
            //saisie des 4 types
            //---saisie libre
            context.TypeQuestion.AddOrUpdate(new TypeQuestion()
            {
                TypeQuestionId = (int)AnswerTypeEnum.SaisieLibre,
                NameType = AnswerTypeEnum.SaisieLibre.ToString()
            });
            //---choix unique
            context.TypeQuestion.AddOrUpdate(new TypeQuestion()
            {
                TypeQuestionId = (int)AnswerTypeEnum.ChoixUnique,
                NameType = AnswerTypeEnum.ChoixUnique.ToString()
            });
            //---choix multiple
            context.TypeQuestion.AddOrUpdate(new TypeQuestion()
            {
                TypeQuestionId = (int)AnswerTypeEnum.ChoixMultiple,
                NameType = AnswerTypeEnum.ChoixMultiple.ToString()
            });
            //---saisie code
            context.TypeQuestion.AddOrUpdate(new TypeQuestion()
            {
                TypeQuestionId = (int)AnswerTypeEnum.SaisieCode,
                NameType = AnswerTypeEnum.SaisieCode.ToString()
            });
        }

    }
}
