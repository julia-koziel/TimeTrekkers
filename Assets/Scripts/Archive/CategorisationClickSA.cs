using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorisationClickSA : MonoBehaviour
{ 
    
    private CategorisationPretestSA logic;
    public GameObject targetShip;
    public GameObject tick;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        logic = FindObjectOfType<CategorisationPretestSA>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("clicked");

        logic.correctclicked();
        tick.SetActive(true);
        animator.SetTrigger("Click");
        StartCoroutine(clickdelay());

    }

      private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1f); 
        tick.SetActive(false);
    }

}
