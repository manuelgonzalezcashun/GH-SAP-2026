
public class InBattleState : GameState
{
    public override void EnterState()
    {
        EventBus.Raise(new StopAudioEvent { audioEffect = GameManager.Instance.OverworldTheme });
    }
}
