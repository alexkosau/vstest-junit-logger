using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AlexKosau.BuildTools.JUnitLogger.JUnitSchema;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NUnit.Framework;
using Ploeh.AutoFixture;
using TestCase = AlexKosau.BuildTools.JUnitLogger.JUnitSchema.TestCase;

namespace AlexKosau.BuildTools.JUnitLogger.Tests
{
    [TestFixture]
    public class JUnitTestLoggerTests
    {
        [Test]
        public void CanBeSerialized()
        {
            var fixture = new Fixture();
            fixture.Customize<TestSuite>(ob => ob.Without(tr => tr.SystemErrWrapped).Without(tc => tc.SystemOutWrapped));
            fixture.Customize<TestCase>(ob => ob.Without(tr => tr.SystemErrWrapped).Without(tc => tc.SystemOutWrapped));
            fixture.Customize<ErrorOrFailure>(ob => ob.Without(tr => tr.TextWrapped));
            TestRun obj = fixture.Create(new TestRun());
            var serializer = new XmlSerializer(typeof (TestRun));

            //using (var stream = File.Create("abc.xml"))
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, obj);
            }
        }

        [Test]
        public void GetClassName()
        {
            Assert.AreEqual("aa.bb.cc", JUnitTestLogger.GetClassName("aa.bb.cc.dd"));
            Assert.AreEqual("aa", JUnitTestLogger.GetClassName("aa"));
        }

        [Test]
        public void SmallConstructorWorks()
        {
            var logger = new JUnitTestLogger();
            logger.Initialize(new MyTestLoggerEvents(), "dummy");
        }

        [Test]
        public void TestResultsAreRecorded()
        {
            var events = new MyTestLoggerEvents();
            var logger = new JUnitTestLogger();
            logger.Initialize(events, new Dictionary<string, string>());

            var testCase = new Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase("TestCaseFullName",
                new Uri("executor://dummy"), "c:\\abc.dll");
            var testResult = new TestResult(testCase);
            events.FireTestResult(new TestResultEventArgs(testResult));

            Assert.AreEqual(1, logger.testCases.Count, "There should be only 1 test case now.");
        }

        [Test]
        public void TestRunCompleteIsHandled()
        {
            var events = new MyTestLoggerEvents();
            var logger = new JUnitTestLogger();
            logger.Initialize(events, new Dictionary<string, string>());

            var stats = new MyTestRunStatistics(6, 1, 2, 3);
            var e1 = new TestRunCompleteEventArgs(stats, false, false, null, null, TimeSpan.FromSeconds(1));
            events.FireTestRunComplete(e1);
        }
    }
}