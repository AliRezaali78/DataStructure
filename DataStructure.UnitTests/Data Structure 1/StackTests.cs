using DataStructure.Data_Structure_1;
using NUnit.Framework;

namespace DataStructure.UnitTests.Data_Structure_1
{
    [TestFixture]
    class StackTests
    {
        [Test]
        public void IsEmpty_WhenCalled_ReturnTrue()
        {
            var stack = new ArrayStack<int>();
            
            var result = stack.IsEmpty();

            Assert.That(result, Is.True);
        }

        [Test]
        public void Push_WhenCalled_AddItemToStack()
        {
            var stack = new ArrayStack<int>();
            
            stack.Push(1);

            Assert.That(!stack.IsEmpty(), Is.True);
        }

        [Test]
        public void Push_WhenCalledSoManyTimes_ThrowsNoException()
        {
            var stack = new ArrayStack<int>();
            
            stack.Push(1);

            Assert.That(()=> stack.Push(1), Throws.Nothing);
        }

        [Test]
        public void Pull_WhenCalled_ReturnLastItem()
        {
            var stack = new ArrayStack<int>();

            stack.Push(1);
            stack.Push(2);

            Assert.That(stack.Pull(),Is.EqualTo(2));
        }

        [Test]
        public void Pull_WhenCalled_MakeStackEmpty()
        {
            var stack = new ArrayStack<int>();

            stack.Push(1);
            stack.Pull();

            Assert.That(stack.IsEmpty(),Is.True);
        }

        [Test]
        public void Pull_WhenCalledWhenStackIsEmpty_ThrowStackIsEmptyException()
        {
            var stack = new ArrayStack<int>();

            Assert.That(()=>stack.Pull(),Throws.TypeOf<StackIsEmptyException>());
        }

        [Test]
        public void Peek_WhenCalled_ReturnLastItem()
        {
            var stack = new ArrayStack<int>();
            stack.Push(1);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Peek_WhenCalledWhenStackIsEmpty_ThrowStackIsEmptyException()
        {
            var stack = new ArrayStack<int>();

            Assert.That(()=>stack.Peek(), Throws.TypeOf<StackIsEmptyException>());
        }
    }
}
