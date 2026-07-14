using System;
using UnityEngine;

[Serializable]
public class Battler
{
    public event Action<float, float> onHealthChanged;

    // Getters //
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }
    public Move[] Moves { get; private set; }
    public float Initiative { get; private set; }
    public string Name { get; private set; }
    private int Row;
    // public int aptitude { get; private set; }
    // for when customizing movesets is added, its the max amount of moves a creature can have
    public Color Color { get; private set; }
    public Team Team { get; private set; }

    public bool TakeDamage(int damage)
    {
        Health -= damage;
        Health = Health < 0 ? 0 : Health;

        onHealthChanged?.Invoke(Health, MaxHealth);

        return Health <= 0;
    }
    public void Heal(float healing)
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
        private float maxHealth = 50;
        private float initiative = 5;
        private float health = -1;
        private Move[] moves = null;

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
        public Builder WithInitiative(float initiative)
        {
            this.initiative = initiative;
            return this;
        }
        public Builder WithMaxHealth(float maxHealth)
        {
            this.maxHealth = maxHealth;
            return this;
        }
        public Builder WithHealth(float currentHealth)
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
        public Battler Build()
        {
            var battler = new Battler
            {
                Name = name,
                Color = color,
                Initiative = initiative,
                MaxHealth = maxHealth,
                Health = health,
                Moves = moves
            };
            return battler;
        }
    }
}

public enum Team { PLAYER, OPPONENT }