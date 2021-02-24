using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public interface ITestEnv
    {
        void TestResult(ITestCase testCase, bool ok);
    }

    public interface ITestCase
    {
    }


    public interface ITest : IAddonInstance
    {
        void RunTest(ITestEnv env);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class TestAddonType : AddonType
    {
        public override string Name
        {
            get { return "test"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ITest); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(TestAttribute); }
        }

        public static readonly TestAddonType Instance = new TestAddonType();
    }
}
