using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //public Text text_Coin;
    public Text Result_Coin;
    

    public int currentPlayerFeather = 0;
    public int currentEnemyFeather = 0; 
    private int currentCoinCount = 0;

    public int maxFeather = 8;

    public bool playerLead;
    public bool enemyLead;

    // Gauge
    public Slider sliderFeatherCount;

    [SerializeField] GameObject[] g_PlayerFeatherWing;
    [SerializeField] GameObject[] g_EnemyFeatherWing;

    //[SerializeField] ParticleSystem playerCrownParticle;


    NewPlayerController thePlayer;
    Enemy theEnemy;
    
    private void Awake() 
    {
        thePlayer = FindObjectOfType<NewPlayerController>();
        theEnemy = FindObjectOfType<Enemy>();
    }
    void Start() 
    {
        for(int i=0; i <g_PlayerFeatherWing.Length; i++) 
        {
            g_PlayerFeatherWing[i].SetActive(false);
            g_EnemyFeatherWing[i].SetActive(false);
        }

        enemyLead = false;  
        playerLead = false;
    }
    void Update()
    {
        sliderFeatherCount.value = currentPlayerFeather /  maxFeather;
        checkFeather();
        if (currentPlayerFeather >= 9)
            currentPlayerFeather = maxFeather;
        else if (currentPlayerFeather <= 0)
            currentPlayerFeather = 0;

        if (currentEnemyFeather >= 9)
            currentEnemyFeather = maxFeather;
        else if (currentEnemyFeather <= 0)
            currentEnemyFeather = 0;

        if(GameManager.Instance.mode_system1)
        {
            if(currentPlayerFeather >= maxFeather)
            {

                //StartCoroutine(Pass());
                Debug.Log("Minimum Feather get");
            }
            if(currentEnemyFeather >=maxFeather)
            {
                theEnemy.canGet = false;
                theEnemy.enemySystem = true;
            }
        }
        else if(GameManager.Instance.mode_system2)
        {
            //text_Coin.text = currentCoinCount.ToString(); // format:"0#"
            Result_Coin.text = currentCoinCount.ToString();
        }
        
        //playerCrownParticle.transform.position = playerCrown.transform.position;
        //playerCrownParticle.Stop();
    }
    public void player_IncreaseFeather(int _num)
    {
        currentPlayerFeather += _num;
        player_UpdateFeatherStatus();
    }
    public void enemy_IncreaseFeather(int _num)
    {
        currentEnemyFeather += _num;
        //player_UpdateFeatherStatus();
        enemy_UpdateFeatherStatus();
    }
    public void player_DecreaseFeather(int _num)
    {
        currentPlayerFeather -= _num;
        player_UpdateFeatherStatus();
    }
    public void enemy_DecreaseFeather(int _num)
    {
        currentEnemyFeather -= _num;
        enemy_UpdateFeatherStatus();
    }
    void player_UpdateFeatherStatus()
    {
        for(int i=0; i< g_PlayerFeatherWing.Length; i++)
        {
            //if(i * 3 + 1 < currnetPlayerFeather) 
            if(i < currentPlayerFeather)
            //if(i *2  < currnetPlayerFeather)
            {
                //Debug.Log("깃털 획득 " + i + "번째 배열");
                g_PlayerFeatherWing[i].SetActive(true);
                // 깃털 획득 애니메이션 넣어야됨
            }
            else
            {
                //Debug.Log("깃털 획득 " + i + "번째 배열");
                g_PlayerFeatherWing[i].SetActive(false);
                // 깃털 빠지는 애니메이션
            }
        }
        //Debug.Log(currnetPlayerFeather);
    }

    void enemy_UpdateFeatherStatus()
    {
        for(int i=0;i< g_EnemyFeatherWing.Length; i++)
        {
            if(i < currentEnemyFeather)
            {
                g_EnemyFeatherWing[i].SetActive(true);
            }
            else
            {
                g_EnemyFeatherWing[i].SetActive(false);
            }
        }
    }
    void checkFeather()
    {
        if(currentPlayerFeather > currentEnemyFeather)
        {
            enemyLead = false;
            playerLead = true;   
        }
        else if(currentPlayerFeather == currentEnemyFeather)
        {
            enemyLead= false;
            playerLead = false;
        }
        else if(currentPlayerFeather < currentEnemyFeather)
        {
            enemyLead = true;
            playerLead = false;            
        }
    }
    public void GetCoin(int _num)
    {
        currentCoinCount += _num;
    }
    IEnumerator Pass()
    {
        GameManager.Instance.mode_system1 = false;
        yield return null;
        GameManager.Instance.mode_system3 = true;
    }
}
