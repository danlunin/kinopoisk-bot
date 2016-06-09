using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FilmAdvisor;
using NUnit.Framework;
using NUnit.Framework.Api;
using TelegramBot;
using RestSharp;


namespace ControllerTests
{
    [TestFixture]
    public class TestsC
    {
        private IParser parser;
        private IRequester requester;
        private Controller controller;
        private IBot bot;
        private Question[] questions;

        [SetUp]
        public void SetUp()
        {
            parser = A.Fake<IParser>();
            requester = A.Fake<IRequester>();
            bot = A.Fake<IBot>();
            controller = new Controller(parser, requester, bot);
            this.questions = controller.GetQuestions();
            foreach (var q in questions)
            {
                q.Answer = "Test";
            }

        }

        [Test]
        public void TestGetQuestions()
        {
            this.questions = controller.GetQuestions();
            Assert.AreEqual(questions.Length, 3);

        }

        [Test]
        public void TestCheckAnswer()
        {
            questions[1].Answer = "1980";
            Assert.True(controller.CheckAnswer(questions[1]));
        }

        [Test]
        public void TestEarnFilms()
        {
            controller.EarnFilms(
                controller.GetDictionaryForRequest(questions, bot, 112, new MsgOffset()),
                4);
            A.CallTo(() => requester.Search(A<IParameters>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void TestEarnFilms2()
        {
            controller.EarnFilms(
                controller.GetDictionaryForRequest(questions, bot, 112, new MsgOffset()),
                4);
            A.CallTo(() => parser.GetFilms(A<IRestResponse>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}