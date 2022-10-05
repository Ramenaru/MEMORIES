using System;
using System.Security.Cryptography;
using System.Text;

namespace TrollBridge {

	public static class AsymmetricEncryption 
	{

		private static bool _optimalAsymmetricEncryptionPadding = false;

		public static void GenerateKeys(int keySize, out string publicKey, out string publicAndPrivateKey)
		{
			using(var provider = new RSACryptoServiceProvider(keySize))
			{
				publicKey = provider.ToXmlString (false);
				publicAndPrivateKey = provider.ToXmlString (true);
			}
		}

		public static string EncryptText(string text, int keySize, string publicKeyXml)
		{
			var encrypted = Encrypt (Encoding.UTF8.GetBytes(text), keySize, publicKeyXml);
			return Convert.ToBase64String (encrypted);
		}

		public static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
		{
			if (data == null || data.Length == 0) {
				throw new ArgumentException ("Null Data or Empty", "data");
			}
			int maxLength = GetMaxDataLength (keySize);
			if(data.Length > maxLength){
				throw new ArgumentException (String.Format("Maximum data length is {0}", data.Length, "data"));
			}
			if(!IsKeySizeValid(keySize)){
				throw new ArgumentException ("Key size is not valid", "keySize");
			}
			if(String.IsNullOrEmpty(publicKeyXml)){
				throw new ArgumentException ("Key is null or empty", "publicKeyXml");
			}

			using (var provider = new RSACryptoServiceProvider(keySize))
			{
				provider.FromXmlString (publicKeyXml);
				return provider.Encrypt (data, _optimalAsymmetricEncryptionPadding);
			}
		}

		public static string DecryptText(string text, int keySize, string publicAndPrivateKeyXml)
		{
			var decrypted = Decrypt (Convert.FromBase64String(text), keySize, publicAndPrivateKeyXml);
			return Encoding.UTF8.GetString (decrypted);
		}

		public static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
		{
			if (data == null || data.Length == 0) {
				throw new ArgumentException ("Null Data or Empty", "data");
			}
			if(!IsKeySizeValid(keySize)){
				throw new ArgumentException ("Key size is not valid", "keySize");
			}
			if(String.IsNullOrEmpty(publicAndPrivateKeyXml)){
				throw new ArgumentException ("Key is null or empty", "publicKeyXml");
			}

			using(var provider = new RSACryptoServiceProvider(keySize))
			{
				provider.FromXmlString (publicAndPrivateKeyXml);
				return provider.Decrypt (data, _optimalAsymmetricEncryptionPadding);
			}
		}

		public static int GetMaxDataLength(int keySize)
		{
			if(_optimalAsymmetricEncryptionPadding){
				return ((keySize - 384) / 8) + 7;
			}
			return ((keySize - 384) / 8) + 37;
		}

		public static bool IsKeySizeValid(int keySize)
		{
			return keySize >= 384 && keySize <= 16384 && keySize % 8 == 0;
		}
	}
}
