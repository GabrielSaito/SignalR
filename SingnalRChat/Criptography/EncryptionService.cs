using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionService
{
    private byte[] encryptionKey;

    public EncryptionService(byte[] key)
    {
        encryptionKey = key;
    }

    public string Encrypt(string message)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = encryptionKey;
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(message);
                    }
                }
                byte[] iv = aesAlg.IV;
                byte[] encryptedData = msEncrypt.ToArray();

                byte[] combinedIvData = new byte[iv.Length + encryptedData.Length];
                Array.Copy(iv, 0, combinedIvData, 0, iv.Length);
                Array.Copy(encryptedData, 0, combinedIvData, iv.Length, encryptedData.Length);

                return Convert.ToBase64String(combinedIvData);
            }
        }
    }

    public string Decrypt(string encryptedMessage)
    {
        byte[] combinedIvData = Convert.FromBase64String(encryptedMessage);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = encryptionKey;

            byte[] iv = new byte[aesAlg.BlockSize / 8];
            byte[] encryptedData = new byte[combinedIvData.Length - iv.Length];
            Array.Copy(combinedIvData, 0, iv, 0, iv.Length);
            Array.Copy(combinedIvData, iv.Length, encryptedData, 0, encryptedData.Length);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
