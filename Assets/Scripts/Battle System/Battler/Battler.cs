using System;
using UnityEngine;

[Serializable]
public class Battler
{
    public event Action<float, float> onHealthChanged;

    // Getters //
    public float Health { get; private set; }
    public float MaxHealth { get; private set; }
    public float Healing { get; private set; }
    public int Power { get; private set; }
    public float Initiative { get; private set; }
    public string Name { get; private set; }
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

    public class Builder
    {
        // Battler Stats //
        private float maxHealth = 50;
        private int power = 50;
        private float initiative = 5;
        private float healing = 50;
        private float health = -1;

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
        public Builder WithPower(int power)
        {
            this.power = power;
            return this;
        }
        public Builder WithInitiative(float initiative)
        {
            this.initiative = initiative;
            return this;
        }
        public Builder WithHealing(float healing)
        {
            this.healing = healing;
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
        public Battler Build()
        {
            var battler = new Battler
            {
                Name = name,
                Color = color,
                Power = power,
                Initiative = initiative,
                Healing = healing,
                MaxHealth = maxHealth,
                Health = health
            };
            return battler;
        }
    }
}

public enum Team { PLAYER, OPPONENT }