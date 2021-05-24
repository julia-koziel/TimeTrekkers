using UnityEngine;

public class MC_OpeningSequence : MonoBehaviour 
{
    Camera cam;
    Movement sequence;
    Vector3 camStart = new Vector3(-160, -230, -10);
    Vector3 camEnd = new Vector3(0, 0, -10);
    float camStartSize = 300;
    float camEndSize = 5;
    float camZoomSize = 0.6f;
    public SpaceshipParticleSystem spaceshipParticleSystem;
    public GameEvent stageEnd;
    public FloatVariable speed;
    float defaultSpeed;
    float slowerSpeed = 0.7f;
    float defaultLifetime;
    float longLifetime = 100;
    public FloatVariable coherence;
    public CategoricalInputVariable direction;
    public IntVariable Left;
    public IntVariable Right;
    AudioTranslator audioTranslator;
    public TranslatableAudioClip itsNightTime;
    public TranslatableAudioClip thereAreTwoPlanets;
    public TranslatableAudioClip andLookThereAreThousandsOfSpaceships;

    void Awake()
    {
        audioTranslator = GetComponent<AudioTranslator>();
        defaultSpeed = speed;
        defaultLifetime = spaceshipParticleSystem.lifetime;
        cam = Camera.main;
        sequence = this.BuildMovement()
                        .First(run: time => {})
                        .Then(() => {
                            cam.orthographicSize = camStartSize;
                            cam.transform.position = camStart;
                            coherence.Value = 0.9f;
                            direction.Value = Left;
                        })
                        .PlayAndWait(itsNightTime, audioTranslator, minWaitTime: 1)
                        .Then(run: time => {
                            cam.orthographicSize = camStartSize + (camEndSize - camStartSize) * Mathf.Pow(time, 0.3f);
                            cam.transform.position = camStart + (camEnd - camStart) * Mathf.Pow(time, 0.3f);
                        }, duration: 2)
                        .PlayAndWait(thereAreTwoPlanets, audioTranslator, minWaitTime: 1)
                        .Then(() => { 
                            spaceshipParticleSystem.lifetime = longLifetime;
                            spaceshipParticleSystem.switchOn();
                        })
                        .PlayAndWait(andLookThereAreThousandsOfSpaceships, audioTranslator, minWaitTime: 2)
                        .Then(() => direction.Value = Right)
                        .Wait(0.5f)
                        .Then(run: time => {
                            cam.orthographicSize = camEndSize + (camZoomSize - camEndSize) * Mathf.Sqrt(time);
                            speed.Value = defaultSpeed + (slowerSpeed - defaultSpeed) * Mathf.Sqrt(time);
                        })
                        .Wait(3)
                        .Then(run: time => {
                            cam.orthographicSize = camZoomSize + (camEndSize - camZoomSize) * Mathf.Sqrt(time);
                            speed.Value = slowerSpeed + (defaultSpeed - slowerSpeed) * Mathf.Sqrt(time);
                        })
                        .Wait(2)
                        .Then(() => { 
                            spaceshipParticleSystem.lifetime = defaultLifetime;
                            stageEnd.Raise();
                        });

    }

    void OnEnable() => sequence.Start();

    public void Reset()
    {
        sequence.Stop();
        audioTranslator.audioSource.Stop();
        cam.transform.position = camEnd;
        cam.orthographicSize = camEndSize;
        speed.Value = defaultSpeed;
        if (defaultLifetime != 0) spaceshipParticleSystem.lifetime = defaultLifetime;
    }
}