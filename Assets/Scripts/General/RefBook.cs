using System.Collections;
using System.Collections.Generic;

namespace General
{
    public class RefBook
    {
        static Dictionary<string, object> masterBook 
            = new Dictionary<string, object>();

        public static void Register(string key, object refrence)
        {
            masterBook.Add(key, refrence);
        }

        public static object Summon(string key)
        {
            return masterBook[key];
        }
    }
}