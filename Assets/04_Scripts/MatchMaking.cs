using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class MatchMaking : MonoBehaviour{
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject Complete;
    [SerializeField] private TMP_Text playerCountText;
    private int totalPlayers = 4;
    private int currentPlayers = 0;

    private void Start()
    {
        Loading.SetActive(true);
        Complete.SetActive(false);
        StartCoroutine(PlayerCount());
    }

    IEnumerator PlayerCount()
    {
        for (int i = 0; i < 5; i++) { 
            int playerCount = i;
            currentPlayers = playerCount;
            playerCountText.text = $"{currentPlayers}/{totalPlayers}";
            yield return new WaitForSeconds(1f); 
        };
        
        if (currentPlayers >= totalPlayers)
        {
            OnMatchComplete();
        }
        yield return null;
    }

    private void OnMatchComplete()
    {
        StartCoroutine(LoadingComplete());
    }

    IEnumerator LoadingComplete()
    {
        Loading.SetActive(false);
        Complete.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Test");
        StopCoroutine(LoadingComplete());
    }
}