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

    private int _levelSelectId { get { return SceneManager.sceneCountInBuildSettings - 1; } }

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

    void Start()
    {
        currentLevelId = SceneManager.GetActiveScene().buildIndex;
        levelMetaData = levelCollection.GetLevelBySceneBuildIndex(currentLevelId);

        // trigger animation for start of level
        // TODO: make this happen through events
        SceneChangeCircle.instance?.SceneStarts();

        StartCoroutine(SetState());
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
            LoadLevel(currentLevelId - 1);
    }

    public void LoadLevel(int levelID)
    {
        StateManager.instance?.SetState(GameState.TRANSITION);
        // TODO: make this happen through events
        SceneChangeCircle.instance?.SceneEnds();
        StartCoroutine(DelayLoadScene(levelID, transitionDuration));
    }
    public void ReloadLevel() => LoadLevel(currentLevelId);

    public void LoadFirstScene() => LoadLevel(0);

    public void LoadLevelSelectMenu() => LoadLevel(_levelSelectId);

    public bool IsLevelUnlocked(int levelId) => playerSavedData.IsUnlocked(levelId);

    public List<int> GetUnlockedLevels() => playerSavedData.GetUnlockedLevels();

    public bool IsPreviousLevelAvailable() => (IsLevelUnlocked(currentLevelId - 1));

    public bool IsNextLevelAvailable() => (IsLevelUnlocked(currentLevelId + 1));

    public void UnlockLevel(int levelId) => playerSavedData.Unlock(levelId);

    public void UnlockNextLevel() => playerSavedData.Unlock(currentLevelId + 1);

    public void AddGem(int gemId) => playerSavedData.AddGem(currentLevelId, gemId);

    public void SaveData() => DataSaver.SaveGameState(playerSavedData);

    public void RetrieveGameState() => playerSavedData = DataSaver.LoadGameState();

    public void DeleteSavedData() => DataSaver.DeleteSavedData();

    private IEnumerator DelayLoadScene(int sceneBuildIndex, float seconds)
    {
        // use WaitForSecondsRealtime to allow moving when timeScale = 0 (game paused)
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void Quit() => Application.Quit();

    private IEnumerator SetState()
    {
        yield return new WaitForSecondsRealtime(transitionDuration);
        GameState gameState = currentLevelId == _levelSelectId ? GameState.LEVEL_SELECT : GameState.PLAY;
        StateManager.instance?.SetState(gameState);
    }
}
