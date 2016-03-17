using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CommunityInfoSite.Models;
using CommunityInfoSite.DAL;
using System.Web.Mvc;
using CommunityInfoSite.Controllers;

namespace TopicsControllerTest
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void IndexTest()
        {
            //arrange
            var topics = new List<Topic>() {
                new Topic() {TopicId = 1, TopicName = "Walnuts" },
                new Topic() {TopicId = 2, TopicName = "Peanuts"},
                new Topic() {TopicId = 3, TopicName = "Mangos"}
            };

            var repo = new FakeTopicsRepo(topics);
            var target = new TopicsController(repo);

            // act

            var result = (ViewResult)target.Index();

            // assert
            var model = (List<Topic>)result.Model;
            Assert.AreEqual(model[0].TopicId, 1);
            Assert.AreEqual(model[1].TopicId, 2);
            Assert.AreEqual(model[2].TopicId, 3);
            Assert.AreEqual(model.Count, 3);
        }

        [TestMethod]
        public void DetailsTest()
        {
            //arrange
            var topics = new List<Topic>() {
                new Topic() {TopicId = 1, TopicName = "Walnuts" },
                new Topic() {TopicId = 2, TopicName = "Peanuts"},
                new Topic() {TopicId = 3, TopicName = "Mangos"}
            };

            var repo = new FakeTopicsRepo(topics);
            var target = new TopicsController(repo);

            // act
            var result = (ViewResult)target.Details(2);

            // assert
            var model = (Topic)result.Model;
            Assert.AreEqual(topics[1].TopicId, model.TopicId);
            Assert.AreEqual(topics[1].TopicName, model.TopicName);
        }
    }
}
