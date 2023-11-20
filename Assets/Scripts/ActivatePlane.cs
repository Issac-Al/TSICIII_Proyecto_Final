using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActivatePlane : MonoBehaviour
{
    public List<GameObject> builders;
    public GameObject planeFinder;
    public void SetWorld(int target)
    {
        builders[target].SetActive(true);
        planeFinder.SetActive(true);
    }
}
