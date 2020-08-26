using DataStructure.Data_Structure_1;
using NUnit.Framework;

namespace DataStructure.UnitTests.Data_Structure_1
{
    [TestFixture]
    class LinkedListTests
    {
        [Test]
        public void AddFirst_WhenCalled_AddNodeAtStartOfList()
        {
            var list = new LinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(11);

            Assert.That(list.Contains(11), Is.True);
        }

        [Test]
        public void AddLast_WhenCalled_AddNodeAtEndOfList()
        {
            var list = new LinkedList<int>();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddLast(30);

            Assert.That(list.Contains(30), Is.True);
        }

        [Test]
        public void DeleteFirst_WhenCalled_DeleteNodeAtFirstOfList()
        {
            var list = new LinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(20); // first
            list.AddLast(30);
            list.AddLast(40); // last

            list.DeleteFirst();

            Assert.That(list.Contains(10), Is.True);
            Assert.That(list.Contains(20), Is.False);
        }

        [Test]
        public void DeleteLast_WhenCalled_DeleteNodeAtLastOfList()
        {
            var list = new LinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(20); // first
            list.AddLast(30);
            list.AddLast(40); // last

            list.DeleteLast();

            Assert.That(list.Contains(30), Is.True);
            Assert.That(list.Contains(40), Is.False);
        }
      
        [Test]
        public void DeleteLast_OneElementExist_DeleteNode()
        {
            var list = new LinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(20);

            list.DeleteLast();
            list.DeleteLast();

            Assert.That(list.Contains(10), Is.False);
            Assert.That(list.Contains(20), Is.False);

        }

        [Test]
        public void IndexOf_WhenCalled_ReturnIndexOfValue()
        {
            var list = new LinkedList<int>();
            list.AddFirst(10);
            list.AddFirst(20);
            list.AddLast(30);
            list.AddLast(40);

            var index = list.IndexOf(40);

            Assert.That(index, Is.EqualTo(3));
        }
    }
}
