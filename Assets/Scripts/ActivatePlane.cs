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
        planeFinder.SetActive(true);
    }

    public void SetBuilder()
    {
        builders[0].SetActive(true);
    }
}
