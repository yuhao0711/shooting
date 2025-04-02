using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class XRMenuUI : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void OnStartGame()
    {
        string playerName = nameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName); // 存入玩家名
        SceneManager.LoadScene("GameScene"); // 切到遊戲場景
    }

    public void OnQuitGame()
    {
        Application.Quit(); // 離開遊戲
        Debug.Log("Quit");
    }
}
