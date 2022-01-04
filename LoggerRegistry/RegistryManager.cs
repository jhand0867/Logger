using System;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace LoggerUtil
{
    public class RegistryManager
    {
        // Write Registry 
        public bool CreateKey(string keyName,string subkeyName, string subkeyValue)
        {
            //accessing HKLM root element  
            //and adding "OurSettings" subkey to the "SOFTWARE" subkey  
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyName);

            //storing the values  
            key.SetValue(subkeyName, subkeyValue);
            key.Close();

            return true;
        }
        

        // Read Registry
        public void ReadKey(string keyName)
        {
            // accessing HKLM element
            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName);

            if (key != null)
            {
                string[] keyValues = key.GetValueNames();
                var valFromKey = key.GetValue("key");
                var theType = valFromKey.GetType();
                object keyValue = "";
                object keyType = "";
                string displayKeyValues = "";
                foreach (string item in keyValues)
                {
                    keyValue = key.GetValue(item);
                    keyType = keyValue.GetType().Name;
                    if (keyValue.GetType().Name == "Byte[]")
                    {
                        displayKeyValues += item + "=" + printByteArray((Byte[])keyValue);
                    }
                    else
                    {
                        displayKeyValues += item + "=" + keyValue + "\n";
                    }
                }
                MessageBox.Show(displayKeyValues + "\n");
            }
            key.Close();
        }

        public  string printByteArray(byte[] arr)
        {
            string result = "";
            foreach (byte b in arr)
            {
                result += ((int)b).ToString("X");
            }
            return result;
        }

        public string printByteArrayDec(byte[] arr)
        {
            string result = "";
            foreach (byte b in arr)
            {
                result += ((int)b).ToString();
            }
            return result;
        }


        public byte[] StringToByteArray(string str)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetBytes(str);
        }
        public string ByteArrayToString(byte[] arr)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(arr);
        }
        public int Asc(string s)
        {
            return Encoding.Unicode.GetBytes(s)[0];
        }


    }
}
