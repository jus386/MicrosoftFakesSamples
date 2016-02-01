namespace EnterpriseLogger.Tests.Unit
{
    using System;
    using System.Fakes;
    using System.IO.Fakes;
    using Logger.LogAggregator;
    using Microsoft.QualityTools.Testing.Fakes;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests using MSFakes shims
    /// </summary>
    [TestClass]
    public class LogAggregatorTestsShims
    {
        [TestMethod]
        public void AggregateLogs_PastThreeDays_ReturnsAllLinesFromPastThreeDays()
        {
            using (ShimsContext.Create())
            {
                // Arrange
                ShimDirectory.GetFilesStringString = (dir, pattern) =>
                {
                    return new[]
                        {
                            @"C:\someLogDir\Log_20121001.log",
                            @"C:\someLogDir\Log_20121002.log",
                            @"C:\someLogDir\Log_20121005.log"
                        };
                };
                ShimFile.ReadAllLinesString = path =>
                {
                    switch (path)
                    {
                        case @"C:\someLogDir\Log_20121001.log":
                            return new[] { "OctFirstLine1", "OctFirstLine2" };
                        case @"C:\someLogDir\Log_20121002.log":
                            return new[] { "ThreeDaysAgoFirstLine", "OctSecondLine2" };
                        case @"C:\someLogDir\Log_20121005.log":
                            return new[] { "OctFifthLine1", "TodayLastLine" };
                    }

                    return new string[] { };
                };
                ShimDateTime.TodayGet = () =>
                {
                    return new DateTime(2012, 10, 05);
                };

                // Act
                var sut = new LogAggregator();
                var result = sut.AggregateLogs(@"C:\SomeLogDir", daysInPast: 3);
                
                // Assert
                Assert.AreEqual(4, result.Length, "Number of aggregated lines incorrect.");
                CollectionAssert.Contains(result, "ThreeDaysAgoFirstLine", "Expected line missing from aggregated log.");
                CollectionAssert.Contains(result, "TodayLastLine", "Expected line missing from aggregated log.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(LogFileException))]
        public void AggregateLogs_InvalidFileName_ThrowsException()
        {
            using (ShimsContext.Create())
            {
                // Arrange
                ShimDirectory.GetFilesStringString = (dir, pattern) => new[]
                {
                    @"C:\someLogDir\Log_20121001.log",
                    @"C:\someLogDir\Log_20121002.log",
                    @"C:\someLogDir\Log_2015-11-09.log"
                };
                ShimFile.ReadAllLinesString = path =>
                {
                    switch (path)
                    {
                        case @"C:\someLogDir\Log_20121001.log":
                            return new[] { "OctFirstLine1", "OctFirstLine2" };
                        case @"C:\someLogDir\Log_20121002.log":
                            return new[] { "ThreeDaysAgoFirstLine", "OctSecondLine2" };
                        case @"C:\someLogDir\Log_Log_2015-11-09.log":
                            return new[] { "OctFifthLine1", "TodayLastLine" };
                    }

                    return new string[] { };
                };
                ShimDateTime.TodayGet = () => new DateTime(2012, 10, 05);

                // Act
                var sut = new LogAggregator();
                var result = sut.AggregateLogs(@"C:\SomeLogDir", daysInPast: 3);

                // Assert expected LogFileException
            }
        }
    }
}
