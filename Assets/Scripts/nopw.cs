using UnityEngine;

public class nopw : MonoBehaviour
{
    public GameObject projectile;
    private int full_auto;
    public int gun;
    void start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("yo");
            if (gun == 1)
            {
                Debug.Log("gun " + gun);
                gun = 2;
            }
            if (gun == 2)
            {
                Debug.Log("gun " + gun);
                gun = 3;
            }
            if (gun == 3)
            {
                Debug.Log("gun " + gun); 
                gun = 1;  
            }
        }

            if (Input.GetButton("Fire1"))
            {
            
            if (gun == 1)
            {
                full_auto++;
                if (full_auto == 100)
                {
                    var clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
                    //Destroy after 2 seconds to stop clutter.
                    Destroy(clone, 5.0f);
                    full_auto = 0;

                }
            }
            if (gun == 2)
            {
                full_auto++;
                if (full_auto == 50)
                {
                    var clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
                    //Destroy after 2 seconds to stop clutter.
                    Destroy(clone, 5.0f);
                    full_auto = 0;

                }
            }
            if (gun == 3)
            {
                full_auto++;
                if (full_auto == 30)
                {
                    var clone = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
                    //Destroy after 2 seconds to stop clutter.
                    Destroy(clone, 5.0f);
                    full_auto = 0;

                }
            }
        }
        
    }
}
