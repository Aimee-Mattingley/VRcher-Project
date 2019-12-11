using UnityEngine;
using System.Collections;

public class GameManagerMain : MonoBehaviour {

    public enum SpawnState { Spawning, Waiting, Counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.Counting;

    public GameObject gameOverCanvas;
    public GameObject gameCompleteCanvas;
    public GameObject controllerLeft;
    public GameObject controllerRight;
    public GameObject model;
    private bool towerDefeated;
    private bool gameIsOver;
    AudioSource[] sounds;
    public AudioSource music;
    public AudioSource gameComplete;
    public AudioSource gameOverAudio;
    public AudioSource waveCompletedAudio;
    public AudioClip musicClip;
    public AudioClip gameCompleteClip;
    public AudioClip gameOverClip;
    public AudioClip waveCompletedClip;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        music = sounds[0];
        gameComplete = sounds[1];
        gameOverAudio = sounds[2];
        waveCompletedAudio = sounds[3];

        gameIsOver = false;

        gameOverCanvas.SetActive(false);
        gameCompleteCanvas.SetActive(false);
        towerDefeated = false;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(!towerDefeated){
            if (state == SpawnState.Waiting)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
            if (state != SpawnState.Spawning)
                {
                    StartCoroutine( SpawnWave ( waves[nextWave] ) );
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
        else if(towerDefeated && !gameIsOver){
            gameOver();
        }

    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        waveCompletedAudio.PlayOneShot(waveCompletedClip, 1F);
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            GameComplete();
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        //Debug.Log(GameObject.FindWithTag("Enemy"));
        /*if (GameObject.FindWithTag("Enemy") == null)
            {
                Debug.Log("All enemies dead!");
                return false;
        }
        else{
            //Debug.Log("There are still some enemies!");
            return true;
        }*/
        
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds( 1f/_wave.rate );
        }

        state = SpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    private void gameOver(){
        Debug.Log("GAME OVER!!!");
        gameOverAudio.PlayOneShot(gameOverClip, 1F);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;

        controllerLeft.SetActive(false);
        model.SetActive(true);
        GameObject arrow = controllerRight.transform.GetChild (0).gameObject;
        arrow.SetActive(false);

        gameIsOver = true;

    }
    public void towerDead(){
        towerDefeated = true;
    }

    private void GameComplete(){
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        gameComplete.PlayOneShot(gameCompleteClip, 1F);
        controllerLeft.SetActive(false);
        model.SetActive(true);
        GameObject arrow = controllerRight.transform.GetChild (0).gameObject;
        arrow.SetActive(false);
    }

}
