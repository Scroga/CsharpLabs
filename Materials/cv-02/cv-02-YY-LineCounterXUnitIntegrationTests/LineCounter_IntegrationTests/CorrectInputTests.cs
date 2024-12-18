namespace LineCounter_IntegrationTests {
	public class CorrectInputTests {
		[Fact]
		public void Basic_SingleLine() {
			// Arrange
			var input = """
				Hello world
				""";
			var reader = new StringReader(input);
			var writer = new StringWriter();

			var counter = new LineCounter(reader, writer);

			// Act
			counter.Execute();

			// Assert
			Assert.Equal("1" + writer.NewLine, writer.ToString());
		}

		[Fact]
		public void Basic_TwoLines() {
			// Arrange
			var input = """
				Hello world
				Ahoj svete
				""";
			var reader = new StringReader(input);
			var writer = new StringWriter();

			var counter = new LineCounter(reader, writer);

			// Act
			counter.Execute();

			// Assert
			Assert.Equal("2" + writer.NewLine, writer.ToString());
		}

		[Fact]
		public void Complex_ManyLinesMultipleParagraphs_Hamlet() {
			// Arrange
			var input = """
				To be, or not to be, that is the question:
				Whether 'tis nobler in the mind to suffer
				The slings and arrows of outrageous fortune,
				Or to take Arms against a Sea of troubles,
				And by opposing end them: to die, to sleep
				No more; and by a sleep, to say we end
				The heart-ache, and the thousand natural shocks
				That Flesh is heir to? 'Tis a consummation
				Devoutly to be wished. To die, to sleep,
				To sleep, perchance to Dream; aye, there's the rub,
				For in that sleep of death, what dreams may come,
				When we have shuffled off this mortal coil,
				Must give us pause.

				There's the respect
				That makes Calamity of so long life:
				For who would bear the Whips and Scorns of time,
				The Oppressor's wrong, the proud man's Contumely,
				The pangs of dispised Love, the Law�s delay,
				The insolence of Office, and the spurns
				That patient merit of th'unworthy takes,
				When he himself might his Quietus make
				With a bare Bodkin? Who would Fardels bear,
				To grunt and sweat under a weary life,
				But that the dread of something after death,
				The undiscovered country, from whose bourn
				No traveller returns, puzzles the will,
				And makes us rather bear those ills we have,
				Than fly to others that we know not of?

				Thus conscience does make cowards of us all,
				And thus the native hue of Resolution
				Is sicklied o'er, with the pale cast of Thought,
				And enterprises of great pitch and moment,
				With this regard their Currents turn awry,
				And lose the name of Action. Soft you now,
				The fair Ophelia? Nymph, in thy Orisons
				Be all my sins remember'd.
				""";
			var reader = new StringReader(input);
			var writer = new StringWriter();

			var counter = new LineCounter(reader, writer);

			// Act
			counter.Execute();

			// Assert
			Assert.Equal("38" + writer.NewLine, writer.ToString());
		}
	}
}