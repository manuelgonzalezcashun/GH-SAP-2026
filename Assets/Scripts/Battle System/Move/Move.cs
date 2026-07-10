using System;
using UnityEngine;

[Serializable]
public class Move
{
    // Getters //
    public int Damage { get; private set; }
    //number damage the move does, // // negative for healing moves
    public int Healing { get; private set; }
    // Amount of healing the move does 
    public int Distance { get; private set; }
    //0 is same row (close range), 1 is normal range, 2 is ranged, and -1 is self
    public bool Row { get; private set; }
    //determining if it affects a whole row of creatures
    public bool AlliesAffected { get; private set; }
    // true means it effects everyone, false means it only hits enemies
    public string Type { get; private set; }
    //the type
    //the move's real name (for moves that increase in power over time) i.e Slam lvl 1 vs Slam lvl 3
    public string Name { get; private set; }
    //the display name
    public string Desc { get; private set; }
    //the description/explanation of the move

    //public Effect effect {get; private set; }
    // ^to be added later but will encompass forced movement, poison, and other weird things


    public class Maker
    {

        private int Damage = 2;
        private int Healing = 0;
        private int Distance = 1;
        private bool Row = false;
        private bool AlliesAffected = false;

        private string Type = "Lead";
        private string Name = "Slam";
        private string Desc = "A normal punch";

        public Maker WithName(string name)
        {
            this.Name = name;
            return this;
        }
        public Maker WithType(string type)
        {
            this.Type = type;
            return this;
        }
        public Maker WithDesc(string desc)
        {
            this.Desc = desc;
            return this;
        }
        public Maker WithDamage(int damage)
        {
            this.Damage = damage;
            return this;
        }
        public Maker WithHealing(int healing)
        {
            this.Healing = healing;
            return this;
        }
        public Maker WithDistance(int distance)
        {
            this.Distance = distance;
            return this;
        }
        public Maker WithRow(bool row)
        {
            this.Row = row;
            return this;
        }
        public Maker WithAllyHit(bool ally)
        {
            this.AlliesAffected = ally;
            return this;
        }
        public Move Make()
        {
            var move = new Move
            {
                Name = Name,
                Type = Type,
                Desc = Desc,
                Damage = Damage,
                Healing = Healing,
                Distance = Distance,
                Row = Row,
                AlliesAffected = AlliesAffected,

            };
            return move;
        }
    }
}