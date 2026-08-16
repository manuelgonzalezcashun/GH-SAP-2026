using UnityEngine;

public class InOverworldState : GameState
{
    public override void EnterState()
    {
        Debug.Log("Enter overworld!");
        EventBus.Raise(new PlayAudioEvent { audioEffect = GameManager.Instance.OverworldTheme });
    }
    public override void ExitState()
    {
        Debug.Log("Exit overworld!");
        EventBus.Raise(new StopAudioEvent { audioEffect = GameManager.Instance.OverworldTheme });
    }
}
