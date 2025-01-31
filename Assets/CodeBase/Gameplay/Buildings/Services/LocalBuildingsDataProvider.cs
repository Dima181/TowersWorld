﻿using Cysharp.Threading.Tasks;
using Gameplay.Model;
using Infrastructure.Pipeline.DataProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Gameplay.Buildings.Services
{
    public class LocalBuildingsDataProvider : LocalDataProvider<BuildingsCollection>
    {
        private const string PathBullet_1 = "Prefabs/Gameplay/Bullets/Cannon Projectile.prefab";

        private readonly GameObject bullet_1 = Resources.Load<GameObject>(PathBullet_1);

        private static Dictionary<EBuilding, int> BASE_REQUIRE_1 = new() { { EBuilding.Base, 1 } };
        private static Dictionary<EBuilding, int> BASE_REQUIRE_2 = new() { { EBuilding.Base, 2 } };
        private static Dictionary<EBuilding, int> BASE_REQUIRE_3 = new() { { EBuilding.Base, 3 } };
        private static Dictionary<EBuilding, int> BASE_REQUIRE_4 = new() { { EBuilding.Base, 4 } };
        private static Dictionary<EBuilding, int> BASE_REQUIRE_5 = new() { { EBuilding.Base, 5 } };

        protected override async UniTask<BuildingsCollection> Load(
            DiContainer di,
            DisposableManager disposableManager)
        {
            await UniTask.CompletedTask;
            var result = new List<BuildingModel>();

            /*result.Add(CannonTower());*/
            result.Add(Base());

            /*foreach (var item in result)
            {
                Debug.Log(item.Id);
            }*/

            result.AddRange(new BuildingModel[]
            {
                Upgradable(BASE_REQUIRE_2, EBuilding.CannonTower, "CannonTower"),
                Upgradable(BASE_REQUIRE_2, EBuilding.SimpleTower, "SimpleTower")
            });

            return new BuildingsCollection(result.ToDictionary(builder => builder.Id));
        }


        private TimeSpan S(float seconds) =>
            TimeSpan.FromSeconds(seconds);
        private TimeSpan M(float minutes) =>
            TimeSpan.FromMinutes(minutes);
        private TimeSpan H(float hours) =>
            TimeSpan.FromHours(hours);
        private TimeSpan DHM(float days, float hours, float minutes) =>
            TimeSpan.FromMinutes((days * 24 + hours) * 60 + minutes);


        private BuildingModel Base()
        {
            TimeSpan[] durs = new TimeSpan[] { S(15), S(20), S(30), M(1.5f), M(3) };
            int[] gemsCost = new[] { 7, 10, 10, 10, 10 };

            BuildingStateConfig[] statesConfig = new[]
            {
                new BuildingStateConfig(110, 0.5f, 8, bullet_1, -0.5f),
                new BuildingStateConfig(120, 0.45f, 8, bullet_1, -0.45f),
                new BuildingStateConfig(130, 0.45f, 8, bullet_1, -0.45f),
                new BuildingStateConfig(140, 0.45f, 8, bullet_1, -0.45f),
                new BuildingStateConfig(150, 0.45f, 8, bullet_1, -0.45f),
            };

            BuildingResourcesConfig[] resourcesConfig = new[]
            {
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
            };

            return new BuildingBuilder()
                .WithId(EBuilding.Base)
                .WithName("Base")
                .InitialStay()
                .AddLevels(statesConfig.Length, statesConfig, resourcesConfig, gemsCost, durs, BaseRequiers(0, 5))
                .Build();
        }

        private BuildingModel CannonTower()
        {
            int[] health = new[] { 10, 15, 20, 25, 30 };
            float[] shootInterval = new[] { 0.5f, 0.45f, 0.4f, 0.35f, 0.3f };
            float[] range = new[] { 8f, 8.5f, 9f, 9.5f, 10f };

            // Поменять GameObject на что-то разумное 
            GameObject[] projectilePrefab = new[] { bullet_1, bullet_1, bullet_1, bullet_1, bullet_1 };

            float[] lastShotTime = new[] { -0.5f, -0.45f, -0.4f, -0.35f, -0.3f };
            TimeSpan[] durs = new TimeSpan[] { S(2), S(6), S(30), M(1.5f), M(3) };

            int[] ironCost = new[] { 5, 10, 15, 15, 15 };
            int[] meatCost = new[] { 5, 10, 15, 15, 15 };
            int[] geamsCost = new[] { 3, 3, 3, 3, 3 };

            BuildingStateConfig[] stateConfig = new[]
            {
                new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
            };
            
            BuildingResourcesConfig[] resourcesConfig = new[]
            {
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
                new BuildingResourcesConfig(10, 15),
            };

            /*for (int i = 0; i < health.Length; i++)
            {
                levels.Add(new BuildingLevelConfig(
                    new BuildingStateConfig(health[i], shootInterval[i], range[i], projectilePrefab[i], lastShotTime[i]),
                    new BuildingResourcesConfig(ironCost[i], meatCost[i]),
                    geamsCost[i],
                    durs[i],
                    BaseRequiers(1, 5)
            }*/

            return new BuildingBuilder()
                .WithId(EBuilding.CannonTower)
                .WithName("CannonTower")
                .InitialSite()
                .AddLevels(resourcesConfig.Length, stateConfig, resourcesConfig, geamsCost, durs, BaseRequiers(1, 5))
                .Build();

        }

        private Dictionary<EBuilding, int>[] BaseRequiers(int unlockLevelRequire, int count)
        {
            var result = new Dictionary<EBuilding, int>[count];
            result[0] = new Dictionary<EBuilding, int>() { { EBuilding.Base, unlockLevelRequire } };

            for (int i = 1; i < count; i++)
                result[i] = new Dictionary<EBuilding, int>() { { EBuilding.Base, unlockLevelRequire + i } };

            return result;
        }

        private BuildingModel Upgradable(
            IReadOnlyDictionary<EBuilding, int> unlockRequires,
            EBuilding building,
            string name,
            bool stay = false)
        {

            var productiveConfig = new BuildingConfig(new[]
            {
                /*new BuildingLevelConfig(10, 0.5f, 8, bullet_1, -0.5f, 3, M(1), unlockRequires),
                new BuildingLevelConfig(15, 0.45f, 8, bullet_1, -0.45f, 3, M(2), BASE_REQUIRE_1),
                new BuildingLevelConfig(20, 0.4f, 8, bullet_1, -0.4f, 3, M(3), BASE_REQUIRE_2),
                new BuildingLevelConfig(25, 0.35f, 8, bullet_1, -0.35f, 3, M(4), BASE_REQUIRE_3),
                new BuildingLevelConfig(30, 0.3f, 8, bullet_1, -0.3f, 3, M(5), BASE_REQUIRE_4)*/

                new BuildingLevelConfig(
                    new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                    new BuildingResourcesConfig(10, 15),
                    3,
                    S(5),
                    unlockRequires
                ),
                new BuildingLevelConfig(
                    new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                    new BuildingResourcesConfig(10, 15),
                    10,
                    M(1),
                    BASE_REQUIRE_1
                ), 
                new BuildingLevelConfig(
                    new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                    new BuildingResourcesConfig(10, 15),
                    10,
                    M(1),
                    BASE_REQUIRE_1
                ), 
                new BuildingLevelConfig(
                    new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                    new BuildingResourcesConfig(10, 15),
                    10,
                    M(1),
                    BASE_REQUIRE_1
                ), 
                new BuildingLevelConfig(
                    new BuildingStateConfig(10, 0.5f, 8, bullet_1, -0.5f),
                    new BuildingResourcesConfig(10, 15),
                    10,
                    M(1),
                    BASE_REQUIRE_1
                ), 

            }, EBuildingState.Site, 0);

            return new BuildingModel(
                building, 
                name, 
                "Some description", 
                productiveConfig,
                EBuildingState.Stay, 
                0, 
                null);
        }
    }
}
