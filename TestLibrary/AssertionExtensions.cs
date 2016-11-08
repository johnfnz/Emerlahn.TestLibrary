using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Emerlahn.Utilities;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Idioms;

namespace Emerlahn.TestLibrary
{
    public static class AssertionExtensions
    {
        public static void AssertAll(this IEnumerable<Action> assertions)
        {
            var exceptions = new List<Exception>();
            foreach (var assertion in assertions)
            {
                try
                {
                    assertion();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            if (exceptions.Any())
            {
                if (exceptions.Count == 1) throw exceptions.First();
                string message = $"Multiple assertions failed:\r\n\r\n{string.Join("\r\n\r\n", exceptions.Select(e => e.Message))}\r\n";
                throw new Exception(message);
            }
        }

        public static void AssertContainsSameValuesAs(this ICollection<string> left, ICollection<string> right, string becauseOnlyOnLeft = null, string becauseOnlyOnRight = null)
        {
            var comparer = new SetComparer<string>();
            var comparison = comparer.Compare(left, right, StringComparer.Ordinal);

            var asserts = new List<Action> {
                () => comparison.OnlyOnLeft.Should().BeEmpty(becauseOnlyOnLeft),
                () => comparison.OnlyOnRight.Should().BeEmpty(becauseOnlyOnRight)
            };

            asserts.AssertAll();
        }

        public static void VerifyConstructorGuardClauses(this Type type)
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var deferred = new DeferredBehaviourExpectation();
            var assertion = new GuardClauseAssertion(fixture, deferred);
            assertion.Verify(type.GetConstructors());
            deferred.Assertions.AssertAll();
        }
    }
}
