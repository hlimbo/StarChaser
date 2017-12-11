using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerLifeMessenger : MonoBehaviour, ILifeMessenger {

    [Tooltip("Can have between 0 to 3 lives inclusive")]
    public int lives;
    [SerializeField]
    private Image[] hearts;
    private int maxLives;
    private AudioSource aSrc;

    void Start()
    {
        aSrc = GetComponent<AudioSource>();
        Assert.IsTrue(lives <= transform.childCount, "Lives: " + lives + " | transform.childCount: " + transform.childCount);
        hearts = new Image[lives];
        maxLives = lives;
        for (int i = 0; i < lives; ++i)
        {
            hearts[i] = transform.GetChild(i).GetComponent<Image>();
            hearts[i].enabled = true;
        }
    }

    public void LoseLife()
    {
        Debug.Log("Lose 1 life");
        hearts[Mathf.Clamp(lives - 1,0,maxLives - 1)].enabled = false;
        lives = Mathf.Clamp(--lives, 0, maxLives);
        aSrc.Play();
       // Handheld.Vibrate();//annoying af when playing
    }

    public void GainLife()
    {
        Debug.Log("Gain 1 life");
        hearts[Mathf.Clamp(lives - 1,0,maxLives - 1)].enabled = true;
        lives = Mathf.Clamp(++lives, 0, maxLives);
    }
}
