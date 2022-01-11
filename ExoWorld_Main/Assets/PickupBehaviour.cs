using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            collision.GetComponent<Movement>().oxygenLevel += 0.2f;
            if (collision.GetComponent<Movement>().oxygenLevel > 1)
            {
                collision.GetComponent<Movement>().oxygenLevel = 1;
            }
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
