namespace CommunityInfoSite.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CommunityInfoSite.Models.CommunityForumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CommunityInfoSite.Models.CommunityForumContext";
        }

        protected override void Seed(CommunityForumContext context)
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

            //user manager stuff
            UserManager<Member> userManager = new UserManager<Member>(
              new UserStore<Member>(context));

            Member memberAllen = new Member { Name = "AllenB", Email = "Bertrandallen@gmail.com" };
            Member memberLee = new Member { Name = "LeeB", Email = "BertrandLee@gmail.com" };

            var resultAllen = userManager.Create(memberAllen, "passwordAllen");
            var resultLee = userManager.Create(memberLee, "passwordLee");

            context.Roles.Add(new IdentityRole() { Name = "Admin" });
            context.Roles.Add(new IdentityRole() { Name = "User" });
            context.SaveChanges();

            userManager.AddToRole(memberAllen.Id, "Admin");
            userManager.AddToRole(memberAllen.Id, "User");

            Message message1 = new Message
            {
                Subject = "Hey",
                From = memberAllen.Name,
                Date = new DateTime(2016, 08, 07),
                Body = "Welcome to the new forum!"
                //TopicID = 1
            };
            Message message2 = new Message
            {
                Subject = "Hey back.",
                From = memberLee.Name,
                Date = new DateTime(2016, 08, 07),
                Body = "Glad to be here."
                //TopicID = 1
            };
            Message message3 = new Message
            {
                Subject = "Quid pro quo",
                From = memberLee.Name,
                Date = new DateTime(2016, 08, 08),
                Body = "This forum looks like ass"
                //TopicID = 2
            };
            Message message4 = new Message
            {
                Subject = "re: Quid pro quo",
                From = memberAllen.Name,
                Date = new DateTime(2016, 08, 08),
                Body = "Yea"
                //TopicID = 2
            };

            Topic topicGeneral = new Topic
            {
                TopicName = "General Discussion",
                //TopicId = 1
            };

            Topic topicOffTopic = new Topic
            {
                TopicName = "Off-Topic",
                //TopicId = 2
            };

            //add messages to DB before adding to topic

            memberAllen.Messages.Add(message1);
            memberAllen.Messages.Add(message4);
            memberLee.Messages.Add(message2);
            memberLee.Messages.Add(message3);

            topicGeneral.Messages.Add(message1);
            topicGeneral.Messages.Add(message2);
            topicOffTopic.Messages.Add(message3);
            topicOffTopic.Messages.Add(message4);

            //context.Members.AddOrUpdate(memberAllen);
            //context.Members.AddOrUpdate(memberLee);

            //context.Topics.AddOrUpdate(topicGeneral);
            //context.Topics.AddOrUpdate(topicOffTopic);

            context.Messages.AddOrUpdate(m => m.Subject, message1, message2, message3, message4);
            context.Members.AddOrUpdate(me => me.Name, memberAllen, memberLee);
            context.Topics.AddOrUpdate(t => t.TopicName, topicGeneral, topicOffTopic);

            //base.Seed(context);
        }
    }
}
