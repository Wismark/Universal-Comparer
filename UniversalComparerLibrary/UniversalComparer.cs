using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversalComparerLibrary
{
    public class UniversalComparer : IComparer<object>
    {
        private readonly bool _nullValueIsSmallest;
        public List<Condition> Conditions = new List<Condition>();

        public UniversalComparer(string sortString, bool nullValueIsSmallest)
        {
            _nullValueIsSmallest = nullValueIsSmallest;
            ParseSortCondition(sortString);
        }

        private void ParseSortCondition(string sortString)
        {
            var list = sortString.Split(',').ToList();

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

        }


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
