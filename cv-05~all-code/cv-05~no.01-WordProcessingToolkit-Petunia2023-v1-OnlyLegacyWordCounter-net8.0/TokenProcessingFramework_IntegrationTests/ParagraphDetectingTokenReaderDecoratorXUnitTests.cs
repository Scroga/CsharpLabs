using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TokenProcessingFramework;

namespace TokenProcessingFramework_IntegrationTests;

internal class ParagraphDetectingTokenReaderDecoratorXUnitTests
{
    [Fact]
    public void TreeLines_OneTextLine() 
    {
        // Arrange
        var input = """
				cena
				12

				""";
        var selectedColumn = "cena";
        var expectedOutput = """
				cena
				----
				12

				""";

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var outputWriter = new StringWriter();
        var processor = new TableSummatorProcessor(outputWriter, selectedColumn);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, processor);

        // Assert
        Assert.Equal(expectedOutput, outputWriter.ToString());
    }
}
