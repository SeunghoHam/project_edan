using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text text_Coin;
    public Text Result_Coin;
    

    public int currentPlayerFeather = 0;
    public int currentEnemyFeather = 0; 
    private int currentCoinCount = 0;

    public bool playerLead;
    public bool enemyLead;


    [SerializeField] GameObject[] g_PlayerFeatherWing;
    [SerializeField] GameObject[] g_EnemyFeatherWing;

    //[SerializeField] ParticleSystem playerCrownParticle;

    [SerializeField] Text playerCfeather;
    [SerializeField] Text enemyCfeather;
    [SerializeField] GameObject g_textPlayerFeather;
    [SerializeField] GameObject g_textEnemyFeather;

    public ParticleSystem WingCharge;
    public ParticleSystem WingCharge_e;
    // ***** Timer

    [Header("Character")]
    [SerializeField] GameObject playerCharacter;
    [SerializeField] GameObject enemyCharacter;

    PlayerController thePlayer;
    Enemy theEnemy;
    
    private void Awake() 
    {
        thePlayer = FindObjectOfType<PlayerController>();
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

        checkFeather();
        if (currentPlayerFeather >= 9)
            currentPlayerFeather = 8;
        else if (currentPlayerFeather <= 0)
            currentPlayerFeather = 0;

        if (currentEnemyFeather >= 9)
            currentEnemyFeather = 8;
        else if (currentEnemyFeather <= 0)
            currentEnemyFeather = 0;

        if(GameManager.Instance.mode_system1)
        {
            playerCfeather.text = currentPlayerFeather.ToString();
            enemyCfeather.text = currentEnemyFeather.ToString();
            if(currentPlayerFeather >= 8)
            {
                thePlayer.canGet = false;
                StartCoroutine(Pass());
            }
            if(currentEnemyFeather >=8)
            {
                theEnemy.canGet = false;
                theEnemy.enemySystem = true;
            }
        }
        else if(GameManager.Instance.mode_system2)
        {
            g_textPlayerFeather.SetActive(false);
            g_textEnemyFeather.SetActive(false);
            text_Coin.text = currentCoinCount.ToString(); // format:"0#"
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

            // CrownParticle
            //StartCoroutine(CrownParticle_p());
            //playerCrownParticle.Play();
            enemyLead = false;
            playerLead = true;

            /* 플레이어 적과 충돌 ***************************
            if(thePlayer.isCollision == true) // player > enemy
            {
                currnetPlayerFeather += currentEnemyFeather;
                currentEnemyFeather = 0;
                StartCoroutine(CrownParticle_p());
            }*/
        }
        
        else if(currentPlayerFeather < currentEnemyFeather)
        {

            enemyLead = true;
            playerLead = false;
            /*
            if(thePlayer.isCollision == true) // player < enemy
            {
                currentEnemyFeather += currnetPlayerFeather;
                currnetPlayerFeather = 0;
                for(int i=0; i < g_PlayerFeather.Length; i++)
                {
                    g_PlayerFeather[i].SetActive(false);
                    // 깃털 빠지는 애니메이션
                }
            }*/
        }
    }
    void resetFeather()
    {
        
    }
    public void GetCoin(int _num)
    {
        currentCoinCount += _num;
    }
    IEnumerator Pass()
    {
        GameManager.Instance.mode_system1 = false;
        WingCharge.Play();
        yield return null;
        GameManager.Instance.mode_system3 = true;
    }
}
