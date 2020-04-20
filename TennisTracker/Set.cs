using System.Collections.Generic;
using System.Linq;

namespace TennisTracker
{
	/// <summary>
	/// A unit of scoring.
	/// A set consists of games and the first player to win six games with a two-game advantage wins the set.
	/// In most tournaments a tiebreak is used at six games all to decide the outcome of a set.
	/// </summary>
	public class Set
	{
		public Player Server { get; }
		private readonly List<Game> _games = new List<Game>();

		public Set(Player server)
		{
			Server = server;
		}

		public Game AddGame(Player servingPlayer)
		{
			var game = new Game(servingPlayer);
			_games.Add(game);
			return game;
		}

		public IReadOnlyList<Game> Games => _games;

		public Game CurrentGame => Games.LastOrDefault();

		public bool IsComplete => Winner != null;

		public Player? Winner
		{
			get
			{
				var results = Match.Players
					.Select(x => new {Player = x, Score = Games.Count(p => p.Winner == x)})
					.OrderByDescending(r => r.Score)
					.ToList();

				var last = results.Last();
				var first = results.First();

				// If the leader has 2 points over the other player and has a score of at least 6. 
				if (first.Score >= last.Score + 2 && first.Score >= 6)
					return first.Player;

				return null;
			}
		}

		public string Score(Player servingPlayer)
		{
			var results = Games
				.Where(g => g.IsComplete)
				.GroupBy(x => x.Winner)
				.ToDictionary(x => x.Key, x => x.Count());

			var servingScore = results.SingleOrDefault(r => r.Key == servingPlayer).Value;
			var receivingScore = results.SingleOrDefault(r => r.Key != servingPlayer).Value;

			return $"{servingScore}-{receivingScore}";
		}
	}
}