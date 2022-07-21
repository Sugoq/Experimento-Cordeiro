using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    
    public List<GameObject> levels = new List<GameObject>();
    private int currentLevel;

    private void Awake() => instance = this;

    private void OnDestroy() => instance = null;

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 0);
        transform.position = Vector3.zero;
        Instantiate(levels[currentLevel], transform.position, Quaternion.identity);
    }


    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", (currentLevel+1) % levels.Count);
        SceneManager.LoadScene(0);
        
    }



}
