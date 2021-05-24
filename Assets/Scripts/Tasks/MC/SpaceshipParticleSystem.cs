using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipParticleSystem : MonoBehaviour
{
    public GameObject spaceshipPrefab;
    public int goalNumParticles;
    public float dotsRadius;
    public IntVariable nSpaceships;
    public bool isOn = true;
    float colliderRadius;
    int groupCounter = 0;
    public float lifetime;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        colliderRadius = spaceshipPrefab.GetComponent<CircleCollider2D>().radius;
        colliderRadius = transform.TransformVector(colliderRadius, 0, 0).x;
        mask = LayerMask.GetMask("Spaceship");
    }

    // Update is called once per frame
    void Update()
    {
        // if (isOn)
        // {
        //     if (nSpaceships > goalNumParticles)
        //     {
        //         print("help");
        //     }
        //     while (nSpaceships < goalNumParticles)
        //     {
        //         createNewSpaceship();
        //     }
        // }
    }

    void createSpaceships()
    {
        while (nSpaceships < goalNumParticles)
        {
            createNewSpaceship();
        }
    }

    private void createNewSpaceship()
    {
        var pos = randomPos();
        GameObject spaceship = Instantiate(spaceshipPrefab, pos, Quaternion.identity, transform);
        nSpaceships++;
    }

    public void setDirection(Vector3 dir)
    {
        // TODO set floatvar direction
    }

    public void setDirection(int dir)
    {
        // TODO handle
    }

    public void setCoherence(float coh)
    {
        // TODO set floatvar coh
    }

    public void setParams(int dir, float coh)
    {
        // TODO handle
    }

    public void setParams(Vector3 dir, float coh)
    {
        // TODO handle
    }

    public void switchOn()
    {
        createSpaceships();
    }

    public Vector3 randomPos()
    {
        Vector3 pos;
        do
        {
            pos = Random.insideUnitCircle * dotsRadius;
        } while (wouldCollide(pos));
        return pos;
    }

    public bool wouldCollide(Vector3 pos)
    {
        return Physics2D.OverlapCircle(pos, colliderRadius, mask) != null;
    }

    public int getGroup()
    {
        groupCounter++;
        if (groupCounter == 2) groupCounter = 0;

        return groupCounter;
    }
}
