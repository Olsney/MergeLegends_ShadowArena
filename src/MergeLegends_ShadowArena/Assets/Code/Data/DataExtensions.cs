using UnityEngine;

namespace Code.Data
{
    public static class DataExtensions
    {
        public static float SqrMagnitudeTo(this Vector3 from, Vector3 to) 
            => Vector3.SqrMagnitude(to - from);
        
        public static string ToJson<T>(this T obj) where T : class
            => JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}