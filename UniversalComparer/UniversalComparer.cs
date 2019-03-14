using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UniversalComparer
{
    public class UniversalComparer : IComparer<object>
    {
        private string SortString { get; }

        private readonly bool _nullValueIsSmallest;
        public List<Condition> Conditions = new List<Condition>();
        private int _conditionIterator;
        private bool _desc;

        public UniversalComparer(string sortString, bool nullValueIsSmallest)
        {
            SortString = sortString;
            _nullValueIsSmallest = nullValueIsSmallest;
            ParseSortCondition();
        }

        private void ParseSortCondition()
        {
            List<string> list = SortString.Split(' ').ToList();

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
                        if (list[i + 1].Contains("desc"))
                        {
                            tempCondition = new Condition();
                            tempCondition.Desc = true;
                            tempCondition.ConditionParametr = list[i];
                        }
                    }
                }
                if(tempCondition!=null) Conditions.Add(tempCondition);
            }
        }

        public int Compare(object x, object y)
        {
            try
            {
                if (Comparer.Default.Compare(x, y) == 0)
                {
                    return 0;
                }
                int result;
                if (_desc) result = Comparer.Default.Compare(x, y) * -1;
                else result = Comparer.Default.Compare(x, y);
                if (x is null || y is null)
                {
                    if (_nullValueIsSmallest) return result * -1;
                    return result;
                }
                return result;
            }
            catch
            {
                // ignored
            }

            // ReSharper disable once PossibleNullReferenceException
            Type myType = x.GetType();
            List<FieldInfo> fields = myType.GetFields().ToList();
            List<PropertyInfo> props = myType.GetProperties().ToList();

            foreach (var condition in Conditions)
            {
                _desc = condition.Desc;
                object value1 = null, value2 = null;
                if (condition.ConditionParametr.Contains('.'))
                {
                    if (props.Count > 0)
                    {
                        value1 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(x);
                        value2 = props.SingleOrDefault(p => p.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(y);
                    }
                    if (value1 == null && value2 == null)
                        if (fields.Count > 0)
                        {
                            value1 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(x);
                            value2 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr.Split('.')[0])?.GetValue(y);
                        }
                    if (value1 != null || value2 != null)
                    {
                        string temp = condition.ConditionParametr;
                        condition.ConditionParametr = temp.Substring(temp.IndexOf('.') + 1);
                        int result = Compare(value1, value2);
                        if (_conditionIterator + 1 == Conditions.Count && result == 0)
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
                    if (value1 == null && value2 == null)
                        if (fields.Count > 0)
                        {
                            value1 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr)?.GetValue(x);
                            value2 = fields.SingleOrDefault(f => f.Name == condition.ConditionParametr)?.GetValue(y);
                        }
                    if (value1 != null || value2 != null)
                    {
                        int result = Compare(value1, value2);
                        if (_conditionIterator + 1 == Conditions.Count && result == 0)
                        {
                            return 0;
                        }
                        if (result != 0) return result;
                    }
                }
                _conditionIterator++;
            }
            return 0;
        }

    }
}
