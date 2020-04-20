using FluentAssertions;
using Xunit;

namespace TennisTracker.Tests
{
	public class GameTests
	{
		[Fact]
		public void Correctly_outputs_love_love()
		{
			var game = new Game(Player.One);
			game.Score(Player.One).Should().Be("0-0");
		}

		[Fact]
		public void Correctly_outputs_2_love()
		{
			var game = new Game(Player.One);

			game.Points.Add(Player.One);
			game.Points.Add(Player.One);

			game.Score(Player.One).Should().Be("30-0");
		}

		[Fact]
		public void Correctly_outputs_3_advantage()
		{
			var game = new Game(Player.One);

			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);

			game.Score(Player.One).Should().Be("40-A");
		}
		[Fact]
		public void Correctly_outputs_10_all()
		{
			var game = new Game(Player.One);

			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);
			game.Points.Add(Player.Two);

			game.Score(Player.One).Should().Be("40-40");
		}
	}
}