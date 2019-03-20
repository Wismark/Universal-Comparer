using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparerLibrary
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

        //private void ParseSortConditionOld()
        //{
        //    List<string> list = SortString.Split(' ').ToList();

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        Condition tempCondition = null;
        //        if (list[i].Contains(','))
        //        {
        //            list[i] = list[i].Remove(list[i].Length - 1);
        //            if (list[i + 1].Contains("desc") && (!list[i].Contains("desc") || list[i].Length > 4))
        //            {
        //                tempCondition = new Condition();
        //                tempCondition.Desc = true;
        //                tempCondition.ConditionParametr = list[i];
        //            }
        //            else
        //            {
        //                if (list[i].Length != 4)
        //                {
        //                    tempCondition = new Condition();
        //                    tempCondition.Desc = false;
        //                    tempCondition.ConditionParametr = list[i];
        //                }
        //                else
        //                {
        //                    if (!list[i].Contains("desc"))
        //                    {
        //                        tempCondition = new Condition();
        //                        tempCondition.Desc = false;
        //                        tempCondition.ConditionParametr = list[i];
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (!(i + 1 > list.Count - 1))
        //            {
        //                if (list[i + 1].Contains("desc"))
        //                {
        //                    tempCondition = new Condition();
        //                    tempCondition.Desc = true;
        //                    tempCondition.ConditionParametr = list[i];
        //                }
        //            }
        //            else
        //            {
        //                if (!(list[i].Contains("desc") && list[i].Length == 4))
        //                {
        //                    tempCondition = new Condition();
        //                    tempCondition.Desc = false;
        //                    tempCondition.ConditionParametr = list[i];
        //                }
        //            }
        //        }

        //        if (list[i].Contains('.'))
        //        {
        //            if (!(i + 1 > list.Count - 1))
        //            {
        //                if (list[i + 1].Contains("desc"))
        //                {
        //                    tempCondition = new Condition();
        //                    tempCondition.Desc = true;
        //                    tempCondition.ConditionParametr = list[i];
        //                }
        //            }
        //        }

        //        if (tempCondition != null) Conditions.Add(tempCondition);
        //    }
        //}


        private void ParseSortCondition()
        {
            var list = SortString.Split(',').ToList();

            for (int j = 0; j < list.Count; j++)
            {
                list[j] = list[j].Trim(); 

                var condition = new Condition();
                if (list[j].Split(' ').Length>1)
                {
                    if (list[j].Split(' ')[1] == "desc")
                    {
                        list[j] = list[j].Split(' ')[0];
                        condition.Desc = true;
                    }
                } 
                foreach (var param in list[j].Split('.').ToList())
                {
                    condition.Parameters.Enqueue(param);
                }
                Conditions.Add(condition);
            }

            //foreach (var condition in Conditions)
            //{
            //    foreach (var parametr in condition.Parameters)
            //    {
            //        Console.WriteLine(parametr);
            //    }
            //    Console.WriteLine("Desc:" + condition.Desc + "\n ---");
            //}
        }

        //public void ParseSort2()
        //{
        //    var list = SortString.Split(' ').ToList();

        //    for (int j = 0; j < list.Count; j++)
        //    {
        //        if (list[j].Contains(',')) list[j]=list[j].Remove(list[j].Length-1);
        //        Console.WriteLine(list[j]);
        //    }

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        Condition tempCondition = null;

        //        if (!(i + 1 > list.Count - 1))
        //        {
        //            if (list[i + 1].Contains("desc"))
        //            {
        //                tempCondition = new Condition();
        //                tempCondition.Desc = true;
        //                tempCondition.ConditionParametr = list[i];
        //            }
        //        }
        //        else
        //        {
        //            if (!(list[i].Contains("desc") && list[i].Length == 4))
        //            {
        //                tempCondition = new Condition();
        //                tempCondition.Desc = false;
        //                tempCondition.ConditionParametr = list[i];
        //            }
        //        }

        //        if (list[i].Contains('.'))
        //        {
        //            if (!(i + 1 > list.Count - 1))
        //            {
        //                if (list[i + 1].Contains("desc"))
        //                {
        //                    tempCondition = new Condition();
        //                    tempCondition.Desc = true;
        //                    tempCondition.ConditionParametr = list[i];
        //                }
        //            }
        //        }

        //        if (tempCondition != null) Conditions.Add(tempCondition);
        //    }

        //    Console.WriteLine("------");
        //    foreach (var condition in Conditions)
        //    {
        //        Console.WriteLine(condition.ConditionParametr + " " + condition.Desc);
        //    }
        //}

        public int Compare(object x, object y)
        {
            int result=0;
            foreach (var condition in Conditions)
            {
                var value1 = GetObjectInnerValue(x, new Queue<string>(condition.Parameters));
                var value2 = GetObjectInnerValue(y, new Queue<string>(condition.Parameters));

                result=Comparer.Default.Compare(value1, value2);

                if (result == 0 && condition != Conditions.Last()) continue;
                if (condition.Desc) result *= -1;
                if ((value1 == null || value2 == null) && _nullValueIsSmallest) result *= -1;
                if (result != 0) return result;
            }
            return result;
        }

        public object GetObjectInnerValue(object obj, Queue<string> pQue)
        {
            if(obj is null) return null;
            if (pQue.Count == 0) return obj;
            object result = null;

            var fields = obj.GetType().GetFields().ToList();
            var props = obj.GetType().GetProperties().ToList();

            if (props.Count > 0)
            {
                result = props.SingleOrDefault(p => p.Name == pQue.Peek())?.GetValue(obj);
            }
            if (fields.Count > 0 && result is null)
            {
                result = fields.SingleOrDefault(p => p.Name == pQue.Peek())?.GetValue(obj);
            }

            pQue.Dequeue();
            result = GetObjectInnerValue(result, pQue);

            return result;
        }
    }  
}
