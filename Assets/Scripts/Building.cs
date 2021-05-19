using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace DeliveryRush
{
    public class Building : MonoBehaviour
    {
        public GameObjectType BuildingType;
        // [SerializeField] private GameObjectType _buildingType;
        
        // [SerializeField] protected List<Obstacle> _obstacles = new List<Obstacle>();

        public Building(GameObjectType buildingType)
        {
            BuildingType = buildingType;
            
            FreePositions = new bool[Positions[BuildingType].Length];
            for (var i = 0; i < FreePositions.Length; i++)
                FreePositions[i] = true;
            // => изначально все возможные точки для спавна пусты.
        }

        // занято ли уже какое-то место для спавна
        public bool[] FreePositions;
        
        private void Start()
        {
            FreePositions = new bool[Positions[BuildingType].Length];
            for (var i = 0; i < FreePositions.Length; i++)
                FreePositions[i] = true;
            // => изначально все возможные точки для спавна пусты.
        }
        
        // TODO: откорректировать значения!!! здесь пока только временные
        /* Каждому типу зданий соответсвуют подходящие позиции для препятствий. */
        public static readonly Dictionary<GameObjectType, PositionInfo[]> Positions
            = new Dictionary<GameObjectType, PositionInfo[]>
            {
                /* RESTAURANT */
                {GameObjectType.Restaurant1, new [] {
                    new PositionInfo(
                        new Vector3(0,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.5),
                        0),
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Car, 0.2),
                        1)
                }},
                
                /* DOWNTOWN */
                {GameObjectType.Boutique1, new PositionInfo[0]},
                
                /* RESIDENTIAL */
                {GameObjectType.Flat1, new []
                {
                    new PositionInfo(
                        new Vector3(0,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.9),
                        0),
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 1),
                        1)
                }},
                {GameObjectType.Flat2, new []
                {
                    new PositionInfo(
                        new Vector3(0,Pigeons.FlightHeight,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.1),
                        0),
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 1),
                        1)
                }},
                {GameObjectType.ConvStore1, new PositionInfo[0]},
                {GameObjectType.ConvStore2, new PositionInfo[0]},
                {GameObjectType.ConvStore3, new PositionInfo[0]},
                
                /* POOR */
                {GameObjectType.PoorFlat1, new PositionInfo[] {
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 1),
                        0)
                }},
                
                /* YARD */
                {GameObjectType.Yard1, new PositionInfo[0]},
                
                /* CROSSROAD */
                {GameObjectType.CrossRoad, new PositionInfo[0]},
            };
        
        public void GenerateObstacles()
        {
            Start();
            
            var rand = new Random();
            foreach (var position in Positions[BuildingType])
            {
                var a = rand.NextDouble();
                if (a <= position.Info.SpawnProbability && FreePositions[position.PositionNumber])
                {
                    // спавним препятствие и занимаем позицию
                    FreePositions[position.PositionNumber] = false;
                }
            }
        }

        public class PositionInfo
        {
            // public BuildingType BuildType; 
            
            // Position is relative to it's parent's building spawnPoint
            public Vector3 RelativePosition;
            public ObstacleInfo Info;
            public int PositionNumber; // порядковый номер в списке для некоторого типа здания. не должен повторяться в списке в пределе одного здания

            public PositionInfo(Vector3 relativePosition, ObstacleInfo info, int positionNumber = -1)
            {
                RelativePosition = relativePosition;
                Info = info;
                PositionNumber = positionNumber;
            }

            public class ObstacleInfo
            {
                // ObstacleType можно заменить на Obstacle, будет в разы удобнее спавнить
                // public ObstacleType ObType;
                public GameObjectType ObType;
                public double SpawnProbability;

                /*public ObstacleInfo(ObstacleType obstacleType, double spawnProbability = 0)
                {
                    ObType = obstacleType;
                    SpawnProbability = spawnProbability;
                }*/
                
                public ObstacleInfo(GameObjectType obstacleType, double spawnProbability = 0)
                {
                    ObType = obstacleType;
                    SpawnProbability = spawnProbability;
                }
            }
        }
    }
}