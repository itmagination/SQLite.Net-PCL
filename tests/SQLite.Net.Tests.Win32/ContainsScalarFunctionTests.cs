using NUnit.Framework;
using SQLite.Net.Functions.Scalar;

namespace SQLite.Net.Tests.Win32
{
    [TestFixture]
    public class ContainsScalarFunctionTests
    {

        [TestCase("aaa", "aaa")]
        [TestCase("baaa", "aaa")]
        [TestCase("baaab", "aaa")]
        [TestCase("aaab", "aaa")]
        [TestCase("Aaab", "aaa")]
        [TestCase("ąbc", "ąbc")]
        [TestCase("Ąbc", "ąbc")]
        [TestCase("ąbc", "Ąbc")]
        [TestCase("Ąbc", "Ąbc")]
        [TestCase("11", "1")]
        [Test]
        public void Given_LeftTextContainsRightText_When_SelectContains_Then_ReturnsTrue(params string[] args)
        {
            var contains = Act(args);

            Assert.True(contains);
        }

        [TestCase("123", "aaa")]
        [TestCase("1aa", "aaa")]
        [TestCase("ąb", "ąbc")]
        [TestCase("Ąb", "Ąbc")]
        [TestCase("Ąb", "")]
        [TestCase("Ąb",  null)]
        [Test]
        public void Given_LeftTextDoesNotContainRightText_When_SelectContains_Then_ReturnsFalse(params string[] args)
        {
            var contains = Act(args);

            Assert.False(contains);
        }

        private static bool Act(string[] args)
        {
            var db = new TestDb();
            db.CreateScalarFunction(new ContainsScalarFunction());

            var input1 = args[0];
            var input2 = args[1];

            var contains = db.ExecuteScalar<bool>("select " + ContainsScalarFunction.Function_Name + "(?, ?);", input1, input2);
            return contains;
        }
    }
}
