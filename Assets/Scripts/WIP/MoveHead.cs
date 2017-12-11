using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHead : MonoBehaviour {

    public float timeToMoveForward;
    public float timeToBite = 0.6f;
    public float moveSpeed;
    public bool isMovingForwards = false;
    public Vector3 originalPos;

    [SerializeField]
    private GameObject biteBox;
    [SerializeField]
    private GameObject[] headSpawns;
    [SerializeField]
    private int headSpawnCount = 6;

    public float TotalDuration
    {
        get { return timeToMoveForward * 2f + timeToBite; }
    }

    private Animator anim;

    //use this bool to check if arms moved back to its original position
    public bool hasReturned
    {
        get
        {
            return !isMovingForwards && ApproxEqual(transform.position, originalPos, .002f);
        }
    }

    void Awake()
    {
        headSpawns = new GameObject[headSpawnCount];
        for(int i = 0;i < headSpawnCount;++i)
        {
            headSpawns[i] = transform.Find("headSpawn" + i).gameObject;
            headSpawns[i].SetActive(false);
        }

        biteBox = transform.GetChild(0).gameObject;
        biteBox.GetComponent<BoxCollider2D>().enabled = false;
        originalPos = transform.position;
        anim = GetComponent<Animator>();
        enabled = false;
    }

    void OnEnable()
    {
        isMovingForwards = true;
        StartCoroutine(ChangeMoveDirectionDelay());
    }

    void Update()
    {
        if (isMovingForwards)
            transform.Translate(transform.up * -1 * Time.deltaTime * moveSpeed, Space.Self);
        else if(!anim.GetBool("canBite") && !isMovingForwards)
            transform.position = Vector3.MoveTowards(transform.position, originalPos, moveSpeed * Time.deltaTime);

        if (hasReturned)
            enabled = false;
    }

    IEnumerator ChangeMoveDirectionDelay()
    {
        yield return new WaitForSeconds(timeToMoveForward);
        isMovingForwards = false;
        //bite animation starts here
        anim.SetBool("canBite", true);
        biteBox.GetComponent<BoxCollider2D>().enabled = true;
        ToggleHeadSpawns(true);
        yield return new WaitForSeconds(timeToBite);
        ToggleHeadSpawns(false);
        biteBox.GetComponent<BoxCollider2D>().enabled = false;
        anim.SetBool("canBite", false);
        yield return null;
    }

    private void ToggleHeadSpawns(bool toggle)
    {
        foreach (GameObject headSpawn in headSpawns)
            headSpawn.SetActive(toggle);
    }

    private bool ApproxEqual(Vector3 a, Vector3 b, float tolerance)
    {
        return AlmostEqual(a.x, b.x, tolerance) && AlmostEqual(a.y, b.y, tolerance) && AlmostEqual(a.z, b.z, tolerance);
    }

    private bool AlmostEqual(float a, float b, float epsilon)
    {
        return Mathf.Abs(a - b) < epsilon;
    }
}
