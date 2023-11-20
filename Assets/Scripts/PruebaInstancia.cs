using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaInstancia : MonoBehaviour
{
    public GameObject pieza;
    public Transform imageTarget;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(pieza, Vector3.zero, Quaternion.identity, imageTarget);
    }

    // Update is called once per frame
}
