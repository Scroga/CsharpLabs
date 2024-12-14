using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TableSummatorApp;
using TokenProcessingFramework;

namespace TableSummatorApp_IntegrationTests;

public class HeaderProcessor_TableSummatorProcessorTests
{
    [Fact]
    public void SingleColumn()
    {
        // Arrange
        var input = """
				    cena

				    """;
        var targetColumnName = "cena";

        int expectedTargetColumnIndex = 0;
        int expectedColumnCount = 1;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var headerProcessor = new HeaderProcessor(targetColumnName);
   

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, headerProcessor);

        // Assert
        Assert.True(headerProcessor.FoundTargetColumn);
        Assert.Equal(expectedTargetColumnIndex, headerProcessor.TargetColumnIndex);
        Assert.Equal(expectedColumnCount, headerProcessor.HeaderColumnCount);
    }

    [Fact]
    public void EmptyLine_InvalidTargetColumnNameInput()
    {
        // Arrange
        var input = """

				    """;
        var targetColumnName = "cena";

        int expectedTargetColumnIndex = 0;
        int expectedColumnCount = 0;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var headerProcessor = new HeaderProcessor(targetColumnName);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, headerProcessor);


        // Assert
        Assert.False(headerProcessor.FoundTargetColumn);
        Assert.Equal(expectedTargetColumnIndex, headerProcessor.TargetColumnIndex);
        Assert.Equal(expectedColumnCount, headerProcessor.HeaderColumnCount);
    }

    [Fact]
    public void MultipleColumns_ValidInput()
    {
        // Arrange
        var input = """
				cena mesic zbozi typ prodejce

				""";

        var targetColumnName = "zbozi";

        int expectedTargetColumnIndex = 2;
        int expectedColumnCount = 5;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var headerProcessor = new HeaderProcessor(targetColumnName);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, headerProcessor);

        // Assert
        Assert.True(headerProcessor.FoundTargetColumn);
        Assert.Equal(expectedTargetColumnIndex, headerProcessor.TargetColumnIndex);
        Assert.Equal(expectedColumnCount, headerProcessor.HeaderColumnCount);
    }

    [Fact]
    public void MultipleColumns_MultipleWhitespaces()
    {
        // Arrange
        var input = """
				nazev   typ     vaha     cena     mesic zbozi typ     prodejce 

				""";

        var targetColumnName = "zbozi";

        int expectedTargetColumnIndex = 5;
        int expectedColumnCount = 8;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var headerProcessor = new HeaderProcessor(targetColumnName);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, headerProcessor);

        // Assert
        Assert.True(headerProcessor.FoundTargetColumn);
        Assert.Equal(expectedTargetColumnIndex, headerProcessor.TargetColumnIndex);
        Assert.Equal(expectedColumnCount, headerProcessor.HeaderColumnCount);
    }
}

public class DataProcessor_TableSummatorProcessorTests
{
    [Fact]
    public void SimpleTable_SingleLine()
    {
        // Arrange
        var input = """
				    leden brambory 15 vlastni Celestyn
				    """;
        int targetColumnIndex = 2;
        int headerColumnCount = 5;

        int expectedSum = 15;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var dataProcessor = new DataProcessor(targetColumnIndex, headerColumnCount);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, dataProcessor);

        // Assert
        Assert.Equal(expectedSum, dataProcessor.Sum);
    }

    [Fact]
    public void SimpleTable_TwoColumns_MultipleWhitespaces()
    {


        // Arrange
        var input = """
        leden       brambory        10 tuzemske     Bartak
        leden  brambory 15 vlastni Celestyn
        """;
        int targetColumnIndex = 2;
        int headerColumnCount = 5;

        int expectedSum = 25;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var dataProcessor = new DataProcessor(targetColumnIndex, headerColumnCount);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, dataProcessor);

        // Assert
        Assert.Equal(expectedSum, dataProcessor.Sum);
    }

    [Fact]
    public void SimpleTable_MultipleColumns_MultipleWhitespaces()
    {


        // Arrange
        var input = """
        leden     brambory 10       tuzemske      Bartak
        leden    brambory 15 vlastni         Celestyn
        leden     jablka 20 dovoz                     Adamec
        """;
        int targetColumnIndex = 2;
        int headerColumnCount = 5;

        int expectedSum = 45;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var dataProcessor = new DataProcessor(targetColumnIndex, headerColumnCount);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, dataProcessor);

        // Assert
        Assert.Equal(expectedSum, dataProcessor.Sum);
    }

    [Fact]
    public void ComplexTable_MultipleColumns()
    {


        // Arrange
        var input = """
        leden mrkev 14500 dovoz Novak
        leden cibule 9400 vlastni Petrak
        unor hrozny 7800 dovoz Kral
        unor brambory 13200 tuzemske Novak
        unor mrkev 3000 vlastni Bartak
        brezen jablka 4500 dovoz Celestyn
        brezen hrozny 6789 vlastni Adamec
        duben mrkev 2450 tuzemske Petrak
        duben cibule 9000 dovoz Kral
        kveten brambory 12000 vlastni Bartak
        kveten jablka 5678 tuzemske Adamec
        cerven mrkev 7890 dovoz Celestyn
        cerven hrozny 13456 vlastni Petrak
        cervenec cibule 2345 tuzemske Kral
        cervenec brambory 15000 dovoz Novak
        srpen jablka 6789 vlastni Bartak
        srpen mrkev 8900 tuzemske Petrak
        zari cibule 4567 dovoz Celestyn
        zari hrozny 2300 vlastni Adamec
        rijen brambory 9000 tuzemske Novak
        rijen mrkev 7890 dovoz Bartak
        listopad cibule 3200 vlastni Kral
        listopad jablka 4567 tuzemske Petrak
        prosinec hrozny 12300 dovoz Celestyn
        prosinec brambory 10500 vlastni Adamec
        """;
        int targetColumnIndex = 2;
        int headerColumnCount = 5;

        int expectedSum = 197021;

        var tokenReader = new ByLinesTokenReader(new StringReader(input));
        var dataProcessor = new DataProcessor(targetColumnIndex, headerColumnCount);

        // Act
        DefaultTokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, dataProcessor);

        // Assert
        Assert.Equal(expectedSum, dataProcessor.Sum);
    }
}