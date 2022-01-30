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
