using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour {

    public enum HitLayer
    {
        DEFAULT=0,
        IGNORE_RAYCAST=2
    }

    public enum Phase
    {
        EASY=0, //hands
        ANGRY=1, //head
        PSYCHO=2,// mouth
        DEAD=3
    }

    public float leftHandHP = 60f;
    public float rightHandHP = 60f;
    public float headHP = 140f;
    public float mouthHP = 80f;
    [SerializeField]
    private float totalHP;

    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject center;
    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject mouth;
    [SerializeField]
    private Phase bossPhase;

    [SerializeField]
    private BossAttackPattern1 attackPattern1;
    [SerializeField]
    private BossAttackPattern2 attackPattern2;

    public GameObject goldStar;

    public bool IsDead
    {
        get { return bossPhase == Phase.DEAD; }
    }

    public float TotalHP
    {
        get { return (leftHandHP + rightHandHP + headHP + mouthHP); }
    }


    public Phase BossPhase
    {
        get
        {
            if (leftHandHP > 0f || rightHandHP > 0f)
                return Phase.EASY;
            if (headHP > 0f)
                return Phase.ANGRY;
            if (mouthHP > 0)
                return Phase.PSYCHO;

            return Phase.DEAD;
        }
    }


    void Awake()
    {
        leftHand = GameObject.Find("LeftHand");
        rightHand = GameObject.Find("RightHand");
        head = GameObject.Find("Head");
        mouth = GameObject.Find("Mouth");
        center = GameObject.Find("Center");
        goldStar = GameObject.Find("goldStar");
        goldStar.SetActive(false);

        attackPattern1 = center.GetComponent<BossAttackPattern1>();
        attackPattern2 = head.GetComponent<BossAttackPattern2>();

        //initialize hp values per body part
        {
            leftHand.GetComponent<BossHP>().hp = leftHandHP;
            rightHand.GetComponent<BossHP>().hp = rightHandHP;
            head.GetComponent<BossHP>().hp = headHP;
            mouth.GetComponent<BossHP>().hp = mouthHP;
        }

    }

    void Start()
    {
        totalHP = TotalHP;
    }

    void Update()
    {
        switch (bossPhase)
        {
            case Phase.EASY:
                attackPattern1.enabled = true;
                head.layer = (int)HitLayer.IGNORE_RAYCAST;
                break;
            case Phase.ANGRY:
                head.layer = (int)HitLayer.DEFAULT;
                attackPattern1.enabled = false;
                attackPattern2.enabled = true;
                break;
            case Phase.PSYCHO:
                attackPattern2.enabled = false;
                head.SetActive(true);
                head.layer = (int)HitLayer.IGNORE_RAYCAST;
                head.GetComponent<Animator>().SetBool("canOpenMouth", true);
                head.GetComponent<Collider2D>().enabled = false;
                mouth.SetActive(true);
                mouth.layer = (int)HitLayer.DEFAULT;
                break;
            case Phase.DEAD:
                head.SetActive(false);
                mouth.SetActive(false);
                //goldStar.SetActive(true);
                SceneManager.LoadScene("WinScene");
                break;
        }
    }

    public void ApplyDamage(float damage)
    {
        //Update HP Values per body part
        switch (BossPhase)
        {
            case Phase.EASY:
                leftHandHP = SetHP(leftHand);
                rightHandHP = SetHP(rightHand);
                break;
            case Phase.ANGRY:
                headHP = SetHP(head);
                break;
            case Phase.PSYCHO:
                head.GetComponent<Emitter>().enabled = false;
                mouthHP = SetHP(mouth);
                break;
            case Phase.DEAD:
                break;
        }
        bossPhase = BossPhase;//debug to see in editor
        totalHP = TotalHP;
    }

    private float SetHP(GameObject bodyPart)
    {
        return bodyPart.activeInHierarchy ? bodyPart.GetComponent<BossHP>().hp : 0f;
    }

}
