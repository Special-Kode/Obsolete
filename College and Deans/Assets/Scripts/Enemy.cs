using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int id = 0;
    
    public int GetId()
    {
        return id;
    }
}
