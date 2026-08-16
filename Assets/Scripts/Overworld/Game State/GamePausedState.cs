using UnityEngine;

public class GamePausedState : GameState
{
    public override void EnterState()
    {
        InputHandler.ChangeActionMaps(InputHandler.menuInput);
        Time.timeScale = 0f;
    }
    public override void ExitState()
    {
        InputHandler.ChangeActionMaps(InputHandler.playerInput);
        Time.timeScale = 1f;
    }
}
