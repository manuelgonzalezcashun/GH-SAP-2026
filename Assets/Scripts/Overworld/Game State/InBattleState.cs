
public class InBattleState : GameState
{
    public override void EnterState()
    {
        // EventBus.Raise(new PlayAudioEvent { audioEffect = battleTheme });
        InputHandler.ChangeActionMaps(InputHandler.combatInput);
    }
    public override void ExitState()
    {
        EventBus.Raise(new EndBattleEvent());
        InputHandler.ChangeActionMaps(InputHandler.playerInput);
    }

}
