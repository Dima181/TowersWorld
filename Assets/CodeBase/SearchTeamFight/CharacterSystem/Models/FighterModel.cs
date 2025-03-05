using Heroes.Model;
using REST.Heroes.Models;
using SearchTeamFight.CharacterSystem.Models.Stats;
using SearchTeamFight.Data;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;
using Zenject;

namespace SearchTeamFight.CharacterSystem.Models
{
    [System.Diagnostics.DebuggerDisplay("{Data.ID}")]
    public class FighterModel
    {
        public readonly ReactiveCommand<int> OnCurrentHealthChanged = new();
        public readonly ReactiveCommand OnDied = new();
        public readonly ReactiveCommand<FighterModel> OnAttack = new();
        public readonly ReactiveCommand OnTeamWon = new();

        // Это эффекты (бафы/дебафы) управляет исполнением эффектов, таких как атаки, заклинания, баффы, дебаффы и т. д.
        /*public readonly ReactiveCommand<IEffect> PrimaryAbilityCasted = new();
        private readonly IEffect[] _newAbilities;*/

        private CancellationTokenSource _abilityUsageTokenSource;

        public Hero Data { get; set; }

        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public float AttackRange { get; private set; }
        public CancellationToken AbilityUsageTokenSource => _abilityUsageTokenSource.Token;

        public FighterFloatStat Attack { get; private set; }
        public FighterFloatStat AttackRate { get; private set; }
        private readonly FighterFloatStat _takingDamage = new(1f);

        public ReactiveProperty<FighterModel> Target { get; private set; } = new();
        public TypesTeam Team { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public bool IsCreep { get; }

        public FighterModel(
            EHero id, 
            TypesTeam team, 
            bool isCreep, 
            HeroesConfig heroesConfig, 
            HeroesModel heroesModel,
            LevelIndexHolder levelIndexHolder,
            AutoBattlerLevels autoBattlerLevels,
            DiContainer diContainer)
        {
            IsCreep = isCreep;

            if(team == TypesTeam.Enemy && !IsCreep)
            {
                var enemies = true // var enemies = HeroTestingToolView.IsInitialized - тут хз должно быть true когда все проинициализируется, но тот скрипт не вызывается 
                    ? autoBattlerLevels.TestEnemies
                    : autoBattlerLevels.TestLevels
                        .SelectMany(l => l.Enemies.Select(e => e.Hero))
                        .ToList();

                var enemyHero = enemies.First(e => e.ID == id);
                Data = enemyHero;
                Data.Initialize(heroesConfig);
            }
            else
            {
                if (isCreep)
                {
                    Data = new Hero(id);
                    /*Data.InitializeCreep(heroesConfig);*/
                }
                else
                {
                    Data = heroesModel.Heroes[id];

                    Data.Initialize(heroesConfig);
                }
            }

            MaxHealth = Data.Stats.Health;
            CurrentHealth = Data.Stats.Health;
            AttackRange = Data.Stats.AttackRange;
            Attack = new FighterFloatStat(Data.Stats.AttackRange);
            AttackRate = new FighterFloatStat(Data.Stats.AttackRate);
            Team = team;
        }

        public void SubstractHealth(
            int baseAttack,
            HeroStats attackerStats,
            HeroStats targetStats,
            int N = 15)
        {
            float attackBonus = attackerStats.ExpeditionAttackBonus;
            float A = baseAttack * (1 + attackBonus / 100);

            float defenseBonus = targetStats.ExpeditionDefenseBonus;
            float B = targetStats.Defense * (1 + defenseBonus / 100) / N;

            float damageBonus = attackerStats.ExpeditionAttackBonus;
            float C = 1 + damageBonus / 100;

            float damage = A * (1 - B / 100) * C;

            if (UnityEngine.Random.Range(0.0f, 100.0f) < attackerStats.CritRate)
            {
                damage *= attackerStats.CritScale;
            }

            int finalDamage = Mathf.RoundToInt(damage);

            SubtractHealth(finalDamage);
        }

        private void SubtractHealth(int amount)
        {
            CurrentHealth -= (int)(amount * _takingDamage.Value.Value);

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDied.Execute();
            }

            OnCurrentHealthChanged.Execute(CurrentHealth);
        }

        public void SetTarget(FighterModel target) => 
            Target.Value = target;
    }
}
