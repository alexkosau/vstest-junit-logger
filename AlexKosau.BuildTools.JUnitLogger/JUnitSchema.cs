using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace AlexKosau.BuildTools.JUnitLogger.JUnitSchema
{
    public class ErrorOrFailure
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("message")]
        public string Message { get; set; }

        [XmlText]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlNode[] TextWrapped
        {
            get
            {
                if (Text == null) return null;

                var dummy = new XmlDocument();
                return new XmlNode[] {dummy.CreateCDataSection(Text)};
            }
            set
            {
                if (value == null)
                {
                    Text = null;
                    return;
                }

                Text = string.Join("\r\n", value.Select(v => v.Value));
            }
        }

        [XmlIgnore]
        public string Text { get; set; }
    }

    public class properties
    {
        public properties()
        {
            property = new List<property>();
        }

        public List<property> property { get; set; }
    }

    public class property
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
    }

    public class TestCase
    {
        [XmlAttribute("skipped")]
        public string Skipped { get; set; }

        [XmlElement("error")]
        public List<ErrorOrFailure> Errors { get; set; }

        [XmlElement("failure")]
        public List<ErrorOrFailure> Failures { get; set; }

        [XmlElement("system-out")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlNode[] SystemOutWrapped
        {
            get
            {
                if (SystemOut == null) return null;

                var dummy = new XmlDocument();
                return new XmlNode[] {dummy.CreateCDataSection(SystemOut)};
            }
            set
            {
                if (value == null)
                {
                    SystemOut = null;
                    return;
                }

                SystemOut = string.Join("\r\n", value.Select(v => v.Value));
            }
        }

        [XmlIgnore]
        public string SystemOut { get; set; }

        [XmlElement("system-err")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlNode[] SystemErrWrapped
        {
            get
            {
                if (SystemErr == null) return null;

                var dummy = new XmlDocument();
                return new XmlNode[] {dummy.CreateCDataSection(SystemErr)};
            }
            set
            {
                if (value == null)
                {
                    SystemErr = null;
                    return;
                }

                SystemErr = string.Join("\r\n", value.Select(v => v.Value));
            }
        }

        [XmlIgnore]
        public string SystemErr { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("assertions")]
        public string Assertions { get; set; }

        [XmlAttribute("time")]
        public double Time { get; set; }

        [XmlAttribute("classname")]
        public string Classname { get; set; }

        [XmlAttribute("status")]
        public string Status { get; set; }
    }

    public class TestSuite
    {
        public TestSuite()
        {
            TestCases = new List<TestCase>();
            Properties = new List<property>();
        }

        [XmlArray("properties")]
        [XmlArrayItem("property", IsNullable = false)]
        public List<property> Properties { get; set; }

        [XmlElement("testcase", IsNullable = false)]
        public List<TestCase> TestCases { get; set; }

        [XmlElement("system-out")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlNode[] SystemOutWrapped
        {
            get
            {
                if (SystemOut == null) return null;

                var dummy = new XmlDocument();
                return new XmlNode[] {dummy.CreateCDataSection(SystemOut)};
            }
            set
            {
                if (value == null)
                {
                    SystemOut = null;
                    return;
                }

                SystemOut = string.Join("\r\n", value.Select(v => v.Value));
            }
        }

        [XmlIgnore]
        public string SystemOut { get; set; }

        [XmlElement("system-err")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XmlNode[] SystemErrWrapped
        {
            get
            {
                if (SystemErr == null) return null;

                var dummy = new XmlDocument();
                return new XmlNode[] {dummy.CreateCDataSection(SystemErr)};
            }
            set
            {
                if (value == null)
                {
                    SystemErr = null;
                    return;
                }

                SystemErr = string.Join("\r\n", value.Select(v => v.Value));
            }
        }

        [XmlIgnore]
        public string SystemErr { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("tests")]
        public int Tests { get; set; }

        [XmlAttribute("failures")]
        public int Failures { get; set; }

        [XmlAttribute("errors")]
        public int Errors { get; set; }

        [XmlAttribute("time")]
        public double Time { get; set; }

        [XmlAttribute("disabled")]
        public int Disabled { get; set; }

        [XmlAttribute("skipped")]
        public int Skipped { get; set; }

        [XmlAttribute("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlAttribute("hostname")]
        public string Hostname { get; set; }

        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlAttribute("package")]
        public string Package { get; set; }
    }

    [XmlRoot("testsuites")]
    public class TestRun
    {
        public TestRun()
        {
            TestSuites = new List<TestSuite>();
        }

        [XmlElement("testsuite")]
        public List<TestSuite> TestSuites { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("time")]
        public double Time { get; set; }

        [XmlAttribute("tests")]
        public int Tests { get; set; }

        [XmlAttribute("failures")]
        public int Failures { get; set; }

        [XmlAttribute("disabled")]
        public int Disabled { get; set; }

        [XmlAttribute("errors")]
        public int Errors { get; set; }
    }
}