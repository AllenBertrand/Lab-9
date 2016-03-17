using CommunityInfoSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.DAL
{
    public class FakeTopicsRepo : ITopicsRepo
    {
        private List<Topic> topics;
        private int maxId = 0;

        public FakeTopicsRepo()
        {
            topics = new List<Topic>();
        }

        public FakeTopicsRepo(List<Topic> t)
        {
            topics = t;
        }

        public Topic AddTopic(Topic topic)
        {
            topic.TopicId = ++maxId;
            topics.Add(topic);
            return topic;
        }

        public Topic DeleteTopicById(int id)
        {
            Topic topic = GetTopicById(id);
            topics.Remove(topic);
            return topic;
        }

        public void Dispose()
        {
            // nothing to do;
        }

        public List<Topic> GetAllTopics()
        {
            return topics;
        }

        public Topic GetTopicById(int? id)
        {
            return topics.Find(s => s.TopicId == id);
        }

        public int UpdateTopic(Topic stack)
        {
            int stackUpdated = 0;
            if (DeleteTopicById(stack.TopicId) != null)
            {
                topics.Add(stack);
                stackUpdated = 1;
            }

            return stackUpdated;
        }
    }
}