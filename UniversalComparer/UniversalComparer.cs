using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UniversalComparer
{
    class UniversalComparer : IComparer
    {
        private string SortString { get; set;}

        private bool _nullValueIsSmallest;
       // private Type Type { get; set; }

        public List<Condition> Conditions = new List<Condition>();
        private int condition_iterator = 0;
        private bool desc=true;

        public UniversalComparer(string sortString, bool nullValueIsSmallest)
        {
            SortString = sortString;
            _nullValueIsSmallest = nullValueIsSmallest;
            ParseSortCondition();
        }

        private void ParseSortCondition()
        {
            List<string> list = SortString.Split(' ').ToList();

            foreach (var s in list)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("--------");

            for (int i=0; i<list.Count; i++)
            {
                Condition tempCondition = null;
                if (list[i].Contains(','))
                {
                    list[i] = list[i].Remove(list[i].Length-1);
                    if (list[i + 1].Contains("desc") && (!list[i].Contains("desc") || list[i].Length>4))
                    {
                        tempCondition = new Condition();
                        tempCondition.Desc=true;
                        tempCondition.ConditionParametr = list[i];
                    }
                    else
                    {
                        if (list[i].Length!=4)
                        {
                            tempCondition = new Condition();
                            tempCondition.Desc = false;
                            tempCondition.ConditionParametr = list[i];
                        }
                        else
                        {
                            if (!list[i].Contains("desc"))
                            {
                                tempCondition = new Condition();
                                tempCondition.Desc = false;
                                tempCondition.ConditionParametr = list[i];
                            }
                        }
                    }
                }
                else
                {
                    if (!(i + 1 > list.Count - 1))
                    {
                        if (list[i + 1].Contains("desc"))
                        {
                            tempCondition = new Condition();
                            tempCondition.Desc = true;
                            tempCondition.ConditionParametr = list[i];
                        }
                    }
                    else
                    {
                        if (!(list[i].Contains("desc") && list[i].Length==4))
                        {
                            tempCondition = new Condition();
                            tempCondition.Desc = false;
                            tempCondition.ConditionParametr = list[i];
                        }
                    }
                }

                if (list[i].Contains('.'))
                {
                    if (!(i + 1 > list.Count - 1))
                    {
                        // var tempStr = list[i].Split('.');
                        if (list[i + 1].Contains("desc"))
                        {
                            tempCondition = new Condition();
                            tempCondition.Desc = true;
                            tempCondition.ConditionParametr = list[i]; //tempStr[0];
                        }
                    }
                };
                if(tempCondition!=null) Conditions.Add(tempCondition);
            }

            foreach (var condition in Conditions)
            {
                Console.WriteLine(condition.ConditionParametr + " " + condition.Desc);
            }

        }

        public int Test(object x, object y)
        {
            Type myType = x.GetType();
            List<FieldInfo> props = myType.GetFields().ToList();

            //foreach (FieldInfo prop in props)
            //{
            //    Console.WriteLine(prop.GetValue(x) == null ? "null" : prop.GetValue(x));
            //}
            //Console.WriteLine("--------");

            foreach (var condition in Conditions)
            {
                object value1, value2;
                if (condition.ConditionParametr.Contains('.'))
                {
                    value1 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr.Split('.')[0]).GetValue(x);
                    value2 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr.Split('.')[0]).GetValue(y);
                }
                else
                {
                    value1 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr).GetValue(x);
                    value2 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr).GetValue(y);
                }

                Console.WriteLine(value1.GetType().ToString() + "---" + value1==null ? "null" : value1);
                switch (value1.GetType().ToString())
                {
                    case "System.String": Console.WriteLine("s="+(string)value1); break;
                    case "System.Int32": Console.WriteLine("i=" + (int)value1);  break;
                    default: Console.WriteLine("data="+((PropertyInfo)InnerValue(value1, condition.ConditionParametr)).GetValue(value1)); break;
                }
            }
           
            return 0;
        }

        public object InnerValue(object obj, string str)
        {
            List<PropertyInfo> props = obj.GetType().GetProperties().ToList();
            object value = props.SingleOrDefault(p => p.Name == str);
            //if (value is null)
            //{              
            //    List<FieldInfo> fields = obj.GetType().GetFields().ToList();
            //    value = fields.SingleOrDefault(p => p.Name == str.Split('.')[0]);
            //    if (value != null) return InnerValue(obj, str.Substring(str.IndexOf('.') + 1));
            //}
            if (value != null)
                return value;
            var some = InnerValue(obj,
                str.Substring(str.IndexOf('.') + 1));
            return some;
        }

        public int Test2(object x, object y)
        {
            try
            {
                if (Comparer.Default.Compare(x, y) == 0)
                {
                    return 0;                   
                }
                else
                {
                    int result;
                    if (desc) result = Comparer.Default.Compare(x, y) * -1;
                    else result = Comparer.Default.Compare(x, y);
                    if (x is null || y is null)
                    {
                        if (_nullValueIsSmallest) return result*-1;
                        return  result;
                    }
                    return result;
                }
            }
            catch
            {
                // ignored
            }

            Type myType = x.GetType();
            List<FieldInfo> fields = myType.GetFields().ToList();
            List<PropertyInfo> props = myType.GetProperties().ToList();
            
            foreach (var condition in Conditions)
            {
                desc = condition.Desc;
                object value1 = null, value2 = null;
                if (condition.ConditionParametr.Contains('.'))
                {
                    if (props.Count > 0)
                    {
                        value1 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(x);
                        value2 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(y);
                    }
                    if (value1 == null) //
                        if (fields.Count > 0)
                        {
                            value1 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(x);
                            value2 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(y);
                        }
                    if (value1 != null || value2 !=null) //
                    {
                        int result = Test2(value1, value2);
                        if (condition_iterator + 1 == Conditions.Count && result == 0)
                        {
                            return 0;
                        }
                        if (result != 0) return result;
                    }
                }
                else
                {
                    if (props.Count > 0)
                    {
                        value1 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr)?.GetValue(x);
                        value2 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr)?.GetValue(y);
                    }
                    if(value1==null) //
                        if (fields.Count > 0)
                        {
                            value1 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr)?.GetValue(x);
                            value2 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr)?.GetValue(y);
                        }
                    if (value1 != null || value2 != null) //
                    {
                        int result = Test2(value1, value2);
                        if (condition_iterator + 1 == Conditions.Count && result==0)
                        {
                            return 0;
                        }
                        if(result!=0) return result;
                    }                  
                }
                condition_iterator++;
            }
            throw new Exception("Error");
        }

        public int Compare(object x, object y)
        {
            return  Comparer.Default.Compare(x, y);
        }

    }
}
