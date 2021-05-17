using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace DeliveryRush
{
    public enum BuildingType
    {
        None,
        
        // Flats in residential area
        Flat1,
        Flat2,
            
        // Convenience stores (e.g. 5ka)
        ConvStore1,
        ConvStore2,
        ConvStore3
    }
    
    public class Building : MonoBehaviour
    {
        private readonly BuildingType _buildingType;
        // [SerializeField] protected List<Obstacle> _obstacles = new List<Obstacle>();

        public Building(BuildingType buildingType)
        {
            _buildingType = buildingType;
            
            _freePositions = new bool[Positions[_buildingType].Length];
            for (var i = 0; i < _freePositions.Length; i++)
                _freePositions[i] = true;
            // => изначально все возможные точки для спавна пусты.
        }

        // занято ли уже какое-то место для спавна
        private readonly bool[] _freePositions;
        
        // TODO: откорректировать значения!!! здесь пока только временные
        /* Каждому типу зданий соответсвуют подходящие позиции для препятствий. */
        private static readonly Dictionary<BuildingType, PositionInfo[]> Positions
            = new Dictionary<BuildingType, PositionInfo[]>
            {
                {BuildingType.Flat1, new []
                {
                    new PositionInfo(
                        new Vector3(0,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(ObstacleType.Pigeon, 0.2),
                        0),
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(ObstacleType.TrashCan, 0.2),
                        1)
                }},
                
                {BuildingType.Flat2, new []
                {
                    new PositionInfo(
                        new Vector3(0,Pigeons.FlightHeight,0),
                        new PositionInfo.ObstacleInfo(ObstacleType.Pigeon, 0.1),
                        0)
                }},
            };
        
        public Building GenerateObstacles()
        {
            if (_buildingType == BuildingType.None) return null;

            var rand = new Random();
            foreach (var position in Positions[_buildingType])
            {
                var a = rand.NextDouble();
                if (a <= position.Info.SpawnProbability && _freePositions[position.PositionNumber])
                {
                    // спавним препятствие и занимаем позицию
                    _freePositions[position.PositionNumber] = false;
                }
            }

            return this;
        }

        public class PositionInfo
        {
            // public BuildingType BuildType; 
            public Vector3 Position;
            public ObstacleInfo Info;
            public int PositionNumber; // порядковый номер в списке для некоторого типа здания. не должен повторяться в списке в пределе одного здания

            public PositionInfo(Vector3 position, ObstacleInfo info, int positionNumber = -1)
            {
                Position = position;
                Info = info;
                PositionNumber = positionNumber;
            }

            public class ObstacleInfo
            {
                // ObstacleType можно заменить на Obstacle, будет в разы удобнее спавнить
                public ObstacleType ObType;
                public double SpawnProbability;

                public ObstacleInfo(ObstacleType obstacleType, double spawnProbability = 0)
                {
                    ObType = obstacleType;
                    SpawnProbability = spawnProbability;
                }
            }
        }
    }
}