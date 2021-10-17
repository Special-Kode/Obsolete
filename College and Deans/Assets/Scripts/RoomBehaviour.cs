using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public Vector2 position;

    public enum RoomType
    {
        Spawn, Enemies, Boss, Cafe, Loot
    };

    public RoomType roomType;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Room spawned!");
        position = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
