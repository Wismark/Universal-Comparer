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
                int result;
                if (Comparer.Default.Compare(x, y) == 0) return 0;               
                if (_desc) result = Comparer.Default.Compare(x, y) * -1;
                else result = Comparer.Default.Compare(x, y);
                if (!(x is null) && !(y is null)) return result;
                if (_nullValueIsSmallest) return result * -1;
                return result;
            }
            catch
            {
                // ignored
            }
            // ReSharper disable once PossibleNullReferenceException
            var fields = x.GetType().GetFields().ToList();
            var props = x.GetType().GetProperties().ToList();

            foreach (var condition in Conditions)
            {
                _desc = condition.Desc;
                object value1 = null, value2 = null;
                string conditionParameter = condition.ConditionParametr;

                if (condition.ConditionParametr.Contains('.')) conditionParameter = condition.ConditionParametr.Split('.')[0];
                if (props.Count > 0)
                {
                    value1 = props.SingleOrDefault(p => p.Name == conditionParameter)?.GetValue(x);
                    value2 = props.SingleOrDefault(p => p.Name == conditionParameter)?.GetValue(y);
                }
                if (value1 == null && value2 == null && fields.Count > 0)
                {
                    value1 = fields.SingleOrDefault(f => f.Name == conditionParameter)?.GetValue(x);
                    value2 = fields.SingleOrDefault(f => f.Name == conditionParameter)?.GetValue(y);
                }

                if (value1 == null && value2 == null) continue;           
                condition.ConditionParametr = condition.ConditionParametr.Substring(condition.ConditionParametr.IndexOf('.') + 1);
                var result = Compare(value1, value2);
                if (Conditions.Last()==condition && result == 0)
                {
                    return 0;
                }
                if (result != 0) return result;
            }
            return 0;
        }

    }
}
