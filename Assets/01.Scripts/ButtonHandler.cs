using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public string playSceneName = "Play_Scene"; // �̵��� �� �̸� (�� �̸� �Է� �Ǵ� Inspector���� ����)

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(playSceneName);
    }
}