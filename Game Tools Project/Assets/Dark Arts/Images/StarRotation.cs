using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotation : MonoBehaviour
{
    public GameObject starsN;
    public GameObject starsM;
    public GameObject starsF;

    public GameObject cloud1;
    public GameObject cloud2;

    public float posXc1, posXc2, posY;

    void Start()
    {
        posXc1 = 0;
        posXc2 = 2640;
        posY = 838f;
    }

    // Update is called once per frame
    void Update()
    {
        //posY = 838f;
        posXc1 -= 10.3f*Time.deltaTime;
            cloud1.transform.position = new Vector3(posXc1, posY, 0);
        if (posXc1 <= -2300)
        {
            posXc1 = 2641;
        }


        posXc2 -= 10.3f * Time.deltaTime;
            cloud2.transform.position = new Vector3(posXc2, posY, 0);
        if (posXc2 <= -2300)
        {
            posXc2 = 2641;
        }

            starsN.transform.Rotate(0f, 0f, -1.1f * Time.deltaTime);
            starsM.transform.Rotate(0f, 0f, -0.3f * Time.deltaTime);
            starsF.transform.Rotate(0f, 0f, -0.8f * Time.deltaTime);
        
    }
}
