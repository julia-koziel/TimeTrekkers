using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipParticle : MonoBehaviour
{
    public SpaceshipParticleSystem ps;
    float dotsRadius { get => ps.dotsRadius; }
    public FloatVariable coherence;
    public FloatVariable direction;
    public FloatVariable LEFT;
    Vector3 dir { get => direction.Value == LEFT ? Vector3.left : Vector3.right; }
    Vector3 randomDir;
    public IntVariable nSpaceships;
    private bool isCoherent = true;
    public FloatVariable speed;
    SpriteRenderer rend;
    CircleCollider2D circleCollider;
    Rigidbody2D rb;
    float time;
    float lifetime;
    public bool shouldHandleCollision = true;
    Color normalColor;
    Color semiTransparentColor;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isCoherent = Random.Range(0.0f, 1.0f) < coherence;
        randomDir = Random.insideUnitCircle.normalized;
        rend = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        lifetime = Random.Range(0.02f, ps.lifetime);
        normalColor = rend.color;
        semiTransparentColor = rend.color;
        semiTransparentColor.a = 0.5f;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > lifetime)
        {
            isCoherent = Random.Range(0.0f, 1.0f) < coherence;
            randomDir = Random.insideUnitCircle.normalized;
            moveToRandomPosition();
            time = 0;

            lifetime = Random.Range(ps.lifetime - 0.02f, ps.lifetime + 0.02f);
        }
        else
        {
            if (time > lifetime - Time.deltaTime) rend.color = semiTransparentColor;
            else if (time > Time.deltaTime) rend.color = normalColor;

            var direction = isCoherent ? dir : randomDir;
            rb.MovePosition(transform.position + direction * Time.deltaTime * speed);
            if (Vector3.Magnitude(rb.position) > dotsRadius)
            {
                var radiusToCentre = (rb.position - Vector2.zero).normalized * dotsRadius;
                var newPos = rb.position - 2 * radiusToCentre;
                if (ps.wouldCollide(newPos)) newPos = ps.randomPos();
                rb.position = newPos;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 8)
        {
            if (shouldHandleCollision)
            {
                var otherSpaceship = other.GetComponent<SpaceshipParticle>();
                otherSpaceship.shouldHandleCollision = false;
                otherSpaceship.moveToRandomPosition();
            }
            else shouldHandleCollision = true; // For next collision   
        }
    }

    public void moveToRandomPosition()
    {
        transform.position = ps.randomPos();
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    void OnDestroy() => nSpaceships--;
}
