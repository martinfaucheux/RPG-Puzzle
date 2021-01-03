using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance = null;

    // maximum level that was reached
    public int maxLevelId;
    // current loaded level id
    public int currentLevelId;

    public float transitionDuration = 0.5f;

    // cache last level that was played (for play button)
    private int _lastLevelPlayedID = 1;
    private bool _isMainMenu = false;

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

        if (!_isMainMenu){
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

    public void LoadLastLevelPlayed(){
        LoadLevel(_lastLevelPlayedID, false);
    }

    public void LoadFirstScene(){
        Debug.Log("load first level");
        LoadLevel(0, false);
    }

    private void LoadLevel(int levelID, bool doSaveData = true){
        SceneChangeCircle.instance.SceneEnds();

        if (doSaveData){
            SaveData(currentLevelID: levelID);
        }
        
        _lastLevelPlayedID = levelID;
        StartCoroutine(DelayLoadScene(levelID, transitionDuration));
    }

    public bool IsLevelUnlocked(int levelId){
        return (levelId <= maxLevelId);
    }

    private void UnlockLevel(int levelID){
        if (maxLevelId < levelID){
            maxLevelId = levelID;
            SaveData(maxLevelId: levelID);
        }
    }

    public void SaveData(int maxLevelId = -1, int currentLevelID = -1){
        if (maxLevelId < 0 ){
            maxLevelId = this.maxLevelId;
        }

        if (currentLevelID < 0){
            currentLevelID = this.currentLevelId;
        }
        DataSaver.SaveGameState(maxLevelId, currentLevelID);

    }

    public void RetrieveGameState(){
        PlayerData playerData = DataSaver.LoadGameState();

        if (playerData != null){
            maxLevelId = playerData.maxLevelId;
            _lastLevelPlayedID = playerData.currentLevelId;
        }
    }

    public void DeleteSavedData(){
        DataSaver.DeleteSavedData();
        maxLevelId = 1;
        _lastLevelPlayedID = 1;
    }

    private IEnumerator DelayLoadScene(int sceneBuildIndex, float seconds)
    {
        // use WaitForSecondsRealtime to allow moving when timeScale = 0 (game paused)
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(sceneBuildIndex);
    }


    public void Quit(){
        Application.Quit();
    }
}
