using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    Vector3 vel = Vector3.zero;
    public Transform player;
    public Transform enemy;

    public Transform pos_1st;
    public Transform pos_2nd;

    private Vector3 firstPos;
    private Vector3 secPos;

    public GameObject podium;
     
    // Start is called before the first frame update
    void Start()
    {
        podium.SetActive(false);
        firstPos = pos_1st.position;
        secPos = pos_2nd.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.raceFinish)
            StartCoroutine(SetEndPodium());
            //setEndingPosition();
    }

    void setEndingPosition()
    {
        podium.SetActive(true);
        player.position = firstPos;
        enemy.position = secPos;
        //player.position = Vector3.SmoothDamp(player.position, pos_1st.localPosition, ref vel, 1f);
        //enemy.position = Vector3.SmoothDamp(player.position, pos_2nd.localPosition, ref vel, 1f);

    }

    IEnumerator SetEndPodium()
    {
        yield return new WaitForSeconds(3f);
        setEndingPosition();
    }
}
