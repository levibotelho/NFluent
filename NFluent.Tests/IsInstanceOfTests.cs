﻿namespace NFluent.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class IsInstanceOfTests
    {
        #region IsInstanceOf tests

        [Test]
        public void IsInstanceOfWorks()
        {
            const string StringObj = "for unit testing";
            const int IntObj = 23;
            const long LongObj = long.MaxValue;
            const double DoubleObj = 23d;
            const decimal DecimalObj = 2;
            var person = new Person();
            List<string> stringList = new List<string>();
            int[] integerArray = new int[10];

            // string
            Check.That(StringObj).IsInstanceOf<string>();

            // numerics
            Check.That(IntObj).IsInstanceOf<int>();
            Check.That(LongObj).IsInstanceOf<long>();
            Check.That(DoubleObj).IsInstanceOf<double>();
            Check.That(DecimalObj).IsInstanceOf<decimal>();
            Check.That((byte)2).IsInstanceOf<byte>();

            //// objects
            Check.That(person).IsInstanceOf<Person>();

            //// IEnumerable
            Check.That(stringList).IsInstanceOf<List<string>>();
            Check.That(integerArray).IsInstanceOf<int[]>();
        }

        [Test]
        [ExpectedException(typeof(FluentAssertionException), ExpectedMessage = "\nThe actual value:\n\t[Telemachus]\nis not an instance of:\n\t[NFluent.Tests.Person]\nbut an instance of:\n\t[NFluent.Tests.Child]\ninstead.")]
        public void IsInstanceOfThrowsExceptionWithDerivedTypeAsCheckedExpression()
        {
            var child = new Child() { Name = "Telemachus" };
            Check.That(child).IsInstanceOf<Person>();
        }

        [Test]
        [ExpectedException(typeof(FluentAssertionException), ExpectedMessage = "\nThe actual value:\n\t[23]\nis not an instance of:\n\t[NFluent.Tests.Person]\nbut an instance of:\n\t[System.Int32]\ninstead.")]
        public void IsInstanceOfThrowsExceptionWithProperFormatWhenFailsWithInt()
        {
            const int IntObject = 23;
            Check.That(IntObject).IsInstanceOf<Person>();
        }

        [Test]
        [ExpectedException(typeof(FluentAssertionException), ExpectedMessage = "\nThe actual value:\n\t[\"for unit testing\"]\nis not an instance of:\n\t[NFluent.Tests.Person]\nbut an instance of:\n\t[System.String]\ninstead.")]
        public void IsInstanceOfThrowsExceptionWithProperFormatWhenFailsWithString()
        {
            const string StringObj = "for unit testing";
            Check.That(StringObj).IsInstanceOf<Person>();
        }

        #endregion

        #region IsNotInstanceOf tests

        [Test]
        public void IsNotInstanceOfWorks()
        {
            const string StringObj = "for unit testing";
            const int IntObj = 23;
            const long LongObj = long.MaxValue;
            const double DoubleObj = 23d;
            const decimal DecimalObj = 2;
            var person = new Person();
            List<string> stringList = new List<string>();
            int[] integerArray = new int[10];

            // string
            Check.That(StringObj).IsNotInstanceOf<int>();

            // numerics
            Check.That(IntObj).IsNotInstanceOf<long>();
            Check.That(LongObj).IsNotInstanceOf<string>();
            Check.That(DoubleObj).IsNotInstanceOf<int>();
            Check.That(DecimalObj).IsNotInstanceOf<float>();
            Check.That((byte)1).IsNotInstanceOf<string>();

            // objects
            Check.That(person).IsNotInstanceOf<NumbersRelatedTests>();

            // IEnumerable
            Check.That(stringList).IsNotInstanceOf<List<int>>();
            Check.That(integerArray).IsNotInstanceOf<string[]>();
        }
        
        [Test]
        public void IsNotInstanceOfWorksWithString()
        {
            const string MotivationalSaying = "Failure is mother of success.";
            Check.That(MotivationalSaying).IsNotInstanceOf<int>();
        }

        [Test]
        [ExpectedException(typeof(FluentAssertionException), ExpectedMessage = "\nThe actual value:\n\t[23]\nis an instance of:\n\t[System.Int32]\nwhich is not expected.")]
        public void IsNotInstanceOfThrowsExceptionWithProperFormatWhenFailsWithInt()
        {
            const int IntObject = 23;

            Check.That(IntObject).IsNotInstanceOf<int>();
        }

        [Test]
        [ExpectedException(typeof(FluentAssertionException), ExpectedMessage = "\nThe actual value:\n\t[\"If you don’t want to slip up tomorrow, speak the truth today (Bruce Lee).\"]\nis an instance of:\n\t[System.String]\nwhich is not expected.")]
        public void IsNotInstanceOfThrowsExceptionWithProperFormatWhenFailsWithString()
        {
            const string Statement = "If you don’t want to slip up tomorrow, speak the truth today (Bruce Lee).";

            Check.That(Statement).IsNotInstanceOf<string>();
        }

        #endregion

        [Test]
        public void InheritsFromWorks()
        {
            var child = new Child() { Name = "Telemachus" };
            Check.That(child).InheritsFrom<Person>();
        }

        [Test]
        public void InheritsFromWorksAlsoWithTheSameType()
        {
            var child = new Child() { Name = "Telemachus" };
            Check.That(child).InheritsFrom<Child>();
        }

        [Test]
        [ExpectedException(typeof(FluentAssertionException), ExpectedMessage = "\nThe checked expression is not of part of the inheritance hierarchy, or of the same type than the specified one.\nIndeed, checked expression type:\n\t[NFluent.Tests.Person]\nis not a derived type of\n\t[NFluent.Tests.Child].")]
        public void InheritsFromThrowsExceptionWhenFailing()
        {
            var father = new Person() { Name = "Odysseus" };
            Check.That(father).InheritsFrom<Child>();
        }

        // TODO: add unit test related to theIsNotInstance error messages (for IEnumerable, object, etc)
    }
}
