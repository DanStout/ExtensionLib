using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace ExtensionLib.Tests
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void Simple_SubstringAfter_works()
        {
            var code = "1837574";
            var message = "Hi, here's your code: "+code;
            var result = message.SubstringAfter("code: ");
            Assert.AreEqual(code, result);
        }

        [Test]
        public void Case_insentitive_SubstringAfter_works()
        {
            var msg = "Hey WILLA WAKKA woo";
            var result = msg.SubstringAfter("willa wakka ", RegexOptions.IgnoreCase);
            Assert.AreEqual("woo", result);
        }

        [Test]
        public void SubstringAfter_returns_empty_string_when_regex_not_contained()
        {
            var text = "Today I walked to the store and saw ";
            var thing = "a pack of pigs";

            var total = text + thing;

            var result = total.SubstringAfter("a packa");
            Assert.AreEqual("", result);
        }

        [Test]
        public void SubstringAfter_works_with_contained_regex()
        {
            var text = "This is a 000string";
            var result = text.SubstringAfter(@"\d{3}");
            Assert.AreEqual("string", result);
        }

        [Test]
        public void SubstringBefore_returns_substring_before_simple_string()
        {
            var text = "Today I went to";
            var add = " the store";
            var comb = text + add;
            Assert.AreEqual(text, comb.SubstringBefore(add));
        }

        [Test]
        public void SubstringBefore_returns_same_string_when_regex_not_contained()
        {
            var text = "The quick brown fox jumps over the lazy dog";
            Assert.AreEqual(text, text.SubstringBefore("999"));
        }

        [Test]
        public void Substring_before_returns_substring_before_regex()
        {
            var textWithSpaces = "testing testing   then something";
            Assert.AreEqual("testing testing", textWithSpaces.SubstringBefore(@"\s{2,}"));
        }
    }
}
