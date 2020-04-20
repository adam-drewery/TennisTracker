using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisTracker
{
	/// <summary>
	/// A contest between two players (singles match) or two teams of players (doubles match),
	/// normally played as the best of three or five sets.
	/// </summary>
	public class Match
	{
		private readonly List<Set> _sets = new List<Set>();

		public Match(string input)
		{
			if (input == null) throw new ArgumentNullException(nameof(input));

			var currentSet = AddSet(ServingPlayer);
			var currentGame = currentSet.AddGame(ServingPlayer);
			
			foreach (var character in input)
			{
				if (character == 'A') currentGame.Points.Add(Player.One);
				else if (character == 'B') currentGame.Points.Add(Player.Two);
				else throw new ArgumentException("Input must consist of only As or Bs");

				if (currentGame.IsComplete && !currentSet.IsComplete)
				{
					SwitchServer();
					currentGame = currentSet.AddGame(ServingPlayer);
				}
				
				if (currentSet.IsComplete)
				{
					currentSet = AddSet(ServingPlayer);
					currentGame = currentSet.AddGame(ServingPlayer);
					SwitchServer();
				}
			}
		}

		public Player ServingPlayer { get; set; } = Player.One;

		public void SwitchServer() => ServingPlayer = ServingPlayer == Player.One ? Player.Two : Player.One;
		
		public Set AddSet(Player server)
		{
			var set = new Set(server);
			_sets.Add(set);
			return set;
		}
		
		public IReadOnlyList<Set> Sets => _sets;
		
		public static Player[] Players { get; } = { Player.One, Player.Two };
		
		public override string ToString()
		{
			var currentGame = _sets.LastOrDefault()?.CurrentGame;
			var setScores = _sets.Select(x => x.Score(ServingPlayer));
			var result = string.Join(' ', setScores);

			if (currentGame != null && !currentGame.IsComplete && currentGame.Points.Any())
				result += $" {currentGame.Score(ServingPlayer)}";

			return result;
		}
	}
}