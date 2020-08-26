using System;
using NUnit.Framework;
using Array = DataStructure.Data_Structure_1.Array;

namespace DataStructure.UnitTests.Data_Structure_1
{
    [TestFixture]
    public class ArrayTests
    {
        [Test]
        public void Initialize_WhenInitialized_LengthOfArrayMustBeSet()
        {
            var array = new Array(3);

            Assert.That(array.Length, Is.EqualTo(3));
        }

        [Test]
        public void Initialize_WhenInitialized_ArrayMustBeEmpty()
        {
            var array = new Array(3);

            Assert.That(array.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Insert_WhenCalled_AddGivenItemsInArray()
        {
            var array = new Array(3);

            array.Insert(10);
            array.Insert(20);
            array.Insert(30);

            Assert.That(array.Count(), Is.EqualTo(3));
            Assert.That(array.GetItems(), Does.Contain(10));
            Assert.That(array.GetItems(), Does.Contain(20));
            Assert.That(array.GetItems(), Does.Contain(30));
        }

        [Test]
        public void Insert_WhenCalled_AddGivenItemsAndChangeLength()
        {
            var array = new Array(3);

            array.Insert(10);
            array.Insert(20);
            array.Insert(30);
            array.Insert(40);
            array.Insert(50);
            array.Insert(60);

            Assert.That(array.Count(), Is.EqualTo(6));
            Assert.That(array.Length, Is.EqualTo(6));
            Assert.That(array.GetItems(),Does.Contain(40));
            Assert.That(array.GetItems(),Does.Contain(50));
            Assert.That(array.GetItems(),Does.Contain(60));
        }

        [Test]
        public void Insert_WhenCalledWithNull_ThrowNullArgumentException()
        {
            var array = new Array(3);

            Assert.That(()=>array.Insert(null), Throws.ArgumentNullException);

        }

        [Test]
        public void RemoveAt_WhenCalled_RemoveItemAtIndex()
        {
            var array = new Array(3);
            array.Insert(10);
            array.Insert(20);
            array.Insert(30);
            array.Insert(30);

            array.RemoveAt(0);
            array.RemoveAt(1);

            Assert.That(array.Count(), Is.EqualTo(2));

        }

        [Test]
        public void RemoveAt_WhenCalledWithInvalidIndex_ThrowsOutOfRangeException()
        {
            var array = new Array(3);

            Assert.That(()=> array.RemoveAt(5), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void IndexOf_WhenCalled_ReturnItemIndex()
        {
            var array = new Array(3);
            array.Insert(10);
            array.Insert(20);
            array.Insert(30);

            var result = array.IndexOf(20);

            Assert.That(result, Is.EqualTo(1));
        }
    }
}