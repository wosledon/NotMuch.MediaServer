using System;
using System.Text;

namespace NotMuch.MediaServer.Extensions;

internal static class BinaryExtensions
{
    public static string ToHex(this byte[] bytes)
    {
        StringBuilder hexBuilder = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            hexBuilder.Append(b.ToString("X2"));
        }
        return hexBuilder.ToString();
    }

    public static byte[] ToBytes(this string hex)
    {
        byte[] bytes = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }
}
