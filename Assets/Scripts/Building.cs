using System;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] protected List<Obstacle> _obstacles = new List<Obstacle>();

        // все возможные точки спавна чего-либо
        // нужно для того, чтобы не заспавнить на одном и том же месте два предмета
        // TODO: для каждого здания добавить точку для полёта голубей!!!
        private static readonly Dictionary<BuildingType, Vector3[]> Positions
            = new Dictionary<BuildingType, Vector3[]>
            {
                {BuildingType.Flat1, new []
                {
                    new Vector3(0, 0, 0)
                }},
                {BuildingType.Flat2, new []
                {
                    new Vector3(0, 0, 0)
                }}
            };
        
        // занято ли уже какое-то место для спавна
        private readonly Dictionary<Vector3, bool> _freePositions;

        public Building(BuildingType buildingType)
        {
            _buildingType = buildingType;
            
            _freePositions = new Dictionary<Vector3, bool>();
            foreach (var position in Positions[_buildingType])
                _freePositions.Add(position, true);
            // => изначально все возможные точки для спавна пусты.
        }

        // TODO: откорректировать значения!!!
        // для голубей задать высоту из класса
        
        /* Каждому типу зданий соответсвуют подходящие позиции для препятствий. */
        private static readonly Dictionary<BuildingType, ObstacleInfo[]> _possibleObstaclePositions
            = new Dictionary<BuildingType, ObstacleInfo[]>
            {
                {BuildingType.Flat1, new []
                {
                        new ObstacleInfo(ObstacleType.Pigeon, new []
                        {
                            new ObstacleInfo.PositionInfo(BuildingType.Flat1, 0, 0.2)
                        }),
                        new ObstacleInfo(ObstacleType.TrashCan, new []
                        {
                            new ObstacleInfo.PositionInfo(BuildingType.Flat1, 1, 0.2)
                        })
                }},
                
                {BuildingType.Flat2, new []
                {
                    new ObstacleInfo(ObstacleType.Pigeon, new []
                    {
                        new ObstacleInfo.PositionInfo(BuildingType.Flat2, 0, 0.1)
                    })
                }},
                
                /*{BuildingType.ConvStore1, new ObstacleInfo[0]},
                {BuildingType.ConvStore2, new ObstacleInfo[0]},
                {BuildingType.ConvStore3, new ObstacleInfo[0]},*/
            };
        
        void GenerateBuilding()
        {
            if (_buildingType == BuildingType.None) return;
            
            
        }

        private class ObstacleInfo
        {
            // ObType должен соответствовать единственный PossiblePositions[] 
            // в пределе одного типа зданий.
            
            public ObstacleType ObType;
            public PositionInfo[] PossiblePositions;

            public ObstacleInfo(ObstacleType obType, PositionInfo[] possiblePositions)
            {
                ObType = obType;
                PossiblePositions = possiblePositions;
            }
            
            public class PositionInfo
            {
                public Vector3 Position;
                // public int PositionNumber; // не уверена, что может пригодиться, но на всякий путь будет
                public double SpawnProbability;

                public PositionInfo(BuildingType buildingType, int positionNumber, double spawnProbability)
                {
                    // positionNumber -- номер позиции спавна из списка, соответсвующему некоторому
                    // типу здания
                    Position = Positions[buildingType][positionNumber];
                    SpawnProbability = spawnProbability;
                }
            }
        }
    }
}