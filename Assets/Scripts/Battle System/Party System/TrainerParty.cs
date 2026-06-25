using System.Linq;

public class TrainerParty : BattleParty
{
    public override Battler GetBattler()
    {
        var battler = Battlers.FirstOrDefault(b => b.Health > 0);
        if (battler == null) return null;

        return battler;
    }
}
