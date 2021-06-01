using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace DeliveryRush
{
    public class Building : MonoBehaviour
    {
        public GameObjectType buildingType;

        // занято ли уже какое-то место для спавна
        public bool[] freePositions;
        
        private void Start()
        {
            freePositions = new bool[Positions[buildingType].Length];
            for (var i = 0; i < freePositions.Length; i++)
                freePositions[i] = true;
            // => изначально все возможные точки для спавна пусты.
        }
      
        // Каждому типу зданий соответсвуют подходящие позиции для препятствий.
        public static readonly Dictionary<GameObjectType, PositionInfo[]> Positions
            = new Dictionary<GameObjectType, PositionInfo[]>
            {
                /* RESTAURANT */
                {GameObjectType.McDonalds, new [] {
                    new PositionInfo(
                        new Vector3(8.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.4),
                        0),
                    new PositionInfo(
                        new Vector3(1f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Car, 0.7),
                        1)
                }},
                
                /* DOWNTOWN */
                // {GameObjectType.Boutique1, new PositionInfo[0]},
                {GameObjectType.Boutique, new PositionInfo[0]},
                {GameObjectType.DodoPizza, new PositionInfo[0]},
                {GameObjectType.AlmaMater, new[] {
                    new PositionInfo(
                        new Vector3(7,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.7),
                        0),
                    new PositionInfo(
                        new Vector3(3f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.1),
                        1),
                    new PositionInfo(
                        new Vector3(7f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Scooter, 0.8),
                        2)
                }},
                {GameObjectType.SkyScraper1, new[] {
                    new PositionInfo(
                        new Vector3(2f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.08),
                        0),
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Car, 0.2),
                        1),
                    new PositionInfo(
                        new Vector3(4f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Scooter, 0.6),
                        2)
                }},
                
                /* RESIDENTIAL */
                {GameObjectType.Flat1, new[] {
                    new PositionInfo(
                        new Vector3(4,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.7),
                        0),
                    new PositionInfo(
                        new Vector3(1,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.05),
                        1),
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.ConvStore1, 0.1),
                        2),
                    new PositionInfo(
                        new Vector3(0,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.ConvStore2, 0.2),
                        3)
                        
                }},
                {GameObjectType.Flat2, new[] {
                        new PositionInfo(
                        new Vector3(4,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.05),
                        0),
                        new PositionInfo(
                            new Vector3(1.5f,0,0),
                            new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.1),
                            1),
                        new PositionInfo(
                            new Vector3(3f,0,0),
                            new PositionInfo.ObstacleInfo(GameObjectType.Scooter, 0.3),
                            2)
                        
                }},
                {GameObjectType.Flat3, new[] {
                    new PositionInfo(
                        new Vector3(1,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.5),
                        0),
                    new PositionInfo(
                        new Vector3(1.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.1),
                        1),
                    new PositionInfo(
                        new Vector3(1.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Car, 0.3),
                        2)
                }},
                {GameObjectType.Flat4, new[] {
                    new PositionInfo(
                        new Vector3(4,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.01),
                        0),
                    new PositionInfo(
                        new Vector3(1.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.05),
                        1),
                    new PositionInfo(
                        new Vector3(3f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Scooter, 0.5),
                        2)
                        
                }},
                {GameObjectType.Flat5, new[] {
                    new PositionInfo(
                        new Vector3(4,Pigeons.FlightHeight,0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.Pigeon, 0.05),
                        0),
                    new PositionInfo(
                        new Vector3(1.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.4),
                        1)
                        
                }},
                {GameObjectType.ConvStore1, new[] {
                    new PositionInfo(
                        new Vector3(2, 0, 0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.3),
                        0),
                        
                }},
                {GameObjectType.ConvStore2, new[] {
                    new PositionInfo(
                        new Vector3(1, 0, 0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.2),
                        0),
                        
                }},
                {GameObjectType.ConvStore3, new[] {
                    new PositionInfo(
                        new Vector3(0, 0, 0), 
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.2),
                        0),
                        
                }},
                
                /* POOR */
                {GameObjectType.PoorFlat1, new[] {
                    new PositionInfo(
                        new Vector3(2.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.TrashCan, 0.4),
                        0)
                }},
                
                /* YARD */
                {GameObjectType.Yard1, new [] {
                    new PositionInfo(
                        new Vector3(3.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.EndTrigger, 1),
                        0)
                }},
                {GameObjectType.Yard2, new [] {
                    new PositionInfo(
                        new Vector3(3.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.EndTrigger, 1),
                        0)
                }},
                
                /* CROSSROAD */
                {GameObjectType.CrossRoad, new []
                {
                    new PositionInfo(
                        new Vector3(0.5f,0,0),
                        new PositionInfo.ObstacleInfo(GameObjectType.Car, 0.8),
                        0)
                }},
            };

        private readonly Random _rand = new Random();
        
        public void GenerateObstacles()
        {
            Start();

            // var rand = new Random();
            foreach (var position in Positions[buildingType])
            {
                var a = _rand.NextDouble();
                if (a <= position.Info.SpawnProbability && freePositions[position.PositionNumber])
                {
                    // спавним препятствие и занимаем позицию
                    freePositions[position.PositionNumber] = false;
                }
            }
        }

        public class PositionInfo
        {
            // Position is relative to it's parent's building spawnPoint
            public Vector3 RelativePosition;
            public readonly ObstacleInfo Info;
            public readonly int PositionNumber; // порядковый номер в списке для некоторого типа здания. не должен повторяться в списке в пределе одного здания

            public PositionInfo(Vector3 relativePosition, ObstacleInfo info, int positionNumber = -1)
            {
                RelativePosition = relativePosition;
                Info = info;
                PositionNumber = positionNumber;
            }

            public class ObstacleInfo
            {
                public readonly GameObjectType ObType;
                public readonly double SpawnProbability;
                
                public ObstacleInfo(GameObjectType obstacleType, double spawnProbability = 0)
                {
                    ObType = obstacleType;
                    SpawnProbability = spawnProbability;
                }
            }
        }
    }
}