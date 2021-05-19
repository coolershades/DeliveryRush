using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = System.Random;

namespace DeliveryRush
{
    public class MapGenerator
    {
        // Temporary values, I'll have to change them soon
        
        private const int MIN_MAP_LENGTH = 10;
        private const int MAX_MAP_LENGTH = 15;
        
        private const int MinAreaLength = 2;
        private const int MaxAreaLength = 3;

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

        private static readonly Dictionary<AreaType, GameObjectType[]> PossibleBuildings
            = new Dictionary<AreaType, GameObjectType[]>
            {
                {AreaType.Restaurant, new [] {GameObjectType.Restaurant1}},
                {AreaType.Downtown, new [] {GameObjectType.Boutique1}},
                {AreaType.Residential, new [] {GameObjectType.Flat1, GameObjectType.Flat2, 
                    GameObjectType.ConvStore1, GameObjectType.ConvStore2, GameObjectType.ConvStore3}},
                {AreaType.Poor, new [] {GameObjectType.PoorFlat1}},
                {AreaType.Yard, new [] {GameObjectType.Yard1}},
                {AreaType.CrossRoad, new [] {GameObjectType.CrossRoad}}
            };
        
        public static AreaType[] GenerateAreaTypeMap()
        {
            var map = new List<AreaType>();
            var r = new Random();
            // var mapLength = r.Next(MIN_MAP_LENGTH, MAX_MAP_LENGTH);
            map.Add(AreaType.Restaurant);

            var order = new List<AreaType> {AreaType.Downtown, AreaType.Residential, AreaType.Poor, AreaType.Residential};
            foreach (var area in order)
            {
                var areaLength = r.Next(MinAreaLength, MaxAreaLength);
                // map.AddMultiple(area, areaLength);
                map.Add(area);
                map.Add(AreaType.CrossRoad); // TODO!!! добавляется последний лишний переход
            }
            map.Add(AreaType.Yard);

            return map.ToArray();
        }

        public static List<GameObjectType> GenerateBuildingTypeArea(AreaType areaType)
        {
            var rand = new Random();
            var areaLength = rand.Next(MinAreaLength, MaxAreaLength);

            if (areaType == AreaType.Restaurant || areaType == AreaType.Yard || areaType == AreaType.CrossRoad)
                areaLength = 1;
            
            var result = new List<GameObjectType>();

            for (var i = 0; i < areaLength; i++)
            {
                var index = rand.Next(0, PossibleBuildings[areaType].Length - 1);
                var type = PossibleBuildings[areaType][index];
                result.Add(type);
            }
            
            return result;
        }

        public static GameObjectType[] GenerateTypeMap(AreaType[] map)
        {
            var result = new List<GameObjectType>();
            for (var i = 0; i < map.Length; i++)
            {
                var areaMap = GenerateBuildingTypeArea(map[i]);
                result.AddRange(areaMap);
            }
            
            return result.ToArray();
        }
    }
}