using System.Collections.Generic;

namespace UniversalComparerLibrary
{
    public class Condition
    {
        public Queue<string> Parameters { get; set; } = new Queue<string>();
        public bool Desc { get; set; }

    }
}
