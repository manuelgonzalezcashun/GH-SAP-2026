using UnityEngine;

public class Effect
{

    public int RowMove { get; private set; }
    //public SO_Status Status { get; private set; }
    public SO_EX_DMG DMG { get; private set; }
    public SO_Summon Summon { get; private set; }
    
    public class EffectMaker
    {

        private int rowMove = 0;
        //private SO_Status status = null;
        private SO_EX_DMG dmg = null;
        private SO_Summon summon = null;

        
        public EffectMaker WithRowMove(int rowMove)
        {
            this.rowMove = rowMove;
            return this;
        }
        // public EffectMaker WithStatus(Status status)
        // {
        //     this.status = status;
        //     return this;
        // }
        public EffectMaker WithDMG(SO_EX_DMG dmg)
        {
            this.dmg = dmg;
            return this;
        }
        public EffectMaker WithSummon(SO_Summon summon)
        {
            this.summon = summon;
            return this;
        }
        
        public Effect Make()
        {
            var effect = new Effect
            {
                RowMove = rowMove,
                //Status = status,
                DMG = dmg,
                Summon = summon

            };
            return effect;
        }
    }
}
