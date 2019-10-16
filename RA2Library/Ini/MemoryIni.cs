using System;
using System.Collections.Generic;
using System.IO;

namespace RA2Library.Ini
{
    /// <summary>
    /// 内存INI<para/>
    /// 将ini文件实例化到内存
    /// </summary>
    public class MemoryIni
    {
        #region 用户属性
        public List<Section> Sections { get; set; }
        public int Count { get => Sections.Count; }
        #endregion

        #region 用户方法
        /// <summary>
        /// 添加一个Section内容
        /// </summary>
        /// <param name="section">Section</param>
        public void Add(Section section) => Sections.Add(section);
        /// <summary>
        /// 添加一个键值对<para/>
        /// 如果没有则新键一个节
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <exception cref="DoubleKeyException">键标识符重复</exception>
        public void Add(string Section, string Key, string Value)
        {
            try
            {
                for (int i = 0; i < Count; i++)
                {
                    if (Sections[i].Identifier.Equals(Section))
                    {
                        Sections[i].Add(Key, Value);
                        return;
                    }
                }
                Section sect = new Section(Section);
                sect.Add(Key, Value);
                Add(sect);
            }
            catch (DoubleKeyException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 删除一个节
        /// </summary>
        /// <param name="Section">节</param>
        /// <exception cref="Section404Exception">找不到对应节</exception>
        public void Drop(string Section)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    Sections.Remove(Sections[i]);
                    return;
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 删除一个键值对
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Key">键</param>
        /// <exception cref="Section404Exception">找不到对应节</exception>
        public void Drop(string Section, string Key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    Sections[i].Drop(Key);
                    return;
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 清空ini文件
        /// </summary>
        public void DropAll() => Sections.Clear();
        /// <summary>
        /// 清空一个节
        /// </summary>
        /// <param name="Section">节</param>
        /// <exception cref="Section404Exception">对应节不存在</exception>
        public void DropAll(string Section)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    Sections[i].DropAll();
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 返回Ini文件内容
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            string ret = "";
            for (int i = 0; i < Count; i++)
            {
                ret += Sections[i].Get();
            }
            return ret;
        }
        /// <summary>
        /// 返回带行注释的Ini文件内容
        /// </summary>
        /// <returns></returns>
        public string GetSummary()
        {
            string ret = "";
            for (int i = 0; i < Count; i++)
            {
                ret += Sections[i].GetSummary();
            }
            return ret;
        }
        /// <summary>
        /// 从文件中读取
        /// </summary>
        /// <param name="Path">文件路径</param>
        public void ReadFromFile(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            Section section = new Section();
            bool haveSummary = false;
            string[] line = sr.ReadLine().Split(';');
            if (line.Length > 1) haveSummary = true;
            if (line[0].Substring(0, 1) == "[")
            {
                if (haveSummary)
                    section = new Section(line[0].Replace("[", "").Replace("]", ""), line[1]);
                else
                    section = new Section(line[0].Replace("[", "").Replace("]", ""));
            }
            while (!sr.EndOfStream)
            {
                // 过滤注释
                line = sr.ReadLine().Split(';');

                //Debug.WriteLine(line.Length);
                // 如果是空行则跳过本次循环
                if (line[0].Length == 0) continue;
                // 节检测
                if (line[0].Substring(0, 1) == "[")
                {
                    Add(section);
                    if (haveSummary)
                        section = new Section(line[0].Replace("[", "").Replace("]", ""), line[1]);
                    else
                        section = new Section(line[0].Replace("[", "").Replace("]", ""));
                }
                // 键值对检测
                else if (line[0].Contains("="))
                {
                    if (haveSummary)
                    {
                        section.Add(line[0].Split('=')[0], line[0].Split('=')[1]);
                        section.SetSummary(line[0].Split('=')[0], line[1]);
                    }
                    else section.Add(line[0].Split('=')[0], line[0].Split('=')[1]);
                }
                // 添加最后一个section
                if (sr.EndOfStream) Add(section);
            }
        }
        /// <summary>
        /// 从文件中读取<para/>
        /// 过滤注释
        /// </summary>
        /// <param name="Path">文件路径</param>
        public void ReadFromFileNoSummary(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            Section section = new Section();
            string line = sr.ReadLine().Split(';')[0];
            if (line.Substring(0, 1) == "[")
            {
                section = new Section(line.Replace("[", "").Replace("]", ""));
            }
            while (!sr.EndOfStream)
            {
                // 过滤注释
                line = sr.ReadLine().Split(';')[0];

                // 如果是空行则跳过本次循环
                if (line.Length == 0) continue;
                // 节检测
                if (line.Substring(0, 1) == "[")
                {
                    Add(section);
                    section = new Section(line.Replace("[", "").Replace("]", ""));
                }
                // 键值对检测
                else if (line.Contains("="))
                    section.Add(line.Split('=')[0], line.Split('=')[1]);

                // 添加最后一个section
                if (sr.EndOfStream) Add(section);
            }
        }
        /// <summary>
        /// 重命名节
        /// </summary>
        /// <param name="OldSection">旧节名</param>
        /// <param name="NewSection">新节名</param>
        /// <exception cref="Section404Exception">找不到节名</exception>
        public void Rename(string OldSection, string NewSection)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(OldSection))
                {
                    Sections[i].SetIdent(NewSection);
                    return;
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 重命名键
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="OldKey">旧键名</param>
        /// <param name="NewKey">新键名</param>
        /// <exception cref="Section404Exception">找不到节</exception>
        public void Rename(string Section, string OldKey, string NewKey)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    Sections[i].Rename(OldKey, NewKey);
                    return;
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 保存到文件<para/>
        /// 如果文件存在 则向文件中新加<para/>
        /// 存在注释
        /// </summary>
        /// <param name="Path">文件路径</param>
        public void SaveInFile(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            string body = "";
            for (int i = 0; i < Count; i++)
            {
                body += Sections[i].GetSummary();
            }
            sw.Write(body);
            sw.Close();
            fs.Dispose();
        }
        /// <summary>
        /// 保存到文件<para/>
        /// 如果文件存在 则向文件中新加<para/>
        /// 不存在注释
        /// </summary>
        /// <param name="Path">文件路径</param>
        public void SaveInFileNoSummary(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            string body = "";
            for (int i = 0; i < Count; i++)
            {
                body += Sections[i].Get();
            }
            sw.Write(body);
            sw.Close();
            fs.Dispose();
        }
        /// <summary>
        /// 保存到文件<para/>
        /// 存在注释
        /// </summary>
        /// <param name="Path">文件路径</param>
        public void SaveToFile(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string body = "";
            for (int i = 0; i < Count; i++)
            {
                body += Sections[i].GetSummary();
            }
            sw.Write(body);
            sw.Close();
            fs.Dispose();
        }
        /// <summary>
        /// 保存到文件<para/>
        /// 不存在注释
        /// </summary>
        /// <param name="Path">文件路径</param>
        public void SaveToFileNoSummary(string Path)
        {
            FileStream fs = new FileStream(Path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            string body = "";
            for (int i = 0; i < Count; i++)
            {
                body += Sections[i].Get();
            }
            sw.Write(body);
            sw.Close();
            fs.Dispose();
        }
        /// <summary>
        /// 查找并返回对应键值
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Key">键</param>
        /// <returns>值</returns>
        /// <exception cref="Section404Exception">找不到对应节</exception>
        public string Search(string Section, string Key)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    return Sections[i].Search(Key);
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 设置一个键值对
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        /// <exception cref="Section404Exception">节不存在</exception>
        public void Set(string Section, string Key, string Value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    Sections[i].Set(Key, Value);
                    return;
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 设置一个节的行注释
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Summary">注释</param>
        /// <exception cref="Section404Exception">节不存在</exception>
        public void SetSummary(string Section, string Summary)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    Sections[i].SetSummary(Summary);
                    return;
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        /// <summary>
        /// 设置键的行注释
        /// </summary>
        /// <param name="Section">节</param>
        /// <param name="Key">键</param>
        /// <param name="Summary">注释</param>
        /// <exception cref="Section404Exception">节不存在</exception>
        public void SetSummary(string Section, string Key, string Summary)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Sections[i].Identifier.Equals(Section))
                {
                    Sections[i].SetSummary(Key, Summary);
                    return;
                }
            }
            throw new Section404Exception("找不到对应节");
        }
        #endregion

        #region 构造方法
        /// <summary>
        /// 无参构造方法<para/>
        /// 初始化Sections列表
        /// </summary>
        public MemoryIni() => Sections = new List<Section>();
        /// <summary>
        /// 构造方法<para/>
        /// 从一个Ini文件中读取(存在行注释)
        /// </summary>
        /// <param name="Path">文件路径</param>
        public MemoryIni(string Path) : this() => ReadFromFile(Path);
        /// <summary>
        /// 构造方法<para/>
        /// 从一个Ini文件中读取
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="HaveSummary">是否读取注释</param>
        public MemoryIni(string Path, bool HaveSummary) : this()
        {
            if (HaveSummary) ReadFromFile(Path);
            else ReadFromFileNoSummary(Path);
        }
        #endregion

        #region 结构体
        /// <summary>
        /// Ini键值对
        /// </summary>
        public struct KeyValue
        {
            #region 用户属性
            /// <summary>
            /// INI键
            /// </summary>
            public string Key { get; private set; }
            /// <summary>
            /// INI值
            /// </summary>
            public string Value { get; private set; }
            /// <summary>
            /// Ini行注释
            /// </summary>
            public string Summary { get; private set; }
            #endregion

            #region 用户方法
            /// <summary>
            /// 返回一个格式为 Key = Value\r\n 的字符串
            /// </summary>
            /// <returns>Key = Value\r\n</returns>
            public string Get()
            {
                return Key + " = " + Value + "\r\n";
            }
            /// <summary>
            /// 返回一个格式为 Key = Value ; Summary\r\n 的字符串
            /// </summary>
            /// <returns>Key = Value ; Summary\r\n</returns>
            public string GetSummary()
            {
                return Key + " = " + Value + " ; " + Summary + "\r\n";
            }
            /// <summary>
            /// 设置键值
            /// </summary>
            /// <param name="Value">键值</param>
            public void Set(string Value) => this.Value = Value;
            /// <summary>
            /// 设置注释
            /// </summary>
            /// <param name="Summary">注释</param>
            public void SetSummary(string Summary) => this.Summary = Summary;
            /// <summary>
            /// 设置键值及注释
            /// </summary>
            /// <param name="Value">键值</param>
            /// <param name="Summary">注释</param>
            public void SetSummary(string Value, string Summary)
            {
                this.Value = Value;
                this.Summary = Summary;
            }
            /// <summary>
            /// 重命名键
            /// </summary>
            /// <param name="Key">新键名</param>
            public void Rename(string Key) => this.Key = Key;
            #endregion

            #region 构造方法
            /// <summary>
            /// 构造方法 创建值和注释为null的键值对
            /// </summary>
            /// <param name="Key">键</param>
            public KeyValue(string Key) : this(Key, null, null) { }
            /// <summary>
            /// 构造方法 创建注释为null键值对
            /// </summary>
            /// <param name="Key">键</param>
            /// <param name="Value">值</param>
            public KeyValue(string Key, string Value) : this(Key, Value, null) { }
            /// <summary>
            /// 构造方法 创建键值对
            /// </summary>
            /// <param name="Key">键</param>
            /// <param name="Value">值</param>
            /// <param name="Summary">注释</param>
            public KeyValue(string Key, string Value, string Summary)
            {
                this.Key = Key;
                this.Value = Value;
                this.Summary = Summary;
            }
            #endregion
        }
        /// <summary>
        /// Ini节
        /// </summary>
        public struct Section
        {
            #region 用户属性
            /// <summary>
            /// Section 的标识符
            /// </summary>
            public string Identifier { get; private set; }
            /// <summary>
            /// Section 中包含的键值对
            /// </summary>
            public List<KeyValue> KeyValues { get; private set; }
            /// <summary>
            /// Section 中的键值对的数量
            /// </summary>
            public int Count { get => KeyValues.Count; }
            /// <summary>
            /// Section 行注释
            /// </summary>
            public string Summary { get; private set; }
            #endregion

            #region 用户方法
            /// <summary>
            /// 向Section中添加一个键值对
            /// </summary>
            /// <param name="keyValue">键值对</param>
            /// <exception cref="DoubleKeyException">键标识符重复</exception>
            public void Add(KeyValue keyValue)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (KeyValues[i].Key.Equals(keyValue))
                    {
                        throw new DoubleKeyException("键标识符重复");
                    }
                }
                KeyValues.Add(keyValue);
            }
            /// <summary>
            /// 向Section中添加一个键值对
            /// </summary>
            /// <param name="Key">键</param>
            /// <param name="Value">值</param>
            /// <exception cref="DoubleKeyException">键标识符重复</exception>
            public void Add(string Key, string Value)
            {
                try
                {
                    Add(new KeyValue(Key, Value));
                }
                catch (DoubleKeyException e)
                {

                    throw e;
                }
            }
            /// <summary>
            /// 删除对应键
            /// </summary>
            /// <param name="Key">键</param>
            /// <returns>成功返回true 找不到返回false</returns>
            public bool Drop(string Key)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (KeyValues[i].Key.Equals(Key))
                    {
                        KeyValues.Remove(KeyValues[i]);
                        return true;
                    }
                }
                return false;
            }
            /// <summary>
            /// 删除所有键<para/>
            /// 清空Section
            /// </summary>
            public void DropAll() => KeyValues.Clear();
            /// <summary>
            /// 返回一个没有注释内容的 Section 内容
            /// </summary>
            /// <returns>[Section] \r\nKVs</returns>
            public string Get()
            {
                string ret;
                ret = "[" + Identifier + "]\r\n";
                for (int i = 0; i < Count; i++)
                    ret += KeyValues[i].Get();
                return ret + "\r\n";
            }
            /// <summary>
            /// 返回一个包含注释内容的 Section 内容
            /// </summary>
            /// <returns>[Section] ; Summary\r\nKVSs</returns>
            public string GetSummary()
            {
                string ret;
                ret = "[" + Identifier + "] ; " + Summary + "\r\n";
                for (int i = 0; i < Count; i++)
                    ret += KeyValues[i].GetSummary();
                return ret + "\r\n";
            }
            /// <summary>
            /// 重命名键
            /// </summary>
            /// <param name="OldKey">旧键名</param>
            /// <param name="NewKey">新键名</param>
            public void Rename(string OldKey, string NewKey)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (KeyValues[i].Key.Equals(OldKey))
                    {
                        KeyValues[i].Rename(NewKey);
                    }
                }
                throw new Key404Exception("键不存在");
            }
            /// <summary>
            /// 搜索并返回键的值
            /// </summary>
            /// <param name="Key">键</param>
            /// <returns>Value 值</returns>
            /// <exception cref="Key404Exception">键不存在</exception>
            public string Search(string Key)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (KeyValues[i].Key.Equals(Key))
                    {
                        return KeyValues[i].Value;
                    }
                }
                throw new Key404Exception("键不存在");
            }
            /// <summary>
            /// 设置键值
            /// </summary>
            /// <param name="Key">键</param>
            /// <param name="Value">值</param>
            public void Set(string Key, string Value)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (KeyValues[i].Key.Equals(Key))
                    {
                        KeyValues[i].Set(Value);
                        return;
                    }
                }
                throw new Key404Exception("键不存在");
            }
            /// <summary>
            /// 设置Section的标识符
            /// </summary>
            /// <param name="Identifier">标识符</param>
            public void SetIdent(string Identifier) => this.Identifier = Identifier;
            /// <summary>
            /// 设置Section的标识符和注释
            /// </summary>
            /// <param name="Identifier">标识符</param>
            /// <param name="Summary">注释</param>
            public void SetIdent(string Identifier, string Summary)
            {
                SetIdent(Identifier);
                SetSummary(Summary);
            }
            /// <summary>
            /// 设置Section的注释
            /// </summary>
            /// <param name="Summary">注释</param>
            public void SetSummary(string Summary) => this.Summary = Summary;
            /// <summary>
            /// 设置键的行注释
            /// </summary>
            /// <param name="Key">键</param>
            /// <param name="Summary">注释</param>
            public void SetSummary(string Key, string Summary)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (KeyValues[i].Key.Equals(Key))
                    {
                        KeyValues[i].SetSummary(Summary);
                        return;
                    }
                }
                throw new Key404Exception("键不存在");
            }
            /// <summary>
            /// 设置键值及注释
            /// </summary>
            /// <param name="Key">键</param>
            /// <param name="Value">值</param>
            /// <param name="Summary">注释</param>
            public void SetSummary(string Key, string Value, string Summary)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (KeyValues[i].Key.Equals(Key))
                    {
                        KeyValues[i].SetSummary(Value, Summary);
                        return;
                    }
                }
                throw new Key404Exception("键不存在");
            }
            #endregion

            #region 构造方法
            /// <summary>
            /// 构造方法 创建一个注释为null的Section
            /// </summary>
            /// <param name="Identifier">Section</param>
            public Section(string Identifier) : this(Identifier, null) { }
            /// <summary>
            /// 构造方法 创建一个Section
            /// </summary>
            /// <param name="Identifier">Section</param>
            /// <param name="Summary">注释</param>
            public Section(string Identifier, string Summary)
            {
                this.Identifier = Identifier;
                this.Summary = Summary;
                KeyValues = new List<KeyValue>();
            }
            #endregion
        }
        #endregion

        #region 异常
        /// <summary>
        /// 键重名异常
        /// </summary>
        [Serializable]
        public class DoubleKeyException : ApplicationException
        {
            public DoubleKeyException() { }
            public DoubleKeyException(string message) : base(message) { }
            public DoubleKeyException(string message, ApplicationException inner) : base(message, inner) { }
            protected DoubleKeyException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
        /// <summary>
        /// 键不存在异常
        /// </summary>
        [Serializable]
        public class Key404Exception : ApplicationException
        {
            public Key404Exception() { }
            public Key404Exception(string message) : base(message) { }
            public Key404Exception(string message, System.Exception inner) : base(message, inner) { }
            protected Key404Exception(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
        /// <summary>
        /// 节不存在异常
        /// </summary>
        [Serializable]
        public class Section404Exception : ApplicationException
        {
            public Section404Exception() { }
            public Section404Exception(string message) : base(message) { }
            public Section404Exception(string message, ApplicationException inner) : base(message, inner) { }
            protected Section404Exception(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
        #endregion
    }
}