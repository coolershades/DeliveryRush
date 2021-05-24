using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DeliveryRush
{
    public static class ListExtensions
    {
        public static void AddMultiple<TItem>(this List<TItem> list, TItem item, int repeats)
        {
            for (var i = 0; i < repeats; i++)
                list.Add(item);
        }

        public static string ToString(this List<AreaType> map)
        {
            var s = new StringBuilder();
            foreach (var area in map)
            {
                s.Append(area.ToString()).Append(" ");
            }

            return s.ToString();
        }
    }
    
    public static class GameObjectExtensions
    {
        public static float GetWidth(this GameObject gameObject)
            => ((RectTransform) gameObject.transform).rect.width;
        
        public static float GetHeight(this GameObject gameObject)
            => ((RectTransform) gameObject.transform).rect.height;
    }
    
    /*public static class ArrayExtensions
    {
        public static string ArrayToString(this bool[] arr)
        {
            var b = new StringBuilder();
            foreach (var obj in arr)
                b.Append(obj.ToString()).Append(" ");
            return b.ToString();
        }
    }*/
}