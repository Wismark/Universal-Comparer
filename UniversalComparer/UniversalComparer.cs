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
        private Type Type { get; set; }

        public List<Condition> Conditions = new List<Condition>();

        public UniversalComparer(string sortString)
        {
            SortString = sortString;
            ParseSortCondition();
        }

        private void ParseSortCondition()
        {
            List<string> list = SortString.Split(' ').ToList();

            //foreach (var s in list)
            //{
            //    Console.WriteLine(s);
            //}

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
                        tempCondition.desc=true;
                        tempCondition.conditionParametr = list[i];
                    }
                    else
                    {
                        if (list[i].Length!=4)
                        {
                            tempCondition = new Condition();
                            tempCondition.desc = false;
                            tempCondition.conditionParametr = list[i];
                        }
                        else
                        {
                            if (!list[i].Contains("desc"))
                            {
                                tempCondition = new Condition();
                                tempCondition.desc = false;
                                tempCondition.conditionParametr = list[i];
                            }
                        }
                    }
                }
                else
                {
                    if(!(i+1>list.Count-1))
                    if (list[i + 1].Contains("desc"))
                    {
                        tempCondition = new Condition();
                        tempCondition.desc = true;
                        tempCondition.conditionParametr = list[i];
                    }
                }

                if (list[i].Contains('.'))
                {
                  //  var tempStr = list[i].Split('.');
                    if (list[i + 1].Contains("desc"))
                    {
                        tempCondition = new Condition();
                        tempCondition.desc = true;
                        tempCondition.conditionParametr = list[i];  //tempStr[tempStr.Length-1];
                    }
                };
                if(tempCondition!=null) Conditions.Add(tempCondition);
            }

            foreach (var condition in Conditions)
            {
                Console.WriteLine(condition.conditionParametr + " " + condition.desc);
            }

        }

        public int Test(object x, object y)
        {
            Type myType = x.GetType();
            IList<FieldInfo> props = myType.GetFields();

            string a;
            //foreach (FieldInfo prop in props)
            //{
            //    Console.WriteLine(prop.GetValue(x) == null ? "null" : prop.GetValue(x));
            //}
            //Console.WriteLine("--------");

            foreach (var condition in Conditions)
            {
                var value1 = (string)props.Single(p => p.Name == condition.conditionParametr).GetValue(x);
                var value2 = (string)props.Single(p => p.Name == condition.conditionParametr).GetValue(y);

                return String.Compare(value1, value2, StringComparison.Ordinal);
            }

            return 0;
        }

        public int Compare(object x, object y)
        {
            x.GetType().GetProperty("FirstName").GetValue(x);
            throw new NotImplementedException();
        }

   
    }

    class Condition
    {
        public string conditionParametr;
        public bool desc;
    }
}
