using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject[] MonsterPrefabs;

    private UIManager um;
    private List<GameObject> monsters;
    private int wave = 0;
    private PlayerHealth ph;

    void Start()
    {
        monsters = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GM != null && GameManager.GM.isGameover)
        {
            return;
        }
        if(wave == 0)
        {
            SpawnWave();
            return;
        }
        if(monsters.Count <= 0 && !SkillManager.instance.isSelecting)
        {
            SkillManager.instance.SetSkillScreen();
            if(ph == null)
            {
                ph = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
            }
            ph.RestoreHealth(20.0f);
            GameManager.GM.OnResume.AddListener(SpawnWave);
        }
        UpdateUI();
    }

    void SpawnWave()
    {
        wave++;
        if (wave == 4)
        {
            GameObject bg = GameObject.FindWithTag("BackGround");
            Destroy(bg);
            SceneManager.LoadScene("Stage2", LoadSceneMode.Additive);
        }
        int spawnCount = Mathf.RoundToInt(wave * 2f);

        for(int i = 0; i < spawnCount; i++)
        {
            CreateMonster();
        }
        UpdateUI();
        GameManager.GM.OnResume.RemoveListener(SpawnWave);
    }
    
    private void CreateMonster()
    {
        GameObject MonsterPrefab = MonsterPrefabs[Random.Range(0, MonsterPrefabs.Length)];
        GameObject monster = Instantiate(MonsterPrefab, new Vector3(Random.Range(-10.0f, 10.0f), 5f, 0f), Quaternion.identity);
        monsters.Add(monster);
        Monster ms = monster.GetComponent<Monster>();
        monster.SetActive(false);
        ms.onDeath += () => monsters.Remove(monster);
        Invoke("ActiveMonster", 1.0f);
    }

    void ActiveMonster()
    {
        for (int i = 0; i < monsters.Count; i++)
            monsters[i].SetActive(true);
    }

    private void UpdateUI()
    {
        UIManager.instance.UpdateWaveText(wave, monsters.Count);
    }
}
