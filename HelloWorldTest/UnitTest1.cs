
using HelloWorld; // Ensure this is the correct namespace for the Program class
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HelloWorldTest
{
    public class UnitTest1
    {

        [Theory]
        [InlineData("5,7\n9,8\n0,8\n4,6\n1,6\n7,9\n3,7\n2,1\n1,0\n6,6\n")]
        [InlineData("6,7\n1,8\n5,8\n4,2\n1,0\n7,9\n3,7\n2,1\n1,0\n6,6\n")]
        [Trait("TestGroup", "ListCalculations")]
        public void TestCalculations(string userInput)
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw); // Capture console output

            // Simulate user input for decimal numbers
            var input = new StringReader(userInput);
            Console.SetIn(input); // Mock the user input

            // Act
            HelloWorld.Program.Main(new string[0]); // Run the Main method

            // Get the console output
            var result = sw.ToString().Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            // Debug output to see the actual result
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine($"Line {i}: '{result[i]}'");
            }

            // Assert that the output contains enough lines
            Assert.True(result.Length >= 5, "The output does not contain enough lines.");

            // Extract expected values from user input
            var numbers = userInput.Trim().Split('\n').Select(double.Parse).ToArray();
            double expectedSum = numbers[3] + numbers[5];
            double expectedDifference = numbers[1] - numbers[8];
            double expectedProduct = numbers[0] * numbers[9];
            double expectedQuotient = numbers[2] / numbers[7];
            double[] expectedRemaining = { numbers[4], numbers[6] };

            // Check the console output for the expected calculations
            Assert.True(LineContainsIgnoreSpaces($"Indeksisijainnin 3 ja 5 lukujen summa: {numbers[3]} + {numbers[5]} = {expectedSum}", result[1]));
            Assert.True(LineContainsIgnoreSpaces($"Indeksisijainnin 1 ja 8 lukujen erotus: {numbers[1]} - {numbers[8]} = {expectedDifference}", result[2]));
            Assert.True(LineContainsIgnoreSpaces($"Indeksisijainnin 0 ja 9 lukujen tulo: {numbers[0]} * {numbers[9]} = {expectedProduct}", result[3]));
            Assert.True(LineContainsIgnoreSpaces($"Indeksisijainnin 2 ja 7 lukujen osamäärä: {numbers[2]} / {numbers[7]} = {expectedQuotient}", result[4]));
            Assert.True(LineContainsIgnoreSpaces($"Listan loput luvut ovat indeksisijainnissa 4 ja 6: {expectedRemaining[0]} ja {expectedRemaining[1]}", result[5]));
        }
        private bool LineContainsIgnoreSpaces(string line, string expectedText)
        {
            // Remove all whitespace and convert to lowercase
            string normalizedLine = Regex.Replace(line, @"\s+", "").ToLower();
            string normalizedExpectedText = Regex.Replace(expectedText, @"\s+", "").ToLower();

            // Create a regex pattern to allow any character for "ä", "ö", "a", and "o"
            string pattern = Regex.Escape(normalizedExpectedText)
                                  .Replace("ö", ".")  // Allow any character for "ö"
                                  .Replace("ä", ".")  // Allow any character for "ä"
                                  .Replace("a", ".")  // Allow any character for "a"
                                  .Replace("o", ".");  // Allow any character for "o"

            // Check if the line matches the pattern, ignoring case
            return Regex.IsMatch(normalizedLine, pattern, RegexOptions.IgnoreCase);
        }


        private int CountWords(string line)
        {
            return line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
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
        private string NormalizeOutput(string output)
        {
            // Normalize line endings to Unix-style '\n' and trim any extra spaces or newlines
            return output.Replace("\r\n", "\n").Trim();
        }
    }
}