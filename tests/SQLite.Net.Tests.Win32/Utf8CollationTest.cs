using System.Linq;
using NUnit.Framework;
using SQLite.Net.Collations;

namespace SQLite.Net.Tests.Win32
{
    [TestFixture]
    public class Utf8CollationTests
    {
        string[] names = new string[]
        {
            "Jan",
            "jan",
            "ąąą",
            "ccc",
            "ććć",
            0.ToString()
        };


        [Test]
        public void Given_Names_When_SelectOrderByAsc_Then_ReturnsSortedByAsc()
        {
            var results = Act(names, "asc");

            Assert.AreEqual(names[5], results[0]);
            Assert.AreEqual(names[2], results[1]);
            Assert.AreEqual(names[3], results[2]);
            Assert.AreEqual(names[4], results[3]);
            Assert.AreEqual(names[1], results[4]);
            Assert.AreEqual(names[0], results[5]);
        }

        [Test]
        public void Given_Names_When_SelectOrderByDesc_Then_ReturnsSortedByDesc()
        {
            var results = Act(names, "desc");

            Assert.AreEqual(names[5], results[5]);
            Assert.AreEqual(names[2], results[4]);
            Assert.AreEqual(names[3], results[3]);
            Assert.AreEqual(names[4], results[2]);
            Assert.AreEqual(names[1], results[1]);
            Assert.AreEqual(names[0], results[0]);
        }

        private class User
        {
            public string Name { get; set; }
        }

        private static string[] Act(string[] userNames, string ascDesc)
        {
            var db = new TestDb();
            db.CreateCollation(new Utf8Collation());

            db.Execute("create table Users(Name varchar collate " + Utf8Collation.Collation_Name + ")");

            foreach (var name in userNames)
            {
                db.Execute("insert into Users(Name) values(?)", name);
            }

            var users = db.Query<User>("select Name from Users order by Name " + ascDesc + ";");
            var names = users.Select(s => s.Name).ToArray();
            return names;
        }
    }
}
