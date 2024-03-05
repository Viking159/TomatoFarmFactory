namespace Extensions.Data
{
    using System.Text;

    /// <summary>
    /// Crypt & decrypt string
    /// </summary>
    public class TextEncrypter
    {
        private readonly string encryptionKey;

        private const string DEFAULT_KEY = "default_key";

        /// <summary>
        /// Default constuctor
        /// </summary>
        public TextEncrypter() : this(DEFAULT_KEY)
        { }

        /// <summary>
        /// Constructer with custom encryption key
        /// </summary>
        /// <param name="encryptionKey"></param>
        public TextEncrypter(string encryptionKey)
            => this.encryptionKey = encryptionKey;

        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <returns>Encrypted text</returns>
        public string EncryptString(string text)
            => XOREncrypt(text);

        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <returns>Decrypted text</returns>
        public string DecryptString(string encryptText)
            => XOREncrypt(encryptText);

        private string XOREncrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                result.Append((char)(text[i] ^ encryptionKey[i % encryptionKey.Length]));
            }
            return result.ToString();
        }
    }
}