using System;
using NUnit.Framework;
using BTrees;

namespace NUnitBSTree
{
    //[TestFixture(typeof(BSTree))]
    //[TestFixture(typeof(BsTreeR))]
    //[TestFixture(typeof(LinkTree))]
    public class NUnitTestsDel<TTree> where TTree : IDelete, new()
    {
        IDelete lst = new TTree();

        [SetUp]
        public void SetUp()
        {
            lst.Clear();
        }

        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 8, 2, 9 }, new int[] { 3, 8, 9, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 8, 2, 9 }, new int[] { 7, 1, 2, 8, 0, 9 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 10, 8, 4, 7, 3 }, 6)]
        public void TestDelRight(int[] input, int[] res, int val)
        {
            IDelete compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelRight(val);
            Assert.IsTrue(lst.Equal(compare));
            Assert.AreEqual(compare.Size(), lst.Size());
        }
        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 9, 8, 1, 0, 2}, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 2, 1, 7, 9, 0, 8 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 10, 8, 4, 7, 3 }, 6)]
        public void TestDelLeft(int[] input, int[] res, int val)
        {
            IDelete compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelLeft(val);
            Assert.IsTrue(lst.Equal(compare));
            Assert.AreEqual(compare.Size(), lst.Size());
        }
        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 9, 8, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 1, 2, 7, 9, 0, 8 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 10, 4, 8, 7, 3 }, 6)]
        public void TestDelLeftRotation(int[] input, int[] res, int val)
        {
            IDelete compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelLeftRotation(val);
            Assert.IsTrue(lst.Equal(compare));
            Assert.AreEqual(compare.Size(), lst.Size());
        }
        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 9, 8, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 7, 1, 2, 9, 0, 8 }, 3)]
        [TestCase(new int[] { 9, 10, 6, 8, 7, 4, 3 }, new int[] { 9, 10, 8, 7, 4, 3 }, 6)]
        public void TestDelRightRotation(int[] input, int[] res, int val)
        {
            IDelete compare = new TTree();
            compare.Init(res);
            lst.Init(input);
            lst.DelRightRotation(val);
            Assert.IsTrue(lst.Equal(compare));
            Assert.AreEqual(compare.Size(), lst.Size());
        }
    }
}
