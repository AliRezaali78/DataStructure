using DataStructure.Data_Structure_1;
using NUnit.Framework;

namespace DataStructure.UnitTests.Data_Structure_1
{
    [TestFixture]
    class HashTableTests
    {
        [Test]
        public void Put_WhenCalled_AddItem()
        {
            var list = new HashTable<int,string>();

            list.Put(1,"test");

            Assert.That(list.Size,Is.EqualTo(1));
        }

        [Test]
        public void Get_ValidKey_GetValue()
        {
            var list = new HashTable<int,string>();
            list.Put(1,"a");
            list.Put(2,"b");
            list.Put(3,"c");


            var result = list.Get(2);

            Assert.That(result, Is.EqualTo("b"));
        }

        [Test]
        public void Get_InvalidKey_ReturnNull()
        {
            var list = new HashTable<int,string>();
            list.Put(1,"test");

            var result = list.Get(7);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Get_InvalidKeyWithNullInternalLinkedList_ReturnNull()
        {
            var list = new HashTable<int,string>();
            list.Put(2,"test");

            var result = list.Get(7);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Put_WhenCalledWithSameKey_UpdateValue()
        {
            var list = new HashTable<int,string>();

            list.Put(1,"test");
            list.Put(1,"tes");

            Assert.That(list.Size,Is.EqualTo(1));
            Assert.That(list.Get(1),Is.EqualTo("tes"));
        }
        
        [Test]
        public void Put_HashKeyIndexIsSame_AddItems()
        {
            var list = new HashTable<int,string>();

            list.Put(1,"test");
            list.Put(6,"2");

            Assert.That(list.Size,Is.EqualTo(2));
            Assert.That(list.Get(6),Is.EqualTo("2"));
            Assert.That(list.Get(1),Is.EqualTo("test"));

        }

        [Test]
        public void Remove_ValidKeyAtFirst_RemoveItem()
        {
            var list = new HashTable<int,string>();
            list.Put(1,"test");
            list.Put(2,"test");

            list.Remove(1);
            var value = list.Get(1);

            Assert.That(value, Is.Null);
        }

        [Test]
        public void Remove_ValidKeyAtLast_RemoveItem()
        {
            var list = new HashTable<int,string>();
            list.Put(1,"test");
            list.Put(2,"test");

            list.Remove(2);
            var value = list.Get(2);

            Assert.That(value, Is.Null);
        }

        [Test]
        public void Remove_ValidKeyAtSecondPlace_RemoveItem()
        {
            var list = new HashTable<int,string>();
            list.Put(1,"test");
            list.Put(2,"test");
            list.Put(3,"test");
            list.Put(4,"test");

            list.Remove(2);
            var value = list.Get(2);

            Assert.That(value, Is.Null);
        }
       
        [Test]
        public void Remove_ValidKeyAtThirdPlace_RemoveItem()
        {
            var list = new HashTable<int,string>();
            list.Put(1,"test");
            list.Put(2,"test");
            list.Put(3,"test");
            list.Put(4,"test");

            list.Remove(3);
            var value = list.Get(3);

            Assert.That(value, Is.Null);
        }
      
        [Test]
        public void Remove_ValidKeyAtMiddle_RemoveItem()
        {
            var list = new HashTable<int,string>();
            list.Put(1,"test");
            list.Put(2,"test");
            list.Put(3,"test");

            list.Remove(2);
            var value = list.Get(2);

            Assert.That(value, Is.Null);
        }
    }
}
