using System;
using UnityEngine;

public enum MoveCategory { DAMAGING, HEALING }
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

    public MoveCategory Category { get; private set; }
    //public Effect effect {get; private set; }
    // ^to be added later but will encompass forced movement, poison, and other weird things


    public class Maker
    {

        private int damage = 2;
        private int healing = 0;
        private int distance = 1;
        private bool row = false;
        private bool alliesAffected = false;

        private string type = "Lead";
        private string name = "Slam";
        private string desc = "A normal punch";
        private MoveCategory category = MoveCategory.DAMAGING;

        public Maker WithName(string name)
        {
            this.name = name;
            return this;
        }
        public Maker WithType(string type)
        {
            this.type = type;
            return this;
        }
        public Maker WithDesc(string desc)
        {
            this.desc = desc;
            return this;
        }
        public Maker WithDamage(int damage)
        {
            this.damage = damage;
            return this;
        }
        public Maker WithHealing(int healing)
        {
            this.healing = healing;
            return this;
        }
        public Maker WithDistance(int distance)
        {
            this.distance = distance;
            return this;
        }
        public Maker WithRow(bool row)
        {
            this.row = row;
            return this;
        }
        public Maker WithAllyHit(bool ally)
        {
            this.alliesAffected = ally;
            return this;
        }
        public Maker WithCategory(MoveCategory category)
        {
            this.category = category;
            return this;
        }
        public Move Make()
        {
            var move = new Move
            {
                Name = name,
                Type = type,
                Desc = desc,
                Damage = damage,
                Healing = healing,
                Distance = distance,
                Row = row,
                AlliesAffected = alliesAffected,
                Category = category
            };
            return move;
        }
    }
}