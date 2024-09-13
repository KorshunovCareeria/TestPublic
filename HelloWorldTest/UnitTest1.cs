


using HelloWorld; // Ensure this is the correct namespace for the Program class
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;

namespace HelloWorldTest
{
    public class UnitTest1
    {


        //Harjoitus - Piirtelyä
        [Fact]
        [Trait("TestGroup", "ArtPrinting")]
        public void ArtPrinting()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Updated expected output to match actual output format
            var expectedOutput = "   *\r\n\r\n   *\r\n  ***\r\n *****\r\n*******";
            var expectedOutput2 = "   *   \r\n       \r\n   *   \r\n  ***  \r\n ***** \r\n*******";


            // Set a timeout of 30 seconds for the test execution
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));

            try
            {
                // Act
                Task task = Task.Run(() =>
                {
                    // Run the program
                    HelloWorld.Program.Main(new string[0]);
                }, cancellationTokenSource.Token);

                task.Wait(cancellationTokenSource.Token);  // Wait for the task to complete or timeout

                // Get the output that was written to the console
                var result = sw.ToString().TrimEnd(); // Trim only the end of the string

                var resultLines = result.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                var expectedLines1 = expectedOutput.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                var expectedLines2 = expectedOutput2.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                // Check if the result matches either expected output
                bool matchesExpectedOutput1 = CompareLines(resultLines, expectedLines1);
                bool matchesExpectedOutput2 = CompareLines(resultLines, expectedLines2);

                // Assert
                Assert.True(matchesExpectedOutput1 || matchesExpectedOutput2, "The output did not match either expected pattern. Output: " + result);



            }
            catch (OperationCanceledException)
            {
                Assert.True(false, "The operation was canceled due to timeout.");
            }
            catch (AggregateException ex) when (ex.InnerException is OperationCanceledException)
            {
                Assert.True(false, "The operation was canceled due to timeout.");
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }


        private bool CompareLines(string[] actualLines, string[] expectedLines)
        {
            if (actualLines.Length != expectedLines.Length)
            {
                return false;
            }

            for (int i = 0; i < actualLines.Length; i++)
            {
                if (actualLines[i] != expectedLines[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}


    

