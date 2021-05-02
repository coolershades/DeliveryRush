using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace DefaultNamespace
{
    public enum Area
    {
        Restaurant,
        Downtown,
        Residential,
        Poor,
        Yard
    }

    public class MapGenerator
    {
        // Temporary values, I'll have to change them soon
        
        private const int MIN_MAP_LENGTH = 10;
        private const int MAX_MAP_LENGTH = 15;
        
        private const int MinAreaLength = 3;
        private const int MaxAreaLength = 6;

        // TODO!!!
        /*private static readonly Dictionary<Tuple<Area, Area>, double> TransitionsProbabilities = 
            new Dictionary<Tuple<Area, Area>, double>
            {
                {Tuple.Create(Area.Downtown, Area.Residential), 1},
                {Tuple.Create(Area.Residential, Area.Poor), 0.6},
                {Tuple.Create(Area.Residential, Area.Poor), 0.2},
                {Tuple.Create(Area.Poor, Area.Residential), 0.8},
            };
        
        private static double TransitionProbability(Area first, Area second)
        {
            var pair = Tuple.Create(first, second);
            if (TransitionsProbabilities.ContainsKey(pair))
                return TransitionsProbabilities[pair]; 
            return 0;
        }
        
        private static readonly Dictionary<Area, List<Area>> PossibleTransitions = 
            new Dictionary<Area, List<Area>>()
            {
                {Area.Restaurant, new List<Area> {Area.Downtown}},
                {Area.Downtown, new List<Area> {Area.Residential}},
                {Area.Residential, new List<Area> {Area.Downtown, Area.Poor}},
            };
        
        public static List<Area> GenerateRandomMap()
        {
            throw new NotImplementedException();
            var map = new List<Area>();
            
            var r = new Random();
            var mapLength = r.Next(MIN_MAP_LENGTH, MAX_MAP_LENGTH);
            map.Add(Area.Restaurant);

            var areaLength = r.Next(MinAreaLength, MaxAreaLength);
            
            
            map.Add(Area.Yard);
            return map;
        }*/

        public static List<Area> GenerateMap()
        {
            var map = new List<Area>();
            var r = new Random();
            // var mapLength = r.Next(MIN_MAP_LENGTH, MAX_MAP_LENGTH);
            map.Add(Area.Restaurant);

            var order = new List<Area> {Area.Downtown, Area.Residential, Area.Poor, Area.Residential};
            foreach (var area in order)
            {
                var areaLength = r.Next(MinAreaLength, MaxAreaLength);
                map.AddMultiple(area, areaLength);
            }
            map.Add(Area.Yard);

            return map;
        }
    }

    public static class ListExtensions
    {
        public static void AddMultiple<TItem>(this List<TItem> list, TItem item, int repeats)
        {
            for (var i = 0; i < repeats; i++)
                list.Add(item);
        }

        public static string ToString(this List<Area> map)
        {
            var s = new StringBuilder();
            foreach (var area in map)
            {
                s.Append(area.ToString()).Append(" ");
            }

            return s.ToString();
        }
    }
}