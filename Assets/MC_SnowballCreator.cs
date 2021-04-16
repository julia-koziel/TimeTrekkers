using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_SnowballCreator : MonoBehaviour
{ 
    public GameObject snowballPrefab;
    public int goalNumParticles;
    public float dotsRadius;
    public IntVariable nSnowballs;
    public bool isOn = true;
    float colliderRadius;
    int groupCounter = 0;
    public float lifetime;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        colliderRadius = snowballPrefab.GetComponent<CircleCollider2D>().radius;
        colliderRadius = transform.TransformVector(colliderRadius, 0, 0).x;
        mask = LayerMask.GetMask("snowball");
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (nSnowballs > goalNumParticles)
            {
                print("help");
            }
            while (nSnowballs < goalNumParticles)
            {
                createNewsnowball();
            }
        }
    }

    void createSnowballs()
    {
        while (nSnowballs < goalNumParticles)
        {
            createNewsnowball();
        }
    }

    private void createNewsnowball()
    {
        var pos = randomPos();
        GameObject snowball = Instantiate(snowballPrefab, pos, Quaternion.identity, transform);
        Debug.Log("prefabinstan");
        nSnowballs++;
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
        createSnowballs();
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
