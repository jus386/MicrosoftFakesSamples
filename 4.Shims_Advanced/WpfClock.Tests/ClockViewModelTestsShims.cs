using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes;

namespace WpfClock.Tests
{
    /// <summary>
    /// Unit testing timers based logic with MSFakes shims
    /// </summary>
    [TestClass]
    public class ClockViewModelTestsShims
    {
        public event EventHandler FakeTick;

        [TestMethod]
        public void Tick3662Times_CheckAngles()
        {
            using (var shimContext = ShimsContext.Create())
            {
                // arrange
                // Fake the Timer.Start method to prevent timer running in this context
                System.Windows.Threading.Fakes.ShimDispatcherTimer.AllInstances.Start =
                (timer) =>
                {
                    // ignore
                };
                // Capture the Timer.Tick event in the FakeTick event handler
                System.Windows.Threading.Fakes.ShimDispatcherTimer.AllInstances.TickAddEventHandler =
                (timer, evHandler) =>
                {
                    FakeTick += evHandler;
                };
                // Fake the DateTime.Now
                System.Fakes.ShimDateTime.NowGet =
                () =>
                {
                    return new DateTime(2015, 1, 28, 0, 0, 0);
                };

                ClockViewModel cvm = new ClockViewModel();

                // act
                // Invoke the Timer.Tick event through captured event handler
                for (int i = 0; i < 3662; i++)
                {
                    FakeTick(this, new EventArgs());
                }

                // assert
                Assert.AreEqual(cvm.HourAngle, 30.5, "Hours angle not correct");
                Assert.AreEqual(cvm.MinutesAngle, 6, "Minutes angle not correct");
                Assert.AreEqual(cvm.SecondsAngle, 6, "Minutes angle not correct");
            }
        }
    }
}
