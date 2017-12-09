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
    private GameObject head;
    [SerializeField]
    private Phase bossPhase;

    public bool IsDead
    {
        get { return totalHP <= 0f; }
    }

    public Phase BossPhase
    {
        get
        {
            if (leftHandHP > 0f || rightHandHP > 0f)
                return Phase.EASY;
            if (headHP > 0f)
                return Phase.ANGRY;
            //mouthHP > 0
            return Phase.PSYCHO;
        }
    }


    void Awake()
    {
        leftHand = GameObject.Find("LeftHand");
        rightHand = GameObject.Find("RightHand");
        head = GameObject.Find("Head");

        //initialize hp values per body part
        {
            leftHand.GetComponent<BossHP>().hp = leftHandHP;
            rightHand.GetComponent<BossHP>().hp = rightHandHP;
            Assert.IsTrue(head.GetComponents<BossHP>().Length == 2, "Boss Head GameObject must have 2 BossHP scripts attached to it");
            head.GetComponents<BossHP>()[0].hp = headHP;
            head.GetComponents<BossHP>()[1].hp = mouthHP;
        }

    }

    void Start()
    {
        totalHP = leftHandHP + rightHandHP + headHP + mouthHP;
    }

    public void ApplyDamage(float damage)
    {
        totalHP -= damage;
        
        //Update HP Values per body part
        {
            leftHandHP = SetHP(leftHand);
            rightHandHP = SetHP(rightHand);
            //TODO: create another gameObject for the boss's mouth
            Assert.IsTrue(head.GetComponents<BossHP>().Length == 2, "Boss Head GameObject must have 2 BossHP scripts attached to it");
            headHP = head.GetComponents<BossHP>()[0].hp;
            mouthHP = head.GetComponents<BossHP>()[1].hp;
        }

        bossPhase = BossPhase;//debug to see in editor
    }

    private float SetHP(GameObject bodyPart)
    {
        return bodyPart.activeInHierarchy ? bodyPart.GetComponent<BossHP>().hp : 0f;
    }

}
