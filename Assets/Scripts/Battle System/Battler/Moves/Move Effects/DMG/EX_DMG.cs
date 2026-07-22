using UnityEngine;

public class EX_DMG : ScriptableObject
{
    public SO_Battler Template { get; private set; }
    
    public class ExtraMaker
    {

        private SO_Battler template = null;

        public ExtraMaker WithTemplate(SO_Battler template)
        {
            this.template = template;
            return this;
        }
        
        public EX_DMG Make()
        {
            var ex_dmg = new EX_DMG
            {
                Template = template,
                

            };
            return ex_dmg;
        }
    }
}
