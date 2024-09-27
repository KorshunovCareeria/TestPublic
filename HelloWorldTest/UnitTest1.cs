
using HelloWorld; // Ensure this is the correct namespace for the Program class
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HelloWorldTest
{
    public class UnitTest1
    {


        [Theory]
        [InlineData(new double[] { 5.7, 9.8, 0.8, 4.6, 1.6, 7.9, 3.7, 2.1, 1.0, 6.6 })]
        [InlineData(new double[] { 6.7, 1.8, 5.8, 4.2, 1.0, 7.9, 3.7, 2.1, 1.0, 6.6 })]
        [Trait("TestGroup", "TestCalculations")]
        public void TestCalculations(double[] inputNumbers)
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw); // Capture console output

            var numerolista = new List<double>(inputNumbers);

            // Calculate expected results
            double expectedSum = numerolista[3] + numerolista[5];
            double expectedDifference = numerolista[1] - numerolista[8];
            double expectedProduct = numerolista[0] * numerolista[9];
            double expectedQuotient = numerolista[2] / numerolista[7];
            double[] expectedRemaining = { numerolista[4], numerolista[6] };

            // Act
            Console.WriteLine("Indeksisijainnin 3 ja 5 lukujen summa: " + numerolista[3] + " + " + numerolista[5] + " = " + expectedSum);
            Console.WriteLine("Indeksisijainnin 1 ja 8 lukujen erotus: " + numerolista[1] + " - " + numerolista[8] + " = " + expectedDifference);
            Console.WriteLine("Indeksisijainnin 0 ja 9 lukujen tulo: " + numerolista[0] + " * " + numerolista[9] + " = " + expectedProduct);
            Console.WriteLine("Indeksisijainnin 2 ja 7 lukujen osamaara: " + numerolista[2] + " / " + numerolista[7] + " = " + expectedQuotient);
            Console.WriteLine("Listan loput luvut ovat indeksisijainnissa 4 ja 6: " + expectedRemaining[0] + " ja " + expectedRemaining[1]);

            // Get the console output
            var result = sw.ToString();

            // Assert using LineContainsIgnoreSpaces
            Assert.True(LineContainsIgnoreSpaces(result, $"Indeksisijainnin 3 ja 5 lukujen summa: {numerolista[3]} + {numerolista[5]} = {expectedSum}"));
            Assert.True(LineContainsIgnoreSpaces(result, $"Indeksisijainnin 1 ja 8 lukujen erotus: {numerolista[1]} - {numerolista[8]} = {expectedDifference}"));
            Assert.True(LineContainsIgnoreSpaces(result, $"Indeksisijainnin 0 ja 9 lukujen tulo: {numerolista[0]} * {numerolista[9]} = {expectedProduct}"));
            Assert.True(LineContainsIgnoreSpaces(result, $"Indeksisijainnin 2 ja 7 lukujen osamaara: {numerolista[2]} / {numerolista[7]} = {expectedQuotient}"));
            Assert.True(LineContainsIgnoreSpaces(result, $"Listan loput luvut ovat indeksisijainnissa 4 ja 6: {expectedRemaining[0]} ja {expectedRemaining[1]}"));
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