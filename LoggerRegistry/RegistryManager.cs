//using System;
//using System.Text;
//using System.Windows;
//using Microsoft.Win32;

//namespace LoggerUtil
//{
//    public class RegistryManager
//    {
//        // Write Registry 
//        public bool CreateKey(string keyName,string subkeyName, string subkeyValue)
//        {
//            //accessing HKLM root element  
//            //and adding "OurSettings" subkey to the "SOFTWARE" subkey  
//            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyName);

//            //storing the values  
//            key.SetValue(subkeyName, subkeyValue);
//            key.Close();

//            return true;
//        }


//        // Read Registry
//        public void ReadKey(string keyName)
//        {
//            // accessing HKLM element
//            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName);

//            if (key != null)
//            {
//                string[] keyValues = key.GetValueNames();
//                var valFromKey = key.GetValue("key");
//                var theType = valFromKey.GetType();
//                object keyValue = "";
//                object keyType = "";
//                string displayKeyValues = "";
//                foreach (string item in keyValues)
//                {
//                    keyValue = key.GetValue(item);
//                    keyType = keyValue.GetType().Name;
//                    if (keyValue.GetType().Name == "Byte[]")
//                    {
//                        displayKeyValues += item + "=" + printByteArray((Byte[])keyValue);
//                    }
//                    else
//                    {
//                        displayKeyValues += item + "=" + keyValue + "\n";
//                    }
//                }
//                MessageBox.Show(displayKeyValues + "\n");
//            }
//            key.Close();
//        }

//        public  string printByteArray(byte[] arr)
//        {
//            string result = "";
//            foreach (byte b in arr)
//            {
//                result += ((int)b).ToString("X");
//            }
//            return result;
//        }

//        public string printByteArrayDec(byte[] arr)
//        {
//            string result = "";
//            foreach (byte b in arr)
//            {
//                result += ((int)b).ToString();
//            }
//            return result;
//        }


//        public byte[] StringToByteArray(string str)
//        {
//            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
//            return enc.GetBytes(str);
//        }
//        public string ByteArrayToString(byte[] arr)
//        {
//            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
//            return enc.GetString(arr);
//        }
//        public int Asc(string s)
//        {
//            return Encoding.Unicode.GetBytes(s)[0];
//        }


//    }
//}
using Microsoft.Win32;
using System.Text;

namespace LoggerUtil
{
    public class RegistryManager
    {
        public bool CreateKey(string keyName, string subkeyName, string subkeyValue)
        {
            RegistryKey subKey = Registry.LocalMachine.CreateSubKey(keyName);
            subKey.SetValue(subkeyName, (object)subkeyValue);
            subKey.Close();
            return true;
        }

        public string ReadKey(string keyName)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyName);
            string str = "";
            if (registryKey != null)
            {
                string[] valueNames = registryKey.GetValueNames();
                registryKey.GetValue("key");
                object obj = (object)"";
                foreach (string name in valueNames)
                {
                    object arr = registryKey.GetValue(name);
                    obj = (object)arr.GetType().Name;
                    if (arr.GetType().Name == "Byte[]")
                        str = str + name + "=" + this.printByteArray((byte[])arr) + "\n";
                    else
                        str = str + name + "=" + arr?.ToString() + "\n";
                }
            }
            registryKey.Close();
            return str + "\n";
        }

        public string printByteArray(byte[] arr)
        {
            string str = "";
            foreach (byte num in arr)
                str += ((int)num).ToString("X2");
            return str;
        }

        public string printByteArrayDec(byte[] arr)
        {
            string str = "";
            foreach (byte num in arr)
                str += ((int)num).ToString();
            return str;
        }

        public byte[] StringToByteArray(string str) => new ASCIIEncoding().GetBytes(str);

        public string ByteArrayToString(byte[] arr) => new ASCIIEncoding().GetString(arr);

        public int Asc(string s) => (int)Encoding.Unicode.GetBytes(s)[0];
    }
}
