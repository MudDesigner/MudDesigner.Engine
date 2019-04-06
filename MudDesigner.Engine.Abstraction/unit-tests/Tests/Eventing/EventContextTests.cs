using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MudDesigner.Engine.Components.Environment;
using MudDesigner.Engine.Eventing;
using MudDesigner.Engine.Fixtures;

namespace MudDesigner.Engine
{
    [TestClass]
    public class EventContextTests
    {
        [TestMethod]
        [TestCategory("MudDesigner")]
        [TestCategory("Engine")]
        [TestCategory("Engine Core")]
        [Owner("Johnathon Sullinger")]
        public void Ctor_sets_runtime_property()
        {
            // Arrange
            IRuntimeHost host = Mock.Of<IRuntimeHost>();
            IGame game = Mock.Of<IGame>();
            IEvent @event = Mock.Of<IEvent>();
            ISubscription subscription = Mock.Of<ISubscription>();
            ILogger logger = Mock.Of<ILogger>();

            // Act
            var context = new EventContext(host, game, @event, subscription, logger);

            // Assert
            Assert.IsNotNull(context.EventSubscription, $"{nameof(context.EventSubscription)} was not assigned from the constructor.");
            Assert.IsNotNull(context.Game, $"{nameof(context.Game)} was not assigned from the constructor.");
            Assert.IsNotNull(context.Logger, $"{nameof(context.Logger)} was not assigned from the constructor.");
            Assert.IsNotNull(context.Runtime, $"{nameof(context.Runtime)} was not assigned from the constructor.");
            Assert.IsNotNull(context.TriggeredInstance, $"{nameof(context.TriggeredInstance)} was not assigned from the constructor.");

            Assert.AreEqual(context.EventSubscription, subscription, $"An incorrect {nameof(context.EventSubscription)} value was assigned to the instance.");
            Assert.AreEqual(context.Game, game, $"An incorrect {nameof(context.Game)} value was assigned to the instance.");
            Assert.AreEqual(context.Logger, logger, $"An incorrect {nameof(context.Logger)} value was assigned to the instance.");
            Assert.AreEqual(context.Runtime, host, $"An incorrect {nameof(context.Runtime)} value was assigned to the instance.");
            Assert.AreEqual(context.TriggeredInstance, @event, $"An incorrect {nameof(context.TriggeredInstance)} value was assigned to the instance.");
        }

        [TestMethod]
        [TestCategory("MudDesigner")]
        [TestCategory("Engine")]
        [TestCategory("Engine Core")]
        [Owner("Johnathon Sullinger")]
        public void Ctor_sets_transitionto_property()
        {
            // Arrange
            ITimePeriod transitionFrom = Mock.Of<ITimePeriod>();
            ITimePeriod transitionTo = Mock.Of<ITimePeriod>();

            // Act
            var eventArgs = new TimeOfDayChangedEventArgs(transitionFrom, transitionTo);

            // Assert
            Assert.IsNotNull(eventArgs.TransitioningTo, "TransitioningTo was not assigned from the constructor.");
            Assert.AreEqual(eventArgs.TransitioningTo, transitionTo, "An incorrect TransitioningTo value was assigned to the instance.");
        }
    }
}