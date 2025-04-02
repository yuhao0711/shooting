using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine.SceneManagement;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private float shotPower = 500f;
    [SerializeField] private float ejectPower = 150f;

    [Header("Gameplay")]
    public int totalBullets = 10;
    private int currentBullets;
    private int currentScore = 0;

    [Header("UI")]
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI scoreText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI leaderboardText;

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        currentBullets = totalBullets;
        UpdateUI();

        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentBullets > 0)
        {
            gunAnimator?.SetTrigger("Fire");
            Shoot();
            FireRay();
            currentBullets--;
            UpdateUI();

            if (currentBullets <= 0)
            {
                ShowGameOver();
            }
        }
    }

    public void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            GameObject tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            Destroy(tempFlash, destroyTimer);
        }

        if (bulletPrefab)
        {
            GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(barrelLocation.forward * shotPower);
            }
            Destroy(bullet, destroyTimer);
        }

        CasingRelease();
    }

    void FireRay()
    {
        Ray ray = new Ray(barrelLocation.position, barrelLocation.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            ScoreZone zone = hit.collider.GetComponent<ScoreZone>();
            if (zone != null)
            {
                int score = zone.GetScore();
                currentScore += score;
                Debug.Log("Score:" + score);
                UpdateUI();
            }
        }

        Debug.DrawRay(barrelLocation.position, barrelLocation.forward * 100f, Color.red, 1f);
    }

    void CasingRelease()
    {
        if (!casingExitLocation || !casingPrefab)
            return;

        GameObject tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
        Rigidbody rb = tempCasing.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower),
                (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            rb.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
        }

        Destroy(tempCasing, destroyTimer);
    }

    void UpdateUI()
    {
        if (bulletText != null)
            bulletText.text = $"Bullet:{currentBullets}/{totalBullets}";

        if (scoreText != null)
            scoreText.text = $"Score:{currentScore}";
    }
    void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = $"Final Score: {currentScore}";
        }

        SaveScore(currentScore);
        ShowLeaderboard();
    }
    void SaveScore(int score)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");

        string json = PlayerPrefs.GetString("ScoreList", "");
        ScoreList list = string.IsNullOrEmpty(json) ? new ScoreList() : JsonUtility.FromJson<ScoreList>(json);

        list.entries.Add(new ScoreEntry { name = playerName, score = score });

        list.entries = list.entries.OrderByDescending(e => e.score).Take(10).ToList();

        string newJson = JsonUtility.ToJson(list);
        PlayerPrefs.SetString("ScoreList", newJson);
        PlayerPrefs.Save();
    }


    void ShowLeaderboard()
    {
        string json = PlayerPrefs.GetString("ScoreList", "");
        if (string.IsNullOrEmpty(json) || leaderboardText == null) return;

        ScoreList list = JsonUtility.FromJson<ScoreList>(json);

        string display = "Leaderboard\n";
        int rank = 1;
        foreach (var entry in list.entries)
        {
            display += $"{rank}. {entry.name} - {entry.score}\n";
            rank++;
        }

        leaderboardText.text = display;
    }

}



[System.Serializable]
public class ScoreEntry
{
    public string name;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public List<ScoreEntry> entries = new List<ScoreEntry>();
}
