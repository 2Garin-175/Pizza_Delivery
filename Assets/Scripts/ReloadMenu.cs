using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void OpenReloadMenu(int score)
    {
        scoreText.text = score.ToString();
        gameObject.SetActive(true);
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
