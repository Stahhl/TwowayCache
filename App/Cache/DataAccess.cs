using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Cache
{
    public static class DataAccess
    {
        public static async Task<string> GetIdBySerial(string key)
        {
            await Task.Delay(1500);
            return IdBySerial[key];
        }
        public static async Task<string> GetSerialById(string key)
        {
            await Task.Delay(1500);
            return SerialByID[key];
        }
        private static Dictionary<string, string> IdBySerial = new Dictionary<string, string>()
        {
            { "A100", "100" },
            { "A200", "200" },
            { "A300", "300" },
        };
        private static Dictionary<string, string> SerialByID = new Dictionary<string, string>()
        {
            { "100", "A100" },
            { "200", "A200" },
            { "300", "A300" },
        };
    }
}
