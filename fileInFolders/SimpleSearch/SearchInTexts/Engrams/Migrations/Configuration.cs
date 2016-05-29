namespace Engrams.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Engrams.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EngramsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EngramsContext context)
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

            context.Tags.AddRange(GetTags());
        }

        public static List<Tag> GetTags()
        {
            var list = new List<Tag>();

            list.Add(new Tag { Name = "General", Description = "All general things" });
            list.Add(new Tag { Name = "Knowledge", Description = "Knowledge things" });

            return list;
        }
    }
}
