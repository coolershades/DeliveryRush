using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace DeliveryRush
{
    public static class MapGenerator
    {
        // Temporary values, I'll have to change them soon
        private const int MinAreaLength = 3;
        private const int MaxAreaLength = 6;

        private static readonly Random rand = new Random();

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
                {AreaType.Restaurant, new []
                {
                    GameObjectType.McDonalds
                }},
                {AreaType.Downtown, new [] 
                {
                    // GameObjectType.Boutique1, 
                    GameObjectType.Boutique, 
                    GameObjectType.DodoPizza, GameObjectType.AlmaMater,
                    GameObjectType.SkyScraper1
                }},
                {AreaType.Residential, new [] 
                {
                    GameObjectType.Flat1, GameObjectType.Flat2, 
                    GameObjectType.Flat3, GameObjectType.Flat4, 
                    GameObjectType.Flat5, GameObjectType.ConvStore1, 
                    GameObjectType.ConvStore2, GameObjectType.ConvStore3
                }},
                {AreaType.Poor, new []
                {
                    GameObjectType.PoorFlat1, GameObjectType.Flat1
                }},
                {AreaType.Yard, new []
                {
                    GameObjectType.Yard1,
                    GameObjectType.Yard2
                }},
                {AreaType.CrossRoad, new []
                {
                    GameObjectType.CrossRoad
                }}
            };

        public static Dictionary<AreaType, GameObjectType[]> AreaBackground
            = new Dictionary<AreaType, GameObjectType[]>
            {
                {AreaType.Restaurant, new GameObjectType[0]},
                {AreaType.Downtown, new GameObjectType[0]},
                {AreaType.Residential, new []
                {
                    GameObjectType.ResBack1, GameObjectType.ResBack2, GameObjectType.ResBack3
                }},
                {AreaType.Poor, new []
                {
                    GameObjectType.PoorBack1, GameObjectType.PoorBack2, GameObjectType.PoorBack3
                }},
                {AreaType.Yard, new GameObjectType[0]},
                {AreaType.CrossRoad, new GameObjectType[0]}
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

        private static List<GameObjectType> GenerateBuildingTypeArea(AreaType areaType)
        {
            var areaLength = rand.Next(MinAreaLength, MaxAreaLength);

            if (areaType == AreaType.Restaurant || areaType == AreaType.Yard || areaType == AreaType.CrossRoad)
                areaLength = 1;
            
            var result = new List<GameObjectType>();

            for (var i = 0; i < areaLength; i++)
            {
                var index = rand.Next(0, PossibleBuildings[areaType].Length);
                var type = PossibleBuildings[areaType][index];
                result.Add(type);
            }
            
            return result;
        }
        
        public static List<Tuple<AreaType, List<GameObjectType>>> GenerateTypeMap(AreaType[] map)
        {
            var result = new List<Tuple<AreaType, List<GameObjectType>>>();
            for (var i = 0; i < map.Length; i++)
            {
                var areaMap = GenerateBuildingTypeArea(map[i]);
                result.Add(Tuple.Create(map[i], areaMap));
            }
            
            return result;
        }
    }
}