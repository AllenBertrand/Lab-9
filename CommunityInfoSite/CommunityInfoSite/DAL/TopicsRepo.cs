using CommunityInfoSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.DAL
{
    public class TopicsRepo : IDisposable, ITopicsRepo
    {
        CommunityForumContext db = new CommunityForumContext();

        public List<Topic> GetAllTopics()
        {
            return db.Topics.ToList();
        }

        public Topic GetTopicById(int? id)
        {
            return db.Topics.Find(id);
        }

        public Topic AddTopic(Topic topic)
        {
            Topic dbTopic = db.Topics.Add(topic);
            db.SaveChanges();
            return dbTopic;
        }

        public int UpdateTopic(Topic topic)
        {
            db.Entry(topic).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public Topic DeleteTopicById(int id)
        {
            Topic topic = GetTopicById(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return topic;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}