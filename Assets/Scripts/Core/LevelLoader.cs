using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance = null;

    // maximum level that was reached
    // public int maxLevelId;

    // current loaded level id
    public int currentLevelId;

    public float transitionDuration = 0.5f;

    // cache last level that was played (for play button)
    private int _lastLevelPlayedID = 1;
    private bool _isMainMenu = false;

    private PlayerData _playerSavedData;

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
        _isMainMenu = (currentLevelId == 0);

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

    public void LoadLastLevelPlayed()
    {
        LoadLevel(_lastLevelPlayedID);
    }

    public void LoadFirstScene()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int levelID)
    {
        SceneChangeCircle.instance.SceneEnds();

        _lastLevelPlayedID = levelID;
        StartCoroutine(DelayLoadScene(levelID, transitionDuration));
    }

    public bool IsLevelUnlocked(int levelId)
    {
        return _playerSavedData.IsUnlocked(levelId);
    }

    public List<int> GetUnlockedLevels()
    {
        return _playerSavedData.GetUnlockedLevels();
    }

    public bool IsPreviousLevelAvailable()
    {
        return (IsLevelUnlocked(currentLevelId - 1));
    }

    public bool IsNextLevelAvailable()
    {
        return (IsLevelUnlocked(currentLevelId + 1));
    }

    private void UnlockLevel(int levelId)
    {
        if (!IsLevelUnlocked(levelId))
        {
            _playerSavedData.Update(levelId, new LevelSaveData());
        }
    }

    public void SaveData(int gemCount, bool isSideQuestComplete)
    {
        LevelSaveData levelData = new LevelSaveData(gemCount, isSideQuestComplete);
        DataSaver.SaveGameState(currentLevelId, levelData);
    }


    public void RetrieveGameState()
    {
        _playerSavedData = DataSaver.LoadGameState();
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
