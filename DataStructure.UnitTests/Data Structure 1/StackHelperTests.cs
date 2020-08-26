using DataStructure.Data_Structure_1;
using NUnit.Framework;

namespace DataStructure.UnitTests.Data_Structure_1
{
    [TestFixture]
    class StackHelperTests
    {

        [Test]
        [TestCase("()")]
        [TestCase("(())")]
        [TestCase("[[][]]")]
        [TestCase("{{}}")]
        [TestCase("{1,2,3,4,5}")]
        [TestCase("{<<1>>,<2>,(3),[4],{5}}")]
        [TestCase("{{{}}}")]
        [TestCase("{Console.WriteLine();}")]
        [TestCase("((1+2)*3)")]
        public void Compile_ValidSyntax_Compile(string syntax)
        {
            Assert.That(()=>StackHelper.Compile(syntax), Throws.Nothing);
        }

        [Test]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase("())")]
        [TestCase("())")]
        [TestCase("{{{{{{}}}}}")]
        [TestCase("{Console.WriteLine();")]
        [TestCase("((1+2)*3")]
        public void Compile_InValidSyntax_ThrowException(string syntax)
        {
            Assert.That(()=>StackHelper.Compile(syntax), Throws.Exception);
        }
    }
}
