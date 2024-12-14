namespace TripleQuotes {
	internal class Program {
		static void Main(string[] args) {
			var story = """
				Jack said: "How are you?"
				John replied: "Best day ever!"

				THE END!
				""";

            Console.WriteLine(story);
        }
	}
}