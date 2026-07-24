using UnityEngine;

public class Summon : ScriptableObject
{
    public SO_Battler Template { get; private set; }
    
    public class SummonMaker
    {

        private SO_Battler template = null;

        public SummonMaker WithTemplate(SO_Battler template)
        {
            this.template = template;
            return this;
        }
        
        public Summon Make()
        {
            var summon = new Summon
            {
                Template = template,
                

            };
            return summon;
        }
    }
}