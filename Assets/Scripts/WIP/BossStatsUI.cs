using UnityEngine;
using UnityEngine.UI;

public class BossStatsUI : MonoBehaviour {

    private BossManager bm;

    public Text leftArmText;
    public Text rightArmText;
    public Text headHP;
    public Text mouthHP;

    private void Awake()
    {
        bm = FindObjectOfType<BossManager>();
        leftArmText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        rightArmText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        headHP = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        mouthHP = transform.GetChild(3).GetChild(0).GetComponent<Text>();

        leftArmText.text = bm.leftHandHP.ToString();
        rightArmText.text = bm.rightHandHP.ToString();
        headHP.text = bm.headHP.ToString();
        mouthHP.text = bm.mouthHP.ToString();
    }

    void Update()
    {
        leftArmText.text = bm.leftHandHP.ToString();
        rightArmText.text = bm.rightHandHP.ToString();
        headHP.text = bm.headHP.ToString();
        mouthHP.text = bm.mouthHP.ToString();
    }
}
