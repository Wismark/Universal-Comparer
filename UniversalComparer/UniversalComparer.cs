using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparer
{
    public class UniversalComparer : IComparer<object>
    {
        private string SortString { get; }

        private readonly bool _nullValueIsSmallest;
        public List<Condition> Conditions = new List<Condition>();

        public UniversalComparer(string sortString, bool nullValueIsSmallest)
        {
            SortString = sortString;
            _nullValueIsSmallest = nullValueIsSmallest;
            ParseSortCondition();
        }

        private void ParseSortCondition()
        {
            List<string> list = SortString.Split(' ').ToList();

            for (int i = 0; i < list.Count; i++)
            {
                Condition tempCondition = null;
                if (list[i].Contains(','))
                {
                    list[i] = list[i].Remove(list[i].Length - 1);
                    if (list[i + 1].Contains("desc") && (!list[i].Contains("desc") || list[i].Length > 4))
                    {
                        tempCondition = new Condition();
                        tempCondition.Desc = true;
                        tempCondition.ConditionParametr = list[i];
                    }
                    else
                    {
                        if (list[i].Length != 4)
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
                        if (!(list[i].Contains("desc") && list[i].Length == 4))
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

                if (tempCondition != null) Conditions.Add(tempCondition);
            }
        }

        public void ParseSortConditionReborn()
        {
            var list = SortString.Split(' ').ToList();

            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].Contains(',')) list[j]=list[j].Remove(list[j].Length-1);
                Console.WriteLine(list[j]);
            }

            for (int i = 0; i < list.Count; i++)
            {
                Condition tempCondition = null;

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
                    if (!(list[i].Contains("desc") && list[i].Length == 4))
                    {
                        tempCondition = new Condition();
                        tempCondition.Desc = false;
                        tempCondition.ConditionParametr = list[i];
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

                if (tempCondition != null) Conditions.Add(tempCondition);
            }

            Console.WriteLine("------");
            foreach (var condition in Conditions)
            {
                Console.WriteLine(condition.ConditionParametr + " " + condition.Desc);
            }
        }

        public int Compare(object x, object y)
        {
            int result=0;
            foreach (var condition in Conditions)
            {
                var value1 = GetObjectInnerValue(x, condition.ConditionParametr);
                var value2 = GetObjectInnerValue(y, condition.ConditionParametr);

                result=Comparer.Default.Compare(value1, value2);

                if (result == 0 && condition != Conditions.Last()) continue;
                if (condition.Desc) result *= -1;
                if ((value1 == null || value2 == null) && _nullValueIsSmallest) result *= -1;
                if (result != 0) return result;
            }
            return result;
        }

        public object GetObjectInnerValue(object obj, string paramName)
        {
            if(obj is null) return null;

            object result = null;

            var fields = obj.GetType().GetFields().ToList();
            var props = obj.GetType().GetProperties().ToList();

            if (paramName.Contains('.'))
            {
                if (props.Count > 0) result = props.SingleOrDefault(p => p.Name == paramName.Split('.')[0])?.GetValue(obj);
                if (fields.Count > 0 && result is null) result = fields.SingleOrDefault(p => p.Name == paramName.Split('.')[0])?.GetValue(obj);
                result = GetObjectInnerValue(result, paramName.Substring(paramName.IndexOf('.') + 1));
            }
            else
            {
                if (props.Count > 0) result = props.SingleOrDefault(p => p.Name == paramName)?.GetValue(obj);
                if (fields.Count > 0 && result is null) result = fields.SingleOrDefault(p => p.Name == paramName)?.GetValue(obj);
            }

            return result;
        }
    }  
}
