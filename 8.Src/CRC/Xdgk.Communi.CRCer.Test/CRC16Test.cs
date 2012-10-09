using Xdgk.Communi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Xdgk.Communi.CRCer.Test
{
    
    
    /// <summary>
    ///这是 CRC16Test 的测试类，旨在
    ///包含所有 CRC16Test 单元测试
    ///</summary>
    [TestClass()]
    public class CRC16Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///CalculateCRC 的测试
        ///</summary>
        [TestMethod()]
        public void CalculateCRCTest2()
        {
            byte[] pByte = null; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = CRC16.CalculateCRC(pByte);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///CalculateCRC 的测试
        ///</summary>
        [TestMethod()]
        public void CalculateCRCTest1()
        {
            byte[] pByte = null; // TODO: 初始化为适当的值
            int nBeginIndex = 0; // TODO: 初始化为适当的值
            int nNumberOfBytes = 0; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = CRC16.CalculateCRC(pByte, nBeginIndex, nNumberOfBytes);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///CalculateCRC 的测试
        ///</summary>
        [TestMethod()]
        public void CalculateCRCTest()
        {
            byte[] pByte = null; // TODO: 初始化为适当的值
            int nBeginIndex = 0; // TODO: 初始化为适当的值
            int nNumberOfBytes = 0; // TODO: 初始化为适当的值
            ushort pChecksum = 0; // TODO: 初始化为适当的值
            ushort pChecksumExpected = 0; // TODO: 初始化为适当的值
            CRC16.CalculateCRC(pByte, nBeginIndex, nNumberOfBytes, out pChecksum);
            Assert.AreEqual(pChecksumExpected, pChecksum);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        /// <summary>
        ///Calc 的测试
        ///</summary>
        [TestMethod()]
        public void CalcTest()
        {
            CRC16 target = new CRC16(); // TODO: 初始化为适当的值
            byte[] bytes = null; // TODO: 初始化为适当的值
            int begin = 0; // TODO: 初始化为适当的值
            int length = 0; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = target.Calc(bytes, begin, length);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///CRC16 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void CRC16ConstructorTest()
        {
            CRC16 target = new CRC16();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
