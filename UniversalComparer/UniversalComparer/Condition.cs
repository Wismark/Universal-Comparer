using System.Collections.Generic;

namespace UniversalComparer.UniversalComparer
{
    public class Condition
    {
        public Queue<string> Parameters { get; set; } = new Queue<string>();
        public bool Desc { get; set; }

    }
}
