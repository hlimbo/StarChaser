using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used as an animation event
public class ToggleBossMouth : MonoBehaviour {

    [SerializeField]
    private GameObject mouth;
    public int mouthSpawnLength = 6;
    [SerializeField]
    private GameObject[] mouthSpawns;
    [SerializeField]
    private int counter = 0;
    //on the first and every fourth time the boss opens its mouth, it will fire zigzag bullets
    public int fireFrequency = 4;

    void Awake()
    {
        mouth = GameObject.Find("Mouth");
        mouth.GetComponent<SpriteRenderer>().enabled = false;

        mouthSpawns = new GameObject[mouthSpawnLength];
        for (int i = 0; i < mouthSpawnLength; ++i)
        {
            mouthSpawns[i] = transform.Find("mouthSpawn" + i).gameObject;
            mouthSpawns[i].SetActive(false);
        }
    }

    public void OpenMouth()
    {
        mouth.GetComponent<BoxCollider2D>().enabled = true;
        mouth.GetComponent<SpriteRenderer>().enabled = true;
        ToggleMouthSpawns(true);
        if (counter % fireFrequency == 0)
            mouth.GetComponent<Emitter>().enabled = true;
    }

    public void CloseMouth()
    {
        mouth.GetComponent<BoxCollider2D>().enabled = false;
        mouth.GetComponent<SpriteRenderer>().enabled = false;
        ToggleMouthSpawns(false);
        if (counter++ % fireFrequency == 0)
            mouth.GetComponent<Emitter>().enabled = false;
    }

    private void ToggleMouthSpawns(bool toggle)
    {
        foreach(GameObject mouthSpawn in mouthSpawns)
        {
            mouthSpawn.SetActive(toggle);
        }
    }
}
