using CommunityInfoSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommunityInfoSite.DAL
{
    public interface ITopicsRepo
    {
        Topic AddTopic(Topic topic);
        Topic DeleteTopicById(int id);
        void Dispose();
        List<Topic> GetAllTopics();
        Topic GetTopicById(int? id);
        int UpdateTopic(Topic topic);
    }
}