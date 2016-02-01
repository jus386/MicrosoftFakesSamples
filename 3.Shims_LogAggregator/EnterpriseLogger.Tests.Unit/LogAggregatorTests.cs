namespace EnterpriseLogger.Tests.Unit
{
    using Logger.LogAggregator;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Empty unit tests
    /// </summary>
    [TestClass]
    public class LogAggregatorTests
    {
        [TestMethod]
        public void AggregateLogs_PastThreeDays_ReturnsAllLinesFromPastThreeDays()
        {
            // arrange

            // act

            // assert
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(LogFileException))]
        public void AggregateLogs_InvalidFileName_ThrowsException()
        {
            // arrange

            // act

            // assert not needed, expects exception
        }
    }
}
