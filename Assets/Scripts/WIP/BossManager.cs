using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BossManager : MonoBehaviour {

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
                break;
            case Phase.ANGRY:
                attackPattern1.enabled = false;
                attackPattern2.enabled = true;
                break;
            case Phase.PSYCHO:
                attackPattern2.enabled = false;
                head.SetActive(true);
                head.GetComponent<Animator>().SetBool("canOpenMouth", true);
                head.GetComponent<Collider2D>().enabled = false;
                mouth.SetActive(true);
                break;
            case Phase.DEAD:
                head.SetActive(false);
                mouth.SetActive(false);
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
