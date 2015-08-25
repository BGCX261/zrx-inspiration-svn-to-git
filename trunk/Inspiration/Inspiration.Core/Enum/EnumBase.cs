using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Inspiration.Core.Enum
{
    public class EnumBase
    {
        private List<EnumItem> _List = new List<EnumItem>();
        public List<EnumItem> List
        {
            get
            {
                if (_List.Count == 0)
                {
                    System.Reflection.FieldInfo[] fileds = this.GetType().GetFields();
                    for (int i = 0; i < fileds.Length; i++)
                    {
                        object o = fileds[i].GetValue(this);
                        if (o.GetType() == typeof(EnumItem))
                        {
                            _List.Add((EnumItem)o);
                        }
                    }
                }
                return _List;
            }
        }
        private SelectList _SelectList = null;
        public SelectList SelectList
        {
            get
            {
                if (_SelectList == null)
                {
                    _SelectList = new SelectList(List, "Key", "Value");
                }
                return _SelectList;
            }
        }
        //索引器 它接受星期几的字符串，将它作为索引index
        public int this[string input]
        {
            //get访问器 并返回相应的整数
            get
            {
                foreach (var item in List)
                {
                    if (item.Value == input)
                    {
                        return item.Key;
                    }
                }
                //没找到返回-1
                return -1;
            }
        }
        //索引器 它接受星期几的字符串，将它作为索引index
        public string this[int input]
        {
            //get访问器 并返回相应的整数
            get
            {
                foreach (var item in List)
                {
                    if (item.Key == input)
                    {
                        return item.Value;
                    }
                }
                //没找到返回-1
                return "";
            }
        }
    }
}
