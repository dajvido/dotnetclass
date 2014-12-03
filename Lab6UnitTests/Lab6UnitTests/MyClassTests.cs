using Lab6Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6UnitTests
{

    [TestFixture]
    public class MyClassTests
    {
        //NUnitTestAdapter
        // DatebaseRepository
        //Save Foo
        //Get Foo
        // Test LeanForward
        [Test]
        public void test_LeanForward()
        {
            DateTime date = new DateTime(2014, 10, 8);
            //date. = DayOfWeek.Saturday;
            MyClass test_MyClass = new MyClass();
            test_MyClass.LeanForward(date);
        }
    }
}
