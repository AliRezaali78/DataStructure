using DataStructure.Data_Structure_2;
using NUnit.Framework;
using System;

namespace DataStructure.UnitTests.Data_Structure_2
{
    [TestFixture]
    class HeapTests
    {

        [Test]
        public void Ctor_WhenCalled_SetLength()
        {
            var heap = new MaxHeap();

            Assert.That(heap.Length, Is.EqualTo(16));
        }

        [Test]
        public void Insert_WhenCalledOnce_AddItemToRoot()
        {
            var heap = new MaxHeap();

            heap.Insert(5);

            Assert.That(heap[0], Is.EqualTo(5));
        }
        
        [Test]
        public void Insert_WhenCalledManyTimesWithRightOrders_AddItems()
        {
            var heap = new MaxHeap();

            heap.Insert(5);
            heap.Insert(3);
            heap.Insert(2);
            heap.Insert(1);
            heap.Insert(0);

            Assert.That(heap[0], Is.EqualTo(5));
            Assert.That(heap[1], Is.EqualTo(3));
            Assert.That(heap[2], Is.EqualTo(2));
            Assert.That(heap[3], Is.EqualTo(1));
            Assert.That(heap[4], Is.EqualTo(0));
        }

        [Test]
        public void Insert_WhenCalledManyTimesWithInvalidOrder_ChangeOrder()
        {
            var heap = new MaxHeap();
            
            heap.Insert(15);
            heap.Insert(10);
            heap.Insert(3);
            heap.Insert(8);
            heap.Insert(12);
            heap.Insert(9);
            heap.Insert(4);
            heap.Insert(1);
            heap.Insert(24);

            Assert.That(heap[0], Is.EqualTo(24));
            Assert.That(heap[1], Is.EqualTo(15));
            Assert.That(heap[2], Is.EqualTo(9));
            Assert.That(heap[3], Is.EqualTo(12));
            Assert.That(heap[4], Is.EqualTo(10));
            Assert.That(heap[5], Is.EqualTo(3));
            Assert.That(heap[6], Is.EqualTo(4));
            Assert.That(heap[8], Is.EqualTo(8));
        }
        
        [Test]
        public void Remove_WhenCalled_RemoveRootAndReplaceIt()
        {
            var heap = new MaxHeap();
            heap.Insert(15);
            heap.Insert(10);
            heap.Insert(3);
            heap.Insert(8);
            heap.Insert(12);
            heap.Insert(9);
            heap.Insert(4);
            heap.Insert(1);
            heap.Insert(24);

            heap.Remove();

            Assert.That(heap[0], Is.EqualTo(15));
            Assert.That(heap[1], Is.EqualTo(12));
            Assert.That(heap[2], Is.EqualTo(9));
            Assert.That(heap[3], Is.EqualTo(8));
            Assert.That(heap[4], Is.EqualTo(10));
            Assert.That(heap[5], Is.EqualTo(3));
            Assert.That(heap[6], Is.EqualTo(4));
            Assert.That(heap[7], Is.EqualTo(1));
        }
      
        [Test]
        public void Remove_WhenCalledAsTimeAsItemsInserted_RemoveAllItems()
        {
            var heap = new MaxHeap();
            heap.Insert(15);
            heap.Insert(10);
            heap.Insert(3);
            heap.Insert(8);
            heap.Insert(12);
            heap.Insert(9);
            heap.Insert(4);
            heap.Insert(1);
            heap.Insert(24);

            heap.Remove();
            heap.Remove();
            heap.Remove();
            heap.Remove();
            heap.Remove();
            heap.Remove();
            heap.Remove();
            heap.Remove();
            heap.Remove();

            Assert.That(heap.Size, Is.EqualTo(0));
        }
     
        [Test]
        public void Remove_WhenRootDoesNotHaveChildren_RemoveRootAndReplaceIt()
        {
            var heap = new MaxHeap();
            heap.Insert(15);

            heap.Remove();

            Assert.That(heap[0], Is.EqualTo(15));
        }
      
        [Test]
        public void Remove_WhenRootDoesNotHaveRightChild_RemoveRootAndReplaceIt()
        {
            var heap = new MaxHeap();
            heap.Insert(15);
            heap.Insert(10);
            heap.Insert(9);
            heap.Insert(8);

            heap.Remove();

            Assert.That(heap[0], Is.EqualTo(10));
        }

        [Test]
        public void Insert_WhenHeapIsFull_ThrowIndexOutOfRangeException()
        {
            var heap = new MaxHeap(1);
            
            heap.Insert(1);
            
            Assert.That(()=>heap.Insert(2), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void Remove_WhenHeapIsEmpty_ThrowArgumentNullException()
        {
            var heap = new MaxHeap(1);
            
            Assert.That(()=> heap.Remove(), Throws.ArgumentNullException);
        }

        [Test]
        public void IsMaxHeap_ValidArrayOfIntegers_ReturnsTrue()
        {
            var heap = new MaxHeap();
            var testArray = new[] {15,12,9,8,10,3,4,1};

            bool result = heap.IsMaxHeap(testArray);

            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsMaxHeap_InValidArrayOfIntegers_ReturnsFalse()
        {
            var heap = new MaxHeap();
            var testArray = new[] {1,2,3,4};

            bool result = heap.IsMaxHeap(testArray);

            Assert.That(result, Is.False);
        }
    }
}
