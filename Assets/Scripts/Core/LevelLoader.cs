using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance = null;
    public int currentLevelId;
    public float transitionDuration = 0.5f;
    [HideInInspector] public PlayerData playerSavedData;
    public LevelMetaDataCollection levelCollection;

    public LevelMetaData levelMetaData { get; private set; }

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a CollisionMatrix.
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLevelId = SceneManager.GetActiveScene().buildIndex;
        levelMetaData = levelCollection.GetLevelBySceneBuildIndex(currentLevelId);

        // trigger animation for start of level
        SceneChangeCircle.instance.SceneStarts();

        if (GameManager.instance != null)
        {
            GameManager.instance.BlockPlayer(transitionDuration);
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevelId < SceneManager.sceneCountInBuildSettings)
        {
            int nextLevelId = currentLevelId + 1;
            UnlockLevel(nextLevelId);

            LoadLevel(nextLevelId);
        }
    }

    public void LoadPreviousLevel()
    {
        if (currentLevelId > 0)
        {
            LoadLevel(currentLevelId - 1);
        }
    }

    public void ReloadLevel()
    {
        // prevent action from player
        GameManager.instance.playerCanMove = false;
        LoadLevel(currentLevelId);
    }

    public void LoadLevel(int levelID)
    {
        SceneChangeCircle.instance.SceneEnds();
        StartCoroutine(DelayLoadScene(levelID, transitionDuration));
    }

    public void LoadFirstScene()
    {
        LoadLevel(0);
    }

    public void LoadLevelSelectMenu()
    {
        int sceneId = SceneManager.sceneCountInBuildSettings - 1;
        LoadLevel(sceneId);
    }

    public bool IsLevelUnlocked(int levelId)
    {
        return playerSavedData.IsUnlocked(levelId);
    }

    public List<int> GetUnlockedLevels()
    {
        return playerSavedData.GetUnlockedLevels();
    }

    public bool IsPreviousLevelAvailable()
    {
        return (IsLevelUnlocked(currentLevelId - 1));
    }

    public bool IsNextLevelAvailable()
    {
        return (IsLevelUnlocked(currentLevelId + 1));
    }

    public void UnlockLevel(int levelId)
    {
        playerSavedData.Unlock(levelId);
    }

    public void UnlockNextLevel()
    {
        playerSavedData.Unlock(currentLevelId + 1);
    }

    public void AddGem(int gemId)
    {
        playerSavedData.AddGem(currentLevelId, gemId);
    }

    public void SaveData()
    {
        DataSaver.SaveGameState(playerSavedData);
    }


    public void RetrieveGameState()
    {
        playerSavedData = DataSaver.LoadGameState();
    }

    public void DeleteSavedData()
    {
        DataSaver.DeleteSavedData();
    }

    private IEnumerator DelayLoadScene(int sceneBuildIndex, float seconds)
    {
        // use WaitForSecondsRealtime to allow moving when timeScale = 0 (game paused)
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(sceneBuildIndex);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
