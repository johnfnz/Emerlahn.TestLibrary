using NUnit.Framework;
using System;
using Emerlahn.TestLibrary;
using FluentAssertions;

namespace TestLibraryTests
{
    [TestFixture]
    public class VerifyConstructorGuardClausesTests
    {
        internal class TestClassWithoutGuardClauses
        {
            public TestClassWithoutGuardClauses(StringComparer stringComparer, string dogs)
            {
            }
        }

        internal class TestClassWithGuardClauses
        {
            public TestClassWithGuardClauses(StringComparer stringComparer, string dogs)
            {
                if (stringComparer == null) throw new ArgumentNullException(nameof(stringComparer));
                if (dogs == null) throw new ArgumentNullException(nameof(dogs));
            }
        }

        [Test]
        public void Given_constructor_without_guard_clauses_should_throw()
        {
            Action act = () => typeof(TestClassWithoutGuardClauses).VerifyConstructorGuardClauses();

            act.ShouldThrow<Exception>();
        }

        [Test]
        public void Given_constructor_with_guard_clauses_shouldnt_throw()
        {
            Action act = () => typeof(TestClassWithGuardClauses).VerifyConstructorGuardClauses();

            act.ShouldNotThrow<Exception>();
        }
    }
}
