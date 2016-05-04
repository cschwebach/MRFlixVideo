using System;
using DataAccess;
using BuisnessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BuisnessLogic
{
    /*
     * Class that Hashs your users password so they can log in
     */ 
    public static class HashExtension
    {
        public static string HashMd5(this string input)
        {

            // create a hash object of th correct type
            byte[] data;
            using (MD5 md5Hash = MD5.Create())
            {
                data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            // create a StringBuilder to collect the bytes from the hash
            var s = new StringBuilder();

            // loop through the bytes and format each nibble as a hex string 
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // return the StringBuilder as a regular string 
            return s.ToString();
        }

        public static string HashSha1(this string input)
        {
            // create a hash object of th correct type

            // convert input string to a byte array to be hashed
            byte[] data;

            using (SHA1 sha1Hash = SHA1.Create())
            {
                data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            // create a StringBuilder to collect the bytes from the hash
            var s = new StringBuilder();

            // loop through the bytes and format each nibble as a hex string 
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // return the StringBuilder as a regular string 
            return s.ToString();
        }

        public static string HashSha256(this string input)
        {
            // create a hash object of th correct type
            // convert input string to a byte array to be hashed
            byte[] data;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            // create a StringBuilder to collect the bytes from the hash
            var s = new StringBuilder();

            // loop through the bytes and format each nibble as a hex string 
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // return the StringBuilder as a regular string 
            return s.ToString();
        }

        public static string HashSha512(this string input)
        {
            // create a hash object of th correct type
            // convert input string to a byte array to be hashed
            byte[] data;
            using (SHA512 sha512Hash = SHA512.Create())
            {
                data = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            }

            // create a StringBuilder to collect the bytes from the hash
            var s = new StringBuilder();

            // loop through the bytes and format each nibble as a hex string 
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            // return the StringBuilder as a regular string 
            return s.ToString();
        }

        public static bool VerifyMd5Hash(this string compareString, string hashString)
        //compare string is presumally is a newly entered string, while hash string is presumed to be a stored hash value as one sees in cases of passswords
        {
            //crreate a StringComparer
            var c = StringComparer.OrdinalIgnoreCase;

            //do the comparison and return the result
            return (0 == c.Compare(compareString.HashMd5(), hashString));
        }

        public static bool VerifySha1Hash(this string compareString, string hashString)
        //compare string is presumally is a newly entered string, while hash string is presumed to be a stored hash value as one sees in cases of passswords
        {
            //crreate a StringComparer
            var c = StringComparer.OrdinalIgnoreCase;

            //do the comparison and return the result
            return (0 == c.Compare(compareString.HashSha1(), hashString));
        }

        public static bool VerifySha256Hash(this string compareString, string hashString)
        //compare string is presumally is a newly entered string, while hash string is presumed to be a stored hash value as one sees in cases of passswords
        {
            //crreate a StringComparer
            var c = StringComparer.OrdinalIgnoreCase;

            //do the comparison and return the result
            return (0 == c.Compare(compareString.HashSha256(), hashString));
        }

        public static bool VerifySha512Hash(this string compareString, string hashString)
        //compare string is presumally is a newly entered string, while hash string is presumed to be a stored hash value as one sees in cases of passswords
        {
            //crreate a StringComparer
            var c = StringComparer.OrdinalIgnoreCase;

            //do the comparison and return the result
            return (0 == c.Compare(compareString.HashSha512(), hashString));
        }





    }
}

    
