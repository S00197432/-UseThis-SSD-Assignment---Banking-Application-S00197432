using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Banking_Application;
using System.IO;

namespace SSD_Assignment___Banking_Application
{
   internal class EncryptDecrypt
   {
		
		public string Encrypt(string textToEncrypt)
		{
			if (textToEncrypt != null) {

				string GiveENC = "";
				const string pubkey = "numeight";
				const string seckey = "eightnum";

				byte[] seckeyByte = { };
				seckeyByte = Encoding.UTF8.GetBytes(seckey);

				byte[] pubkeybyte = { };
				pubkeybyte = Encoding.UTF8.GetBytes(pubkey);

				MemoryStream ms = null;
				CryptoStream cs = null;

				byte[] inputbyteArray = Encoding.UTF8.GetBytes(textToEncrypt);
				using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
				{

					ms = new MemoryStream();
					cs = new CryptoStream(ms, des.CreateEncryptor(pubkeybyte, seckeyByte), CryptoStreamMode.Write);
					cs.Write(inputbyteArray, 0, inputbyteArray.Length);
					cs.FlushFinalBlock();
					GiveENC = Convert.ToBase64String(ms.ToArray());
				}
				return GiveENC;
				

			}
		
			else
			{
				Console.WriteLine("No Work");
				return null;
			}

		}
		
		public string Decrpyt(string textToDecrypt)
        {
            try
			{ 
				
				string GiveDEC = "";
				const string pubkey = "numeight";
				const string privkey = "eightnum";

				byte[] privkeyByte = { };
				privkeyByte = Encoding.UTF8.GetBytes(privkey);

				byte[] pubkeyByte = { };
				pubkeyByte = Encoding.UTF8.GetBytes(pubkey);

				MemoryStream ms = null;
				CryptoStream cs = null;

				byte[] inputbyteArray = new byte[textToDecrypt.Replace(" ", "+").Length];
				inputbyteArray = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
				using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
				{
					ms = new MemoryStream();
					cs = new CryptoStream(ms, des.CreateDecryptor(pubkeyByte, privkeyByte), CryptoStreamMode.Write);
					cs.Write(inputbyteArray, 0, inputbyteArray.Length);
					cs.FlushFinalBlock();
					Encoding encoding = Encoding.UTF8;
					GiveDEC = encoding.GetString(ms.ToArray());
				}
				return GiveDEC;
			}
           catch(Exception exx)
            {
				throw new Exception(exx.Message, exx.InnerException);
			}
			
		}
		


   }
}
