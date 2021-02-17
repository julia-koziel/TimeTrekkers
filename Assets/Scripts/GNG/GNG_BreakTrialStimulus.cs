using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class GNG_BreakTrialStimulus : MonoBehaviour 
{
    bool isMoving = false;
    bool pauseInCentre = true;
    public GameEvent breakTrialEnd;
    public FloatVariable speed;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 stopPosition;
    Vector3 direction;
    Animator animator;
    AudioSource sound;
    float soundDelay = 3;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
        direction = (endPosition - startPosition).normalized;
        sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void OnEnable() => isMoving = true;

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position += direction * Time.deltaTime * speed;
            
            if (pauseInCentre && (stopPosition - transform.position).normalized != direction)
            {
                isMoving = false;
                animator.SetBool("isStopped", true);
                StartCoroutine(playSoundForAttention());
            }

            if ((endPosition - transform.position).normalized != direction)
            {
                this.In(0.5f).Call(() => breakTrialEnd.Raise());
            }
        }
    }

    IEnumerator playSoundForAttention()
    {
        while (true)
        {
            sound.Play();
            yield return new WaitForSeconds(soundDelay);
        }
    }

    public void OnMouseDown()
    {
        sound.Play();

        pauseInCentre = false;
        isMoving = true;
        animator.SetBool("isStopped", false);
        StopAllCoroutines();
    }

    public void Reset()
    {
        isMoving = false;
        pauseInCentre = true;
        transform.position = startPosition;
    }
}