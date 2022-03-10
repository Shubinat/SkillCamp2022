using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkillCamp2022;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillCamp2022.Tests
{
    [TestClass()]
    public class RegMarkTests
    {
        [TestMethod()]
        [DataRow("H001AX199", "H025AX199", 25, DisplayName = "Within same series")]
        [DataRow("H050AX199", "H025AX199", 26, DisplayName = "Within same series inverse")]
        [DataRow("H999AC199", "H001AT199", 2, DisplayName = "Next first rank simple")]
        [DataRow("H111AE199", "H105AK199", 994, DisplayName = "Next first rank hard")]
        [DataRow("H001AX199", "H999AY199", 2, DisplayName = "Next first rank inverse")]
        [DataRow("B693CO199", "H581BX199", 496392, DisplayName = "Random big diff")]
        [DataRow("P090MH199", "K892BB199", 614583, DisplayName = "Random big diff")]
        [DataRow("M403XY199", "H006AE199", 3600, DisplayName = "Random big diff")]
        [DataRow("A063ME199", "X937OK199", 1608266, DisplayName = "Random big diff")]
        [DataRow("A063ME199", "A063ME199", 1, DisplayName = "Same mark")]
        public void GetCombinationsCountInRange_AllCases_ReturnsCorrectNumber(string mark1, string mark2, int result)
        {
            int actual = RegMark.GetCombinationsCountInRange(mark1, mark2);
            int expected = result;
            Assert.AreEqual(actual, expected);
        }
    }
}