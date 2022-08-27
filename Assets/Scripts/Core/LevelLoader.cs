using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : SingletoneBase<LevelLoader>
{
    public int currentLevelId;
    public float transitionDuration = 0.5f;
    public LevelMetaDataCollection levelCollection;

    public LevelMetaData levelMetaData { get; private set; }

    private int _levelSelectId { get { return SceneManager.sceneCountInBuildSettings - 1; } }

    // Number of scene included in build that are not levels (except for main menu)
    // - Level Select Scene
    private static int NON_LEVEL_SCENE_COUNT = 1;

    void Start()
    {
        currentLevelId = SceneManager.GetActiveScene().buildIndex;
        levelMetaData = levelCollection.GetLevelBySceneBuildIndex(currentLevelId);

        // TODO: remove debug.log
        // SaveLastLevelPlayed();
        // Debug.Log("last played level: " + playerSavedData.lastPlayedLevel.ToString());

        // trigger animation for start of level
        // TODO: make this happen through events
        SceneChangeCircle.instance?.SceneStarts();

        StartCoroutine(SetState());
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

    public static bool IsLevelId(int levelId)
    {
        return (
            levelId > 0
            && levelId < SceneManager.sceneCountInBuildSettings - NON_LEVEL_SCENE_COUNT
        );
    }
}
