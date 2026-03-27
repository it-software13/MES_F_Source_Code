using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDSJ_Framework.Common
{
    public class RASHelper
    {
        /// <summary>  
        /// RSA的加密函数
        /// </summary>  
        /// <param name="xmlPublicKey">公钥</param>  
        /// <param name="encryptString">待加密的字符串</param>  
        /// <returns></returns>  
        public static string[] CreateRAS()
        {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
                string xmlKeys = rsa.ToXmlString(true);
                string xmlPublicKey = rsa.ToXmlString(false);
                string[]  str = new string[] { xmlKeys, xmlPublicKey};
                return str;

        }
        /// <summary>  
        /// RSA的加密函数
        /// </summary>  
        /// <param name="xmlPublicKey">公钥</param>  
        /// <param name="encryptString">待加密的字符串</param>  
        /// <returns></returns>  
        public static string RASEncryption(string xmlPublicKey, string encryptString)
        {
            string Result = string.Empty; 
            try
            {
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                PlainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray); 
            }
            catch (Exception ex)
            {}
            return Result;
        }
        /// <summary>  
        /// RSA的解密函数  
        /// </summary>  
        /// <param name="xmlPrivateKey">私钥</param>  
        /// <param name="decryptString">待解密的字符串</param>  
        /// <returns></returns>  
        public static string RASDecrypt(string xmlPrivateKey, string decryptString)
        {
            string Result = string.Empty;
            try
            {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
               
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                PlainTextBArray = Convert.FromBase64String(decryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
               
            }
            catch (Exception ex)
            {}
            return Result;
        }

    }
}
