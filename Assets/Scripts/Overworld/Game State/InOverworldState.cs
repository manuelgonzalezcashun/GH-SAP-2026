using UnityEngine;

public class InOverworldState : GameState
{
    public override void EnterState()
    {
        EventBus.Raise(new PlayAudioEvent { audioEffect = GameManager.Instance.OverworldTheme });
    }
}
