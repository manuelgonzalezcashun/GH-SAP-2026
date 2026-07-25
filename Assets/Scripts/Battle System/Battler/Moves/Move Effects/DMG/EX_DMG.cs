using UnityEngine;

public class EX_DMG : ScriptableObject
{
    public BonusDamageCalc Calc { get; private set; }
    
    public class ExtraMaker
    {

        private BonusDamageCalc calc = null;

        public ExtraMaker WithCalc(BonusDamageCalc calc)
        {
            this.calc = calc;
            return this;
        }
        
        public EX_DMG Make()
        {
            var ex_dmg = new EX_DMG
            {
                Calc = calc,
                

            };
            return ex_dmg;
        }
    }
}
