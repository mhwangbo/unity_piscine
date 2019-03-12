using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject fence;
    public GameObject fanParticle;
    public GameObject lights;
    public MainController mainController;

    public void TakeAction(int obj)
    {
        switch (obj)
        {
            case 1:
                fanParticle.SetActive(true);
                objects[0].transform.name = "ActivatedFan";
                break;
            case 2:
                mainController.cardKey = true;
                objects[1].GetComponent<AudioSource>().Play();
                Destroy(objects[1]);
                lights.SetActive(true);
                lights.GetComponent<AudioSource>().Play();
                break;
            case 3:
                if (mainController.cardKey)
                {
                    mainController.cardKey = false;
                    objects[2].GetComponent<AudioSource>().Play();
                    Destroy(fence);
                }
                break;
            case 4:
                mainController.document = true;
                Destroy(objects[3]);
                break;
        }
    }
}