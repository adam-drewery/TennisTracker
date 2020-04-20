using FluentAssertions;
using Xunit;

namespace TennisTracker.Tests
{
	public class SetTests
	{
		[Fact]
		public void Correctly_outputs_no_score()
		{
			var set = new Set(Player.One);
			set.Score(Player.One).Should().Be("0-0");
		}
		
		[Fact]
		public void Correctly_outputs_1_nil()
		{
			var set = new Set(Player.One);
			var game = set.AddGame(Player.One);
			
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			game.Points.Add(Player.One);
			
			set.Score(Player.One).Should().Be("1-0");
		}
		
		[Fact]
		public void Correctly_outputs_3_all()
		{
			var set = new Set(Player.One);

			for (int i = 0; i < 3; i++)
			{
				var game = set.AddGame(Player.One);

				game.Points.Add(Player.One);
				game.Points.Add(Player.One);
				game.Points.Add(Player.One);
				game.Points.Add(Player.One);

				game = set.AddGame(Player.One);

				game.Points.Add(Player.Two);
				game.Points.Add(Player.Two);
				game.Points.Add(Player.Two);
				game.Points.Add(Player.Two);
			}
			
			set.Score(Player.One).Should().Be("3-3");
		}
	}
}