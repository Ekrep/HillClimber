using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;

    public List<Hand> hands;

    private void Awake()
    {
        Instance = this;
    }
}
