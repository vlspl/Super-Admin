﻿using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Summary description for AtomPaymentASE
/// </summary>
public class AtomPaymentASE
{
	public AtomPaymentASE()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public String Encrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        string data = ByteArrayToHexString(Encrypt(plainBytes, GetSymmetricAlgorithm(passphrase, salt, iv, iterations))).ToUpper();
        return data;
    }
    public String decrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
    {
        byte[] str = HexStringToByte(plainText);

        string data1 = Encoding.UTF8.GetString(decrypt(str, GetSymmetricAlgorithm(passphrase, salt, iv, iterations)));
        return data1;
    }
    public byte[] Encrypt(byte[] plainBytes, SymmetricAlgorithm sa)
    {
        return sa.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

    }
    public byte[] decrypt(byte[] plainBytes, SymmetricAlgorithm sa)
    {
        return sa.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }
    public SymmetricAlgorithm GetSymmetricAlgorithm(String passphrase, String salt, Byte[] iv, int iterations)
    {
        var saltBytes = new byte[16];
        var ivBytes = new byte[16];
        Rfc2898DeriveBytes rfcdb = new System.Security.Cryptography.Rfc2898DeriveBytes(passphrase, Encoding.UTF8.GetBytes(salt), iterations);
        saltBytes = rfcdb.GetBytes(32);
        var tempBytes = iv;
        Array.Copy(tempBytes, ivBytes, Math.Min(ivBytes.Length, tempBytes.Length));
        var rij = new RijndaelManaged(); //SymmetricAlgorithm.Create();
        rij.Mode = CipherMode.CBC;
        rij.Padding = PaddingMode.PKCS7;
        rij.FeedbackSize = 128;
        rij.KeySize = 128;

        rij.BlockSize = 128;
        rij.Key = saltBytes;
        rij.IV = ivBytes;
        return rij;
    }
    protected static byte[] HexStringToByte(string hexString)
    {
        try
        {
            int bytesCount = (hexString.Length) / 2;
            byte[] bytes = new byte[bytesCount];
            for (int x = 0; x < bytesCount; ++x)
            {
                bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
        catch
        {
            throw;
        }
    }
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}