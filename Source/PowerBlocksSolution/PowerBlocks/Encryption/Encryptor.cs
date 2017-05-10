using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PowerBlocks.Encryption
{
	
	/// <summary>
	/// Manages the symmetric enctyption and decryption of strings. Warning: this class will NOT protect an in memory string, 
	/// but can effectively be used to quickly and reliable store encrypted strings in the database or configuration files.
	/// 
	/// NOTE This class can be made more secure by a tigher implementation here are some ways if we need more security: 
	///		
	///		- Store salted values in a hidden compiled unmanaged DLL
	///		- Store secret key in another DLL or in some other storage (certificate, database, etc.)
	///		- Using another security provider or a combination of 2 encryption methods chained together
	/// 
	/// For now this simple implementation is sufficient for our requirements.
	/// 
	/// </summary>
	public class Encryptor
	{
		#region Private Fields
		private readonly byte[] _salt;
		private readonly string _secretKey;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructs an instance of the Encryptor class
		/// </summary>
		public Encryptor()
		{
			// This will need to pull the secret and salt from an external source
			// Should we need to implement further security we need to pull these from
			// more secure locations.
			_salt = GetSalt();
			_secretKey = GetSecretKey();
		}

		
		#endregion
		
		#region Methods To Override To Implement Better Security
		
		/// <summary>
		/// FURTURE ENHANCEMENTS: We can store the secret key into a hidden dll.. for example
		/// </summary>
		/// <returns></returns>
		private string GetSecretKey()
		{
			return "CHANGE-THIS-SECRET-IN-YOUR-APP";
		}

		/// <summary>
		/// FURTURE ENHANCEMENTS: We can store the salt into a database table, file, or whatever else we think of 
		/// </summary>
		/// <returns></returns>
		private byte[] GetSalt()
		{
			return Encoding.ASCII.GetBytes("CHANGE-THIS-SALT-IN-YOUR-APP");
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Encript a string using the assigned Encryptor strategy/provider.
		/// You can unencrypt the string using the DecryptString method using the same secret key.
		/// </summary>
		/// <param name="stringToEncrypt"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public string EncryptString(string stringToEncrypt)
		{
			if(string.IsNullOrEmpty(stringToEncrypt))
			{
				throw new ArgumentNullException("stringToEncrypt", "You must pass in a valid string to encrypt.");
			}
			
			string encryptedString;                  
			
			// generate a secure key
			using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_secretKey, _salt))
			{
				// Create a algorithm object and assign keys and IV
				using (RijndaelManaged algorithm = new RijndaelManaged())
				{
					algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
					algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

					// Create a encryptor to perform the stream transform.
					ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);

					// Create stream
					using (MemoryStream streamMemory = new MemoryStream())
					{
						using (CryptoStream streamCrypto = new CryptoStream(streamMemory, encryptor, CryptoStreamMode.Write))
						{
							using (StreamWriter streamWriter = new StreamWriter(streamCrypto))
							{
								//Write all data to the stream.
								streamWriter.Write(stringToEncrypt);
							}
						}
						encryptedString = Convert.ToBase64String(streamMemory.ToArray());
					}
				}
			}
						

			return encryptedString;
		}
		
		/// <summary>
		/// Decrypts a string using the secret key. This will decrypt a string that was previously
		/// encrypted using the matching EncryptString() method using the same secret key
		/// </summary>
		/// <param name="encryptedString"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public string DecryptString(string encryptedString)
		{
			if(string.IsNullOrEmpty(encryptedString))
			{
				throw new ArgumentNullException("encryptedString", "You must pass in a valid string to decrypt.");
			}
			
			string decryptedString;


			// generate the key from the shared secret and the salt
			using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(_secretKey, _salt))
			{
				// Create Algorithm			
				using (RijndaelManaged algorithm = new RijndaelManaged())
				{
					algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
					algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

					// Create your decrytor 
					ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);

					// Create the decryption streams
					byte[] bytes = Convert.FromBase64String(encryptedString);
					using (MemoryStream streamMemory = new MemoryStream(bytes))
					{
						using (CryptoStream streamCrypto = new CryptoStream(streamMemory, decryptor, CryptoStreamMode.Read))
						{
							using (StreamReader streamWriter = new StreamReader(streamCrypto))
							{
								// Get the Decrypted string from the stream
								decryptedString = streamWriter.ReadToEnd();
							}
						}
					}
				}
			}
			
			
			return decryptedString;
		}
		#endregion
	}
}
