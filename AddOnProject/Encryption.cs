
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AddOnProject
{
    public enum EncryptType
    {
        Encrypt = 0,
        Descrypt = 1
    }

    class Encryption
    {
        private byte[] Key { get; set; }

        public string result(EncryptType type, string input)
        {
            var des = new DESCryptoServiceProvider()
            {
                Key = Key,
                IV = Key
            };

            // 코드 분석 필요합니다.
            var ms = new MemoryStream();

            var property = new
            {
                transform = type.Equals(EncryptType.Encrypt) ? des.CreateEncryptor() : des.CreateDecryptor(),
                data = type.Equals(EncryptType.Encrypt) ? Encoding.UTF8.GetBytes(input.ToCharArray()) : Convert.FromBase64String(input)
            };

            var cryStream = new CryptoStream(ms, property.transform, CryptoStreamMode.Write);
            var data = property.data;

            cryStream.Write(data, 0, data.Length);
            cryStream.FlushFinalBlock();

            return type.Equals(EncryptType.Encrypt) ? Convert.ToBase64String(ms.ToArray()) : Encoding.UTF8.GetString(ms.GetBuffer());
        }

        public Encryption(string key) {
            Key = ASCIIEncoding.ASCII.GetBytes(key);
        }
    }
}
