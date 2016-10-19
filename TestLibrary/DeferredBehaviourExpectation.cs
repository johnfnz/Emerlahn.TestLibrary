using Ploeh.AutoFixture.Idioms;
using System;
using System.Collections.Generic;

namespace Emerlahn.TestLibrary
{
    internal class DeferredBehaviourExpectation : IBehaviorExpectation
    {
        private readonly IBehaviorExpectation nullAndEmptyGuidBehaviorExpectation;
        public IList<Action> Assertions { get; } = new List<Action>();
        public DeferredBehaviourExpectation()
        {
            nullAndEmptyGuidBehaviorExpectation = new CompositeBehaviorExpectation(new NullReferenceBehaviorExpectation(), new EmptyGuidBehaviorExpectation());
        }

        public void Verify(IGuardClauseCommand command)
        {
            Assertions.Add(() => nullAndEmptyGuidBehaviorExpectation.Verify(command));
        }
    }
}
