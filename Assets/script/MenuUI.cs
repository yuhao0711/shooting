using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class XRMenuUI : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void OnStartGame()
    {
        string playerName = nameInput.text;
        PlayerPrefs.SetString("PlayerName", playerName); // �s�J���a�W
        SceneManager.LoadScene("GameScene"); // ����C������
    }

    public void OnQuitGame()
    {
        Application.Quit(); // ���}�C��
        Debug.Log("Quit");
    }
}
