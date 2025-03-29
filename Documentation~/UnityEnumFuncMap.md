# UnityEnumFuncMap Example

The UnityEnumFuncMap utility extends EnumFuncMap to support Unity's 
serialization and inspector integration, allowing you to manipulate 
enum-to-function mappings directly in the Unity Editor.

## Example

```
public enum GameState { Start, Play, Pause, End }
public class Game : MonoBehaviour 
{ 
    [SerializeField] 
    private UnityEnumFuncMap<GameState, Action> _stateActions;

    private void Awake()
    {
        _stateActions = new UnityEnumFuncMap<GameState, Action>(new Action[]
        {
            StartGame,
            PlayGame,
            PauseGame,
            EndGame
        });
    }

    public void SetState(GameState state)
    {
        _stateActions.Enum = state;
        _stateActions.Func();
    }

    private void StartGame() { Debug.Log("Game Started"); }
    private void PlayGame() { Debug.Log("Game Playing"); }
    private void PauseGame() { Debug.Log("Game Paused"); }
    private void EndGame() { Debug.Log("Game Ended"); }
}
```