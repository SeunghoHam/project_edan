using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int currentScore;
    public int distanceScore;
    int multiplerScore;
    float maxDistance; 

    bool setOriginPos;

    float originPosZ; // player's originPos

    [SerializeField] Text text_Score;
    [SerializeField] Transform tf_Player;
    // Start is called before the first frame update
    void Start()
    {
        multiplerScore = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentScore);
        if(!setOriginPos&&GameManager.Instance.mode_system2)
        {
            setOriginPos = true;
            originPosZ = tf_Player.position.z;
        }
            

        if(tf_Player.position.z > maxDistance)
        {
            maxDistance = tf_Player.position.z;
            distanceScore = Mathf.RoundToInt(maxDistance - originPosZ);
        }
        currentScore = distanceScore / 10;
        text_Score.text = currentScore.ToString();

    }
    
}
