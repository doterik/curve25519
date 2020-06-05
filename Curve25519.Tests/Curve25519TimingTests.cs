#pragma warning disable IDE0007 // Use implicit type
#pragma warning disable IDE0059 // Unnecessary assignment of a value

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
			for (byte b = 0; b < 255; b++)
			{
				var stopwatch = Stopwatch.StartNew();

				byte[] privateKey = Curve25519.ClampPrivateKey(TestHelpers.GetUniformBytes(b, 32));

				for (int i = 0; i < 1000; i++)
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
