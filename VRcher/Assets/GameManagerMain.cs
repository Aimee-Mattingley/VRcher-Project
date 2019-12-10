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
    public AudioSource[] sounds;
    public AudioSource music;
    public AudioSource gameComplete;


    void Start()
    {
        sounds = GetComponents<AudioSource>();
        music = sounds[0];
        gameComplete = sounds[1];

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
        else{
            gameOver();
        }

    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            GameComplete();
            gameComplete.Play();
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
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameObject woodenBow = controllerLeft.transform.GetChild (1).gameObject;
        GameObject arrow = controllerRight.transform.GetChild (1).gameObject;
        woodenBow.SetActive(false);
        model.SetActive(true);
        arrow.SetActive(false);
        controllerRight.GetComponent<ArrowManagerNejc>().gameIsOver();
    }
    public void towerDead(){
        towerDefeated = true;
    }

    private void GameComplete(){
        gameCompleteCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameObject woodenBow = controllerLeft.transform.GetChild (1).gameObject;
        GameObject arrow = controllerRight.transform.GetChild (1).gameObject;
        woodenBow.SetActive(false);
        model.SetActive(true);
        arrow.SetActive(false);
        controllerRight.GetComponent<ArrowManagerNejc>().gameIsOver();
    }

}
