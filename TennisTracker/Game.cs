using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisTracker
{
	/// <summary>
	/// A game consists of a sequence of points played with the same player serving and is a segment of a set.
	/// Each set consists of at least six games.
	/// </summary>
	public class Game
	{
		public Game(Player servingPlayer) => ServingPlayer = servingPlayer;

		public Player ServingPlayer { get; }

		/// <summary>
		/// Period of play between the first successful service of a ball and the point at which that ball goes out of play.
		/// It is the smallest unit of scoring in tennis.
		/// </summary>
		public IList<Player> Points { get; } = new List<Player>();

		public bool IsComplete => Winner != null;

		public Player? Winner
		{
			get
			{
				var results = Match.Players
					.Select(x => new {Player = x, Score = Points.Count(p => p == x)})
					.OrderByDescending(r => r.Score)
					.ToList();
				
				var last = results.Last();
				var first = results.First();

				// If the leader has 2 points over the other player and has a score of at least 4. 
				if (first.Score >= last.Score + 2 && first.Score >= 4)
					return first.Player;

				return null;
			}
		}

		public string Score(Player servingPlayer)
		{
			var results = Points
				.GroupBy(x => x)
				.ToDictionary(x => x.Key, x => x.Count());

			var servingScore = results.SingleOrDefault(r => r.Key == servingPlayer).Value;
			var receivingScore = results.SingleOrDefault(r => r.Key != servingPlayer).Value;

			return GetDisplayScore(servingScore, receivingScore);
		}

		/// <summary>
		/// Convert actual score to strange tennis-based numerical system.
		/// </summary>
		private static string GetDisplayScore(int first, int second)
		{
			if (first < 0) throw new ArgumentOutOfRangeException(nameof(first));
			if (second < 0) throw new ArgumentOutOfRangeException(nameof(second));

			var parts = new[]
			{
				GetDisplayScore(first),
				GetDisplayScore(second)
			};

			if (first > 3 && first > second) parts[0] = "A";
			else if (second > 3 && second > first) parts[1] = "A";

			return string.Join('-', parts);
		}

		private static string GetDisplayScore(int score)
		{
			return score switch
			{
				0 => "0",
				1 => "15",
				2 => "30",
				_ => "40"
			};
		}
	}
}