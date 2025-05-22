using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChmodConverterApp;

namespace ChmodConverterTests
{
    [TestClass]
    public class ChmodConverterTests
    {
        [TestMethod]
        public void SymbolicToNumeric_FullPermissions_Returns777()
        {
            string result = ChmodConverter.SymbolicToNumeric("rwxrwxrwx");
            Assert.AreEqual("777", result);
        }

        [TestMethod]
        public void SymbolicToNumeric_NoPermissions_Returns000()
        {
            string result = ChmodConverter.SymbolicToNumeric("---------");
            Assert.AreEqual("000", result);
        }

        [TestMethod]
        public void SymbolicToNumeric_StandardPermissions_Returns644()
        {
            string result = ChmodConverter.SymbolicToNumeric("rw-r--r--");
            Assert.AreEqual("644", result);
        }

        [TestMethod]
        public void SymbolicToNumeric_ExecuteOnly_Returns111()
        {
            string result = ChmodConverter.SymbolicToNumeric("--x--x--x");
            Assert.AreEqual("111", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumeric_InvalidLength_ThrowsException()
        {
            ChmodConverter.SymbolicToNumeric("rwxr-xr-"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SymbolicToNumeric_InvalidCharacters_ThrowsException()
        {
            ChmodConverter.SymbolicToNumeric("rwqr-xr-x"); 
        }

        [TestMethod]
        public void NumericToSymbolic_777_ReturnsFullPermissions()
        {
            string result = ChmodConverter.NumericToSymbolic("777");
            Assert.AreEqual("rwxrwxrwx", result);
        }

        [TestMethod]
        public void NumericToSymbolic_000_ReturnsNoPermissions()
        {
            string result = ChmodConverter.NumericToSymbolic("000");
            Assert.AreEqual("---------", result);
        }

        [TestMethod]
        public void NumericToSymbolic_644_ReturnsStandardPermissions()
        {
            string result = ChmodConverter.NumericToSymbolic("644");
            Assert.AreEqual("rw-r--r--", result);
        }

        [TestMethod]
        public void NumericToSymbolic_731_ReturnsSpecificPermissions()
        {
            string result = ChmodConverter.NumericToSymbolic("731");
            Assert.AreEqual("rwx-wx--x", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_InvalidLength_ThrowsException()
        {
            ChmodConverter.NumericToSymbolic("77");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumericToSymbolic_InvalidDigits_ThrowsException()
        {
            ChmodConverter.NumericToSymbolic("788"); 
        }
    }
}