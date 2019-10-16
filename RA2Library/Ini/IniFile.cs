using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RA2Library.Ini
{
    public class IniFile : MemoryIni
    {
        public void AddValue(string Section, string Key, string Value)
        {
            try
            {
                base.Add(Section, Key, Value);
            }
            catch(DoubleKeyException)
            {
                base.Set(Section, Key, Value);
            }
            
        }
        public void AddValue(string Section, string Key, long Value) =>
            AddValue(Section, Key, Value.ToString());
        public void AddValue(string Section, string Key, ulong Value) =>
            AddValue(Section, Key, Value.ToString());
        public void AddValue(string Section, string Key, double Value) =>
            AddValue(Section, Key, Value.ToString());
        public void AddValue(string Section, string Key, bool Value)
        {
            if (Value) AddValue(Section, Key, "1");
            else AddValue(Section, Key, "0");
        }
        public bool GetBool(string Section, string Key)
        {
            switch (Search(Section, Key).Substring(0, 1).ToUpper())
            {
                case "0":
                case "F":
                case "N":
                    return false;
                case "1":
                case "T":
                case "Y":
                    return true;
                default:
                    throw new System.FormatException("错误的Boolean表示形式");
            }
            //return 
        }
        public int GetInt(string Section, string Key)
        {
            return Convert.ToInt32(Search(Section, Key));
        }
        public double GetDouble(string Section, string Key)
        {
            return Convert.ToDouble(Search(Section, Key));
        }
        public string GetStr(string Section, string Key)
        {
            return Search(Section, Key);
        }
        public IniFile() : base() { }
        public IniFile(string path) : base(path) { }
    }
    // 1.0.0.6
}
