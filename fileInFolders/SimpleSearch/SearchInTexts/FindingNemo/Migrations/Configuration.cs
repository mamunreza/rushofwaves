namespace FindingNemo.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using Engrams.Models;

    using FindingNemo.Models;

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
            list.Add(new Tag { Name = "Todo", Description = "Todo items" });
            list.Add(new Tag { Name = "Study", Description = "Study thngs" });
            list.Add(new Tag { Name = "Family", Description = "Family purpose" });
            list.Add(new Tag { Name = "Work", Description = "Work related facts" });
            list.Add(new Tag { Name = "Self Development", Description = "For self development" });

            return list;
        }
    }
}
