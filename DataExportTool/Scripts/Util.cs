using Newtonsoft.Json;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

public static class Util
{
    public static string ToJson(object Obj)
    {
        return JsonConvert.SerializeObject(Obj);
    }

    public static T ToObject<T>(string Str)
    {
        return JsonConvert.DeserializeObject<T>(Str);
    }

    public static byte[] Compress(string str)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var brotliStream = new BrotliStream(memoryStream, CompressionLevel.Optimal))
            {
                using (var streamWriter = new StreamWriter(brotliStream))
                {
                    streamWriter.Write(str);
                }
            }
            return memoryStream.ToArray();
        }
    }

    public static string Decompress(byte[] bytes)
    {
        using (var inputStream = new MemoryStream(bytes))
        {
            using (var brotliStream = new BrotliStream(inputStream, CompressionMode.Decompress))
            {
                using (var streamReader = new StreamReader(brotliStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }

    public static byte[] Encrypt(byte[] bytes)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Def.EncryptKey;
            aes.IV = Def.EncryptIV;
            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    }

    public static byte[] Decrypt(byte[] bytes)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Def.EncryptKey;
            aes.IV = Def.EncryptIV;
            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                using (var memoryStream = new MemoryStream(bytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var outputStream = new MemoryStream())
                        {
                            cryptoStream.CopyTo(outputStream);
                            return outputStream.ToArray();
                        }
                    }
                }
            }
        }
    }
}