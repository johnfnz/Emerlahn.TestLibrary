using NUnit.Framework;
using Emerlahn.TestLibrary;
using FluentAssertions;
using System;

namespace TestLibraryTests
{
    [TestFixture]
    public class VerifyConstructorGuardClausesTests
    {
        internal class TestClassWithMissingGuardClauses
        {
            public TestClassWithMissingGuardClauses(int? cats, string dogs)
            {
                if (cats == null) throw new ArgumentNullException(nameof(cats));
            }
        }

        internal class TestClassWithAllGuardClauses
        {
            public TestClassWithAllGuardClauses(int? cats, string dogs)
            {
                if (cats == null) throw new ArgumentNullException(nameof(cats));
                if (dogs == null) throw new ArgumentNullException(nameof(dogs));
            }
        }

        [Test]
        public void Given_constructor_with_missing_guard_clause_should_throw()
        {
            Action act = () => typeof(TestClassWithMissingGuardClauses).VerifyConstructorGuardClauses();

            act.ShouldThrow<Exception>();
        }

        [Test]
        public void Given_constructor_with_all_guard_clauses_should_not_throw()
        {
            Action act = () => typeof(TestClassWithAllGuardClauses).VerifyConstructorGuardClauses();

            act.ShouldNotThrow<Exception>();
        }

        [Test]
        public void Given_constructor_with_ignored_missing_should_not_throw()
        {
            Action act = () => typeof(TestClassWithAllGuardClauses).VerifyConstructorGuardClauses("dogs");

            act.ShouldNotThrow<Exception>();
        }
    }
}
