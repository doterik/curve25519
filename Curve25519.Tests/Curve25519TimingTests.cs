using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Elliptic.Tests
{
	public class Curve25519TimingTests
	{
		[Fact]
		public void Curve25519_GetPublicKey()
		{
			var ticks = new List<long>();
			for (int i = 0; i < 255; i++)
			{
				var stopwatch = Stopwatch.StartNew();

				byte[] privateKey = Curve25519.ClampPrivateKey(TestHelpers.GetUniformBytes((byte)i, 32));

				for (int j = 0; j < 1000; j++)
				{
					byte[] publicKey = Curve25519.GetPublicKey(privateKey); // IDE0059 - Unnecessary assignment of a value
				}

				ticks.Add(stopwatch.ElapsedMilliseconds);
			}

			var min = long.MaxValue;
			var max = long.MinValue;
			foreach (var t in ticks)
			{
				if (min > t) min = t;
				if (max < t) max = t;
			}

			Assert.Null($"Min: {min}, Max: {max}"); // (.Inconclusive) - will Fail for now; waiting for Xunit.SkippableFact
		}
	}
}
