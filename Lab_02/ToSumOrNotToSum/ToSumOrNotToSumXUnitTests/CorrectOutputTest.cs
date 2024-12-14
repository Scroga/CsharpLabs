namespace ToSumOrNotToSumXUnitTests
{
    public class CorrectOutputTest
    {
        [Fact]
        public void SimpleInput ()
        {
            var inputTable = """
                             mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                             leden   brambory    tuzemske    Bartak      10895       12      130740
                             leden   brambory    vlastni     Celestyn    15478       10      154780
                             leden   jablka      dovoz       Adamec      1321        30      39630
                             """;

            var reader = new StringReader (inputTable);
            var writer = new StringWriter ();

            var counter = new ColumnSumCounter(reader, writer, "cena");
            counter.InitializeColumnSumCounter();
            counter.Execute();
            

            var expectedOutput = """
                                 cena
                                 ----
                                 52
                                 """;

            Assert.Equal(expectedOutput + writer.NewLine, writer.ToString()); 
        }
    }

    public class ErrorMessageTest
    {
        [Fact]
        public void SimpleInput()
        {
            var inputTable = """
                             mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                             leden   brambory    tuzemske    Bartak      10895       12      130740
                             leden   brambory    vlastni     Celestyn    15478       10      154780
                             leden   jablka      dovoz       Adamec      1321        30      39630
                             """;

            var reader = new StringReader(inputTable);
            var writer = new StringWriter();

            var counter = new ColumnSumCounter(reader, writer, "cena");
            counter.InitializeColumnSumCounter();
            counter.Execute();


            var expectedOutput = """
                                 cena
                                 ----
                                 52
                                 """;

            //Assert.Equal(, writer.ToString());
        }
    }
}