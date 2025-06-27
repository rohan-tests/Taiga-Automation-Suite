
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Utilities.ExtentReportManager
{
    public class ExtentReportManager
    {
        private static ExtentReports extent = null!;
        private static ExtentSparkReporter sparkReporter = null!;

        public static ExtentReports GetInstance()
        {
            if (extent == null)
            {
                string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
                string reportDir = Path.Combine(projectRoot, "Reports");
                string reportPath = Path.Combine(reportDir, "TestReport.html");
                sparkReporter = new ExtentSparkReporter(reportPath);
                extent = new ExtentReports();
                extent.AttachReporter(sparkReporter);
            }
            return extent;
        }
    }
}