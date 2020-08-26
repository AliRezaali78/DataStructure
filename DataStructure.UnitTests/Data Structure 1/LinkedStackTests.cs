using DataStructure.Data_Structure_1;
using NUnit.Framework;

namespace DataStructure.UnitTests.Data_Structure_1
{
    [TestFixture]
    class LinkedStackTests
    {

        [Test]
        public void IsEmpty_WhenCalled_ReturnTrue()
        {
            var linkStack = new LinkedStack<int>();

            var result = linkStack.IsEmpty();

            Assert.That(result, Is.True);
        }

        [Test]
        public void Push_WhenCalled_AddItemToStack()
        {
            var stack = new LinkedStack<int>();

            stack.Push(1);

            Assert.That(stack.IsEmpty, Is.False);
        }

        [Test]
        public void Pull_WhenCalled_RemoveLastItemAndRemoveIt()
        {
            var stack = new LinkedStack<int>();
            stack.Push(1);

            var result = stack.Pull();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.IsEmpty, Is.True);
        }

        [Test]
        public void Pull_WhenCalledWhenPushIsCalledTwice_StackIsNotEmpty()
        {
            var stack = new LinkedStack<int>();
            stack.Push(1);
            stack.Push(2);

            var result = stack.Pull();

            Assert.That(result, Is.EqualTo(2));
            Assert.That(stack.IsEmpty, Is.False);
        }
        
        [Test]
        public void Pull_StackIsEmpty_ThrowStackIsEmptyException()
        {
            var stack = new LinkedStack<int>();

            Assert.That(()=> stack.Pull(), Throws.TypeOf<StackIsEmptyException>());
        }

        [Test]
        public void Peek_WhenCalled_ReturnLastItem()
        {
            var stack = new LinkedStack<int>();
            stack.Push(1);

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(stack.IsEmpty(), Is.False);
        }
    }
}
