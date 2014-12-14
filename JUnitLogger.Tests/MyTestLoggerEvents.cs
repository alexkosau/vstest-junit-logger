using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace AlexKosau.BuildTools.JUnitLogger.Tests
{
    public class MyTestLoggerEvents : TestLoggerEvents
    {
        public override event EventHandler<TestRunMessageEventArgs> TestRunMessage = delegate { };
        public override event EventHandler<TestResultEventArgs> TestResult = delegate { };
        public override event EventHandler<TestRunCompleteEventArgs> TestRunComplete = delegate { };

        public void FireTestRunMessaage(TestRunMessageEventArgs e)
        {
            Debug.Assert(TestRunMessage != null, "TestRunMessage != null");
            Debug.Assert(e != null, "e != null");
            TestRunMessage(this, e);
        }

        public void FireTestResult(TestResultEventArgs e)
        {
            Debug.Assert(TestResult != null, "TestResult != null");
            Debug.Assert(e != null, "e != null");
            TestResult(this, e);
        }

        public void FireTestRunComplete(TestRunCompleteEventArgs e)
        {
            Debug.Assert(TestRunComplete != null, "TestRunComplete != null");
            Debug.Assert(e != null, "e != null");
            TestRunComplete(this, e);
        }
    }

    public class MyTestRunStatistics : ITestRunStatistics
    {
        private readonly Dictionary<TestOutcome, long> _storage = new Dictionary<TestOutcome, long>();

        public MyTestRunStatistics(long executed, long skipped, long failed, long passed)
        {
            ExecutedTests = executed;
            _storage[TestOutcome.Failed] = failed;
            _storage[TestOutcome.Skipped] = skipped;
            _storage[TestOutcome.Passed] = passed;
        }

        public long this[TestOutcome testOutcome]
        {
            get { return _storage.ContainsKey(testOutcome) ? _storage[testOutcome] : 0; }
        }

        public long ExecutedTests { get; private set; }
    }
}