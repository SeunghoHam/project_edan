using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionManager : MonoBehaviour
{
    public Transform thePlayer;
    public Transform theEnemy;

    public GameObject playerCrown;
    public GameObject enemyCrown;

    public Image img_playerCrown;
    public Image img_enemyCrown;

    float time = 0f;
    float f_time = 1f;

    public ParticleSystem playerCrownParticle;
    public bool leadPlayer;
    public bool leadEnemy;
    public bool showParticle;
    WaitForSeconds crownParticle = new WaitForSeconds(0.3f);
    // Start is called before the first frame update
    void Start()
    {
        leadPlayer = false;
        leadEnemy = false;
        showParticle = false;
        playerCrown.SetActive(false);
        enemyCrown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.mode_system2 == true)
        {
            if(GameManager.Instance.raceFinish == false)
            {
                checkPosition();
                if (leadPlayer)
                {
                    if (showParticle)
                    {
                        showParticle = true;
                        StartCoroutine(CrownParticle_p());
                        ShowPlayerCrown();
                    }

                    playerCrown.SetActive(true);
                    enemyCrown.SetActive(false);
                }
                else if (leadEnemy)
                {
                    if (!showParticle)
                    {
                        showParticle = true;
                        StartCoroutine(CrownParticle_e());
                    }
                    playerCrown.SetActive(false);
                    enemyCrown.SetActive(true);
                }
            }
         
        }


    }
    public void Show()
    {
        img_playerCrown.gameObject.SetActive(true);
    }
    public void ShowPlayerCrown()
    {
        StartCoroutine(CrownFade());
    }
    IEnumerator CrownFade()
    {
        img_playerCrown.gameObject.SetActive(true);
        time = 0f;
        Color alpha = img_playerCrown.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime * 3 / f_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            img_playerCrown.color = alpha;
            yield return null;
            
        }

        //time = 0f;
        
        time = 0f;
        yield return new WaitForSeconds(0.4f);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime * 3 / f_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            img_playerCrown.color = alpha;
            yield return null;
            
        }
        img_playerCrown.gameObject.SetActive(false);
        yield return null;
    }
    void checkPosition()
    {
        if (thePlayer.transform.position.z > theEnemy.transform.position.z)
        {
            leadPlayer = true;
            leadEnemy = false;
        }
        else if (thePlayer.transform.position.z < theEnemy.transform.position.z)
        {
            leadPlayer = false;
            leadEnemy = true;
        }

        //leadPlayer = false;
        //leadEnemy = false;
    }

    IEnumerator CrownParticle_p()
    {
        playerCrownParticle.Play();
        yield return crownParticle;
        playerCrownParticle.Stop();
        showParticle = false;
    }
    IEnumerator CrownParticle_e()
    {
        yield return crownParticle;
        showParticle = false;
    }
}
