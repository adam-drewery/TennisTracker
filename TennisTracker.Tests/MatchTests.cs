using System;
using FluentAssertions;
using Xunit;

namespace TennisTracker.Tests
{
	public class MatchTests
	{
		[Fact]
		public void Correctly_parses_an_empty_string()
		{
			new Match(string.Empty).ToString().Should().Be("0-0");
		}
		
		[Fact]
		public void Correctly_parses_a_single_point()
		{
			new Match("A").ToString().Should().Be("0-0 15-0");
		}
		
		[Fact]
		public void Correctly_parses_advantage_score()
		{
			new Match("BBBAAAABBB").ToString().Should().Be("1-0");
		}
		
		[Fact]
		public void Correctly_parses_a_large_game()
		{
			var input = "AAAABBBBAAAABBBBAAAABBBBAAAAAAAAAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBBBBBA";
			new Match(input).ToString().Should().Be("3-6 6-4 0-0 0-15");
		}
	}
}