using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Cache
{
    public static class DataAccess
    {
        public static string GetValueForKeyA(string key)
        {
            return StringHelper.GetPrefix(KeyType.B) + key;
        }
        public static string GetValueForKeyB(string key)
        {
            return StringHelper.GetPrefix(KeyType.A) + key;
        }
    }
}
