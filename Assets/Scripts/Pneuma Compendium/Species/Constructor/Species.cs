using UnityEngine;

public class Species
{
    public int MaxHealth { get; private set; }
    public Type FirstType { get; private set; }
    public Type SecondType { get; private set; }
    public int Initiative { get; private set; }
    public int Aptitude { get; private set; }
    public string Pneuma { get; private set; }
    public Move[] LVLOne { get; private set; }
    public Move[] LVLTwo { get; private set; }
    public Move[] LVLThree { get; private set; }
    public Move[] LVLFour { get; private set; }
    public Move[] LVLFive { get; private set; }

    public class Builder
    {
        private string pneuma = "unknown";
        private int maxHealth = 50;
        private int initiative = 5;
        private int aptitude = 3;
        private Type firstType = Type.LEAD;
        private Type secondType = Type.NONE;
        private Move[] one = null;
        private Move[] two = null;
        private Move[] three = null;
        private Move[] four = null;
        private Move[] five = null;

        public Builder WithPneuma(string pneuma)
        {
            this.pneuma = pneuma;
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
        public Builder WithAptitude(int aptitude)
        {
            this.aptitude = aptitude;
            return this;
        }
        public Builder WithOne(Move[] one)
        {
            this.one = one;
            return this;
        }
        public Builder WithTwo(Move[] two)
        {
            this.two = two;
            return this;
        }
        public Builder WithThree(Move[] three)
        {
            this.three = three;
            return this;
        }
        public Builder WithFour(Move[] four)
        {
            this.four = four;
            return this;
        }
        public Builder WithFive(Move[] five)
        {
            this.five = five;
            return this;
        }
        public Species Build()
        {
            var species = new Species
            {
                Pneuma = pneuma,
                Initiative = initiative,
                Aptitude = aptitude,
                MaxHealth = maxHealth,
                FirstType = firstType,
                SecondType = secondType,
                LVLOne = one,
                LVLTwo = two,
                LVLThree = three,
                LVLFour = four,
                LVLFive = five

            };
            return species;
        }
    }
}
