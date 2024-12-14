namespace EventCounterConsoleApp_UnitTests {
	public class EventCounterTests {
		[Fact]
		public void OneEvent() {
			// Arrange
			var counter = new EventCounter();

			// Act
			counter.EventOccured();

			// Assert
			Assert.Equal(1, counter.Count);
		}

		[Fact]
		public void TwoEvents() {
			// Arrange
			var counter = new EventCounter();

			// Act
			counter.EventOccured();
			counter.EventOccured();

			// Assert
			Assert.Equal(2, counter.Count);
		}

		[Fact]
		public void NoEvent() {
			// Arrange
			var counter = new EventCounter();

			// Act
			// DO NOTHING!

			// Assert
			Assert.Equal(0, counter.Count);
		}
	}
}