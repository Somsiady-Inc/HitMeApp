using System;
using HitMeApp.Users.Cryptography;
using NUnit.Framework;

namespace HitMeApp.UsersTests.Cryptography
{
    [TestFixture]
    internal class CryptoTests
    {
        [Test]
        public void Hash_WhenPassedNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Crypto.Hash(null));
        }

        [Test]
        [TestCase("text")]
        [TestCase("")]
        public void Hash_WhenPassedText_ReturnsDifferentText(string text)
        {
            var hash = Crypto.Hash(text);

            Assert.AreNotEqual(hash, text);
        }

        [Test]
        public void Hash_WhenPassedDifferentTexts_ProducesDifferentHashes()
        {
            var hash1 = Crypto.Hash("text1");
            var hash2 = Crypto.Hash("text2");

            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void IsHashValid_WhenPassedNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Crypto.IsHashValid("foo", null));
            Assert.Throws<ArgumentNullException>(() => Crypto.IsHashValid(null, "foo"));
            Assert.Throws<ArgumentNullException>(() => Crypto.IsHashValid(null, null));
        }

        [Test]
        public void IsHashValid_WhenPassedTextAndMatchingHash_ReturnsTrue()
        {
            var text = "text1";
            var hash = Crypto.Hash(text);

            var result = Crypto.IsHashValid(hash, text);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsHashValid_WhenPassedTextAndNotMatchingHash_ReturnsFalse()
        {
            var notMatchingHash = Crypto.Hash("text2");

            var result = Crypto.IsHashValid(notMatchingHash, "text1");

            Assert.IsFalse(result);
        }
    }
}
