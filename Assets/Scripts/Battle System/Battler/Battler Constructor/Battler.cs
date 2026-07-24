using System;
using System.Collections.Generic;
using UnityEngine;


public enum Type { NONE,SALT,SULFUR,MERCURY,LEAD,PHOSPHORUS,ANTIMONY,BISMUTH,ARSENIC}
[Serializable]
public class Battler
{
    public event Action<float, float> onHealthChanged;
    // Getters //
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public Type FirstType { get; private set; }
    public Type SecondType { get; private set; }
    public Move[] Moves { get; private set; }
    public int Initiative { get; private set; }
    public string Name { get; private set; }
    private int Row;
    // public int aptitude { get; private set; }
    // for when customizing movesets is added, its the max amount of moves a creature can have
    [SerializeField] private StatusEffectsUI statusEffectsUI;
    private Dictionary<StatusType, int> statusEffects = new();
    public Color Color { get; private set; }
    public Team Team { get; private set; }

    public bool TakeDamage(int damage, Type type)
    {
        damage = LoopTypes(FirstType,type,damage);
        damage = LoopTypes(SecondType,type,damage);
        Health -= damage;
        Health = Health < 0 ? 0 : Health;

        onHealthChanged?.Invoke(Health, MaxHealth);

        return Health <= 0;
    }
    public void Heal(int healing)
    {
        Health += healing;
        Health = Health < MaxHealth ? Health : MaxHealth;

        onHealthChanged?.Invoke(Health, MaxHealth);
    }
    public void SetTeam(Team team) => Team = team;

    public int getRow()
    {
        return Row;
    }
    public void setRow(int newrow)
    {
        Row = newrow;
        Debug.Log(Row);
    }
    public Team GetTeam()
    {
        return Team;
    }

    public class Builder
    {
        // Battler Stats //
        private int maxHealth = 50;
        private int initiative = 5;
        private int health = -1;
        private Move[] moves = null;
        private Type firstType = Type.LEAD;
        private Type secondType = Type.NONE;

        // Battler Display //
        private string name = "Foo";
        private Color color = Color.softRed;

        public Builder WithName(string name)
        {
            this.name = name;
            return this;
        }
        public Builder WithColor(Color color)
        {
            this.color = color;
            return this;
        }
        public Builder WithInitiative(int initiative)
        {
            this.initiative = initiative;
            return this;
        }
        public Builder WithMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
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
        public Builder WithFirstType(Type type)
        {
            this.firstType = type;
            return this;
        }
        public Builder WithSecondType(Type type)
        {
            this.secondType = type;
            return this;
        }
        public Battler Build()
        {
            var battler = new Battler
            {
                Name = name,
                Color = color,
                Initiative = initiative,
                MaxHealth = maxHealth,
                Health = health,
                Moves = moves,
                FirstType = firstType,
                SecondType = secondType

            };
            return battler;
        }
    }

    public void AddStatus(StatusType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type]+= stackCount;
        }
        else
        {
            statusEffects.Add(type, stackCount);
        }
        statusEffectsUI.UpdateStatusEffectUI(type,GetStatusEffectStacks(type));
    }
    public void RemoveStatus(StatusType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type]-= stackCount;
            if (statusEffects[type] <=0)
            {
                statusEffects.Remove(type);
            }
        }
        statusEffectsUI.UpdateStatusEffectUI(type,GetStatusEffectStacks(type));
    }
    public int GetStatusEffectStacks(StatusType type)
    {
        if(statusEffects.ContainsKey(type)) return statusEffects[type];
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
                Debug.Log(damage);

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
            return dam+1;
        }
    }
    public int Resistance(int dam)
    {
        if (dam - 1 >= 1)
        {
            return dam-1;
        }
        else
        {
            if (dam == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}

public enum Team { PLAYER, OPPONENT }