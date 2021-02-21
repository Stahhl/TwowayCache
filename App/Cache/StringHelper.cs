namespace App.Cache
{
    public static class StringHelper
    {
        public static string AddPrefix(KeyType type, string key)
        {
            return GetPrefix(type) + key;
        }
        public static string GetPrefix(KeyType type)
        {
            return type == KeyType.A ? "a_" : "b_";
        }
    }
}
