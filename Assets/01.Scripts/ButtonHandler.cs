using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public string playSceneName = "Play_Scene"; // 이동할 씬 이름 (씬 이름 입력 또는 Inspector에서 지정)

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(playSceneName);
    }
}