using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnPause;
    [HideInInspector]
    public UnityEvent OnResume;
    public static GameManager GM
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindAnyObjectByType<GameManager>();
            }
            return m_instance;
        }
    }
    private bool IsPause = false;

    public bool isPause
    {
        get
        {
            return IsPause;
        }
    }

    public void TogglePause()
    {
        Debug.Log("Toggle");
        if (!IsPause)
        {
            IsPause = true;
            OnPause.Invoke();
            UIManager.instance.SetGrayScreen(true);
            Time.timeScale = 0;
        }
        else
        {
            IsPause = false;
            OnResume.Invoke();
            Time.timeScale = 1;
            UIManager.instance.SetGrayScreen(false);
            Debug.Log("OnResume");
        }
    }

    private static GameManager m_instance;
    
    public Rigidbody2D player;
    private Camera MainCam;

    public float maxX = 10;
    public float minX = -10;
    public float maxY = 5;
    public float minY = -5;
    public bool isGameover { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GM != this)
        {
            Destroy(this);
        }
        MainCam = Camera.main;
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        player.GetComponent<LivingEntity>().onDeath += Endgame;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 screenPoint = MainCam.WorldToViewportPoint(player.position);
        MainCam.transform.position = new Vector3(Mathf.Min(Mathf.Max(player.transform.position.x, minX), maxX), Mathf.Min(Mathf.Max(minY, player.transform.position.y + 1), maxY), -10);
        //Debug.Log("sp: " + screenPoint);
        /*
        if (screenPoint.y < 0)
        {
            player.transform.position = new Vector3(0,0,0);
            player.linearVelocity = new Vector2(0, 0);
        }
        */
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        LivingEntity le = collision.GetComponent<LivingEntity>();
        if (le != null && !le.dead)
        {
            //Debug.Log("dead");
            le.Die();
        }
    }

    public void PauseGame(bool menu) 
    {

    }

    public void Endgame()
    {
        GetComponent<AudioSource>().Stop();
        isGameover = true;
        UIManager.instance.SetActiveGameoverUI(true);
    }
}
