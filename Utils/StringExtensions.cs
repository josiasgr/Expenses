using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string RemoveInvalidFileNameChars(this string str)
        {
            var invalids = Path.GetInvalidFileNameChars();
            return string.Join("_", str.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }

        [DebuggerStepThrough]
        public static string ToSHA256(this string str)
        {
            using var sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(bytes);
        }
    }
}