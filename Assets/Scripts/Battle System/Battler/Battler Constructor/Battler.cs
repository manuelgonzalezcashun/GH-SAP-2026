using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;


public enum Type { NONE, SALT, SULFUR, MERCURY, LEAD, PHOSPHORUS, ANTIMONY, BISMUTH, ARSENIC }
public enum Stack { NONE, ARMOR, WEAKNESS }

[Serializable]
public class Battler
{
    public Species Species { get; private set; }
    public event Action<float, float> onHealthChanged;
    // Getters //
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public Type FirstType { get; private set; }
    public Type SecondType { get; private set; }
    public Move[] Moves { get; private set; }
    public int Initiative { get; private set; }
    public string Name { get; private set; }
    public string DisplayName { get; private set; }
    private int Row;
    private int Armor_STK = 0;
    private int Weakness_STK = 0;
    [SerializeField] private StatusEffectsUI statusEffectsUI;
    private Dictionary<StatusType, int> statusEffects = new();
    // public Color Color { get; private set; }
    public Sprite Sprite { get; private set; }
    public Team Team { get; private set; }
    public int Aptitude { get; private set; }

    public bool TakeDamage(int damage, Type type)
    {
        if (damage == 0)
        {
            return Health <= 0;
        }

        EventBus.Raise(new DamageUnitEvent { battler = this });
        damage = LoopTypes(FirstType, type, damage);
        damage = LoopTypes(SecondType, type, damage);

        damage = StackCalcs(damage);
        Health -= damage;
        Health = Health < 0 ? 0 : Health;

        onHealthChanged?.Invoke(Health, MaxHealth);
        Weakness_STK = 0;
        Armor_STK = 0;
        return Health <= 0;
    }
    public void ChangeStack(int amt, Stack type)
    {
        if (type == Stack.ARMOR)
        {
            Armor_STK += amt;
        }
        else if (type == Stack.WEAKNESS)
        {
            Weakness_STK += amt;
        }
    }
    public void Heal(int healing)
    {
        Health += healing;
        Health = Health < MaxHealth ? Health : MaxHealth;

        onHealthChanged?.Invoke(Health, MaxHealth);
    }
    public void SetTeam(Team team) => Team = team;
    public void SetDisplayName(string _name) => DisplayName = _name;

    public int getRow()
    {
        return Row;
    }
    public void setRow(int newrow)
    {
        Row = newrow;
    }
    public Team GetTeam()
    {
        return Team;
    }

    public class Builder
    {
        // Battler Stats //
        private Species species = null;
        private int maxHealth = 50;
        private int initiative = 5;
        private int health = -1;
        private int aptitude = 3;
        private Move[] moves = null;
        private Type firstType = Type.LEAD;
        private Type secondType = Type.NONE;

        // Battler Display //
        private string name = "Foo";
        // private Color color = Color.softRed;
        private Sprite sprite = null;

        public Builder WithSpecies(Species species)
        {
            this.species = species;
            return this;
        }
        public Builder WithName(string name)
        {
            this.name = name;
            return this;
        }
        public Builder WithSprite(Sprite sprite)
        {
            this.sprite = sprite;
            return this;
        }
        public Builder WithInitiative()
        {
            initiative = species.Initiative;
            return this;
        }
        public Builder WithAptitude()
        {
            aptitude = species.Aptitude;
            return this;
        }
        public Builder WithMaxHealth()
        {
            maxHealth = species.MaxHealth;
            return this;
        }
        public Builder WithHealth(int currentHealth)
        {
            health = currentHealth;
            return this;
        }
        public Builder WithHealth()
        {
            health = maxHealth;
            return this;
        }
        public Builder WithMoves(Move[] moves)
        {
            this.moves = moves;
            return this;
        }
        public Builder WithFirstType()
        {
            firstType = species.FirstType;
            return this;
        }
        public Builder WithSecondType()
        {
            secondType = species.SecondType;
            return this;
        }
        public Battler Build()
        {
            var battler = new Battler
            {
                Name = name,
                DisplayName = name,
                Sprite = sprite,
                Initiative = initiative,
                MaxHealth = maxHealth,
                Health = health,
                Moves = moves,
                FirstType = firstType,
                SecondType = secondType,
                Aptitude = aptitude

            };
            return battler;
        }
    }

    public void AddStatus(StatusType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] += stackCount;
        }
        else
        {
            statusEffects.Add(type, stackCount);
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }
    public void RemoveStatus(StatusType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] -= stackCount;
            if (statusEffects[type] <= 0)
            {
                statusEffects.Remove(type);
            }
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }
    public int GetStatusEffectStacks(StatusType type)
    {
        if (statusEffects.ContainsKey(type)) return statusEffects[type];
        else return 0;
    }

    public int LoopTypes(Type def, Type atk, int damage)
    {
        if (def == Type.NONE || atk == Type.NONE)
        {
            return damage;
        }
        else if (def == Type.SALT)
        {
            if (atk == Type.SULFUR || atk == Type.ARSENIC)
            {

                return Weakness(damage);
            }
            else if (atk == Type.MERCURY || atk == Type.SALT)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.SULFUR)
        {
            if (atk == Type.MERCURY || atk == Type.PHOSPHORUS || atk == Type.BISMUTH)
            {
                return Weakness(damage);
            }
            else if (atk == Type.SALT || atk == Type.SULFUR)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.MERCURY)
        {
            if (atk == Type.SALT || atk == Type.MERCURY || atk == Type.LEAD)
            {
                return Weakness(damage);
            }
            else if (atk == Type.SULFUR)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.LEAD)
        {
            if (atk == Type.MERCURY || atk == Type.ARSENIC)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.PHOSPHORUS)
        {
            if (atk == Type.SULFUR || atk == Type.ANTIMONY)
            {
                return Weakness(damage);
            }
            else if (atk == Type.BISMUTH || atk == Type.ARSENIC)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.ANTIMONY)
        {
            if (atk == Type.PHOSPHORUS)
            {
                return Weakness(damage);
            }
            else if (atk == Type.BISMUTH)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.BISMUTH)
        {
            if (atk == Type.PHOSPHORUS || atk == Type.ANTIMONY)
            {
                return Weakness(damage);
            }
            else if (atk == Type.SULFUR)
            {
                return Resistance(damage);
            }
        }
        else if (def == Type.ARSENIC)
        {
            if (atk == Type.PHOSPHORUS || atk == Type.BISMUTH)
            {
                return Weakness(damage);
            }
            else if (atk == Type.SALT)
            {
                return Resistance(damage);
            }
        }

        return damage;
    }

    public int Weakness(int dam)
    {
        if (dam == 0)
        {
            return 0;
        }
        else
        {
            return dam + 1;
        }
    }
    public int Resistance(int dam)
    {
        if (dam - 1 >= 1)
        {
            return dam - 1;
        }
        else
        {
            if (dam == 0)
            {
                return 0;
            }
            else
            {
                return 1 + Weakness_STK - Armor_STK;
            }
        }
    }
    public int StackCalcs(int dam)
    {
        if (dam == 0)
        {
            return 0;
        }
        else
        {
            dam += Weakness_STK - Armor_STK;
            if (dam < 0)
            {
                return 0;
            }
            return dam;
        }
    }
}

public enum Team { PLAYER, OPPONENT }