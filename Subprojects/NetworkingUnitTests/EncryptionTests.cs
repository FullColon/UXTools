using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using EC.Networking;
using NUnit.Framework.Api;

namespace NetworkingUnitTests
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void TestEncryptionAndDecryption()
        {
            byte[] original = ECNetworkHelper.ASCIIStringToNetworkBuffer("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            AesManaged aes = ECCryptoHelper.InitializeAesManaged();
            byte[] encrypted = ECCryptoHelper.EncryptBytes(ref original, aes);
            byte[] decrypted = ECCryptoHelper.DecryptBytes(ref encrypted, aes);
            Assert.IsTrue(original.SequenceEqual(decrypted), "Decrypted message does not match original.");
        }

        [TestMethod]
        public void TestEncryption()
        {
            byte[] original = ECNetworkHelper.ASCIIStringToNetworkBuffer("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            AesManaged aes = ECCryptoHelper.InitializeAesManaged();
            byte[] encrypted = ECCryptoHelper.EncryptBytes(ref original, aes);
            Assert.IsFalse(original.SequenceEqual(encrypted), "Encrypted message matches the original.");
        }
    }
}
