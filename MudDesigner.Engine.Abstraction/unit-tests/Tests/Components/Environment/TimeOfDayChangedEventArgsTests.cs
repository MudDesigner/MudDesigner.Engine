using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MudDesigner.Engine.Components.Environment;
using MudDesigner.Engine.Fixtures;

namespace MudDesigner.Engine
{
    [TestClass]
    public class TimeOfDayChangedEventArgsTests
    {
        [TestMethod]
        [TestCategory("MudDesigner")]
        [TestCategory("Engine")]
        [TestCategory("Engine Abstraction")]
        [Owner("Johnathon Sullinger")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Exception_thrown_with_null_ctor_transitionFrom_argument()
        {
            // Act
            new TimeOfDayChangedEventArgs(null, Mock.Of<ITimePeriod>());
        }

        [TestMethod]
        [TestCategory("MudDesigner")]
        [TestCategory("Engine")]
        [TestCategory("Engine Abstraction")]
        [Owner("Johnathon Sullinger")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Exception_thrown_with_null_ctor_transitionTo_argument()
        {
            // Act
            new TimeOfDayChangedEventArgs(Mock.Of<ITimePeriod>(), null);
        }

        [TestMethod]
        [TestCategory("MudDesigner")]
        [TestCategory("Engine")]
        [TestCategory("Engine Core")]
        [Owner("Johnathon Sullinger")]
        public void Ctor_sets_transitionfrom_property()
        {
            // Arrange
            ITimePeriod transitionFrom = Mock.Of<ITimePeriod>();
            ITimePeriod transitionTo = Mock.Of<ITimePeriod>();

            // Act
            var eventArgs = new TimeOfDayChangedEventArgs(transitionFrom, transitionTo);

            // Assert
            Assert.IsNotNull(eventArgs.TransitioningFrom, "TransitioningFrom was not assigned from the constructor.");
            Assert.AreEqual(eventArgs.TransitioningFrom, transitionFrom, "An incorrect TransitioningFrom value was assigned to the instance.");
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