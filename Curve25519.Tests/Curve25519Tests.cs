﻿#pragma warning disable IDE0007 // Use implicit type

using System;
using Xunit;

namespace Elliptic.Tests
{
	public class Curve25519Tests
	{
		[Fact]
		public void DiffieHellmanSuccess()
		{
			Random random = TestHelpers.CreateSemiRandomGenerator(); // not truly random in case we need to reproduce test values

			for (int i = 0; i < 1000; i++)
			{
				byte[] alicePrivate = Curve25519.ClampPrivateKey(TestHelpers.GetRandomBytes(random, 32));
				byte[] alicePublic = Curve25519.GetPublicKey(alicePrivate);

				byte[] bobPrivate = Curve25519.ClampPrivateKey(TestHelpers.GetRandomBytes(random, 32));
				byte[] bobPublic = Curve25519.GetPublicKey(bobPrivate);

				byte[] aliceShared = Curve25519.GetSharedSecret(alicePrivate, bobPublic);
				byte[] bobShared = Curve25519.GetSharedSecret(bobPrivate, alicePublic);

				Assert.Equal(aliceShared, bobShared);
			}
		}

		[Fact]
		public void DiffieHellmanFail()
		{
			Random random = TestHelpers.CreateSemiRandomGenerator();
			for (int i = 0; i < 1000; i++)
			{
				byte[] alicePrivate = Curve25519.ClampPrivateKey(TestHelpers.GetRandomBytes(random, 32));
				byte[] alicePublic = Curve25519.GetPublicKey(alicePrivate);

				byte[] bobPrivate = Curve25519.ClampPrivateKey(TestHelpers.GetRandomBytes(random, 32));
				byte[] bobPublic = Curve25519.GetPublicKey(bobPrivate);

				byte[] aliceShared = Curve25519.GetSharedSecret(alicePrivate, bobPublic);

				byte[] alicePublicWithBitToggled = TestHelpers.ToggleBitInKey(alicePublic, random);
				byte[] bobShared = Curve25519.GetSharedSecret(bobPrivate, alicePublicWithBitToggled);

				Assert.NotEqual(aliceShared, bobShared);
			}
		}
	}
}
