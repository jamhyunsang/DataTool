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

    public static string Compress(string Str)
    {
        var Data = Encoding.UTF8.GetBytes(Str);
        using (var MemoryStream = new MemoryStream())
        {
            using (var BrotliStream = new BrotliStream(MemoryStream, CompressionLevel.Optimal))
            {
                BrotliStream.Write(Data, 0, Data.Length);
            }
            return ToJson(MemoryStream.ToArray());
        }
    }

    public static string Decompress(string Str)
    {
        var Data = ToObject<byte[]>(Str);
        using (var InputStream = new MemoryStream(Data))
        {
            using (var BrotliStream = new BrotliStream(InputStream, CompressionMode.Decompress))
            {
                using (var outputStream = new MemoryStream())
                {
                    BrotliStream.CopyTo(outputStream);
                    return Encoding.UTF8.GetString(outputStream.ToArray());
                }
            }
        }
    }

    public static string Encrypt(string input)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Def.EncryptKey;
            aes.IV = Def.EncryptIV;
            using (var encryptor = aes.CreateEncryptor())
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write(input);
                    }
                    return ToJson(outputStream.ToArray());
                }
            }
        }
    }

    public static string Decrypt(string encryptedData)
    {
        var ByteData = ToObject<byte[]>(encryptedData);

        using (var aes = Aes.Create())
        {
            aes.Key = Def.EncryptKey;
            aes.IV = Def.EncryptIV;
            using (var decryptor = aes.CreateDecryptor())
            {
                using (var inputStream = new MemoryStream(ByteData))
                {
                    using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cryptoStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}