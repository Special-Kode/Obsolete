using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class DungeonGeneratorManager : MonoBehaviour
{
    public enum RandomRoomNumberMethod
    {
        Fixed = 0, Random = 1
    };

    public RandomRoomNumberMethod GenerationMethod; //Generates a fixed number of rooms or a random number of rooms

    public int FixedValue = 8;

    //Bounds of the random method
    public int RandomLowerBound = 5;
    public int RandomUpperBound = 10;

    public int MoveAmount = 10; //Distance between rooms

    public GameObject Room; //Room prefab
    //public GameObject[] RoomList; //List of rooms for further use

    public Vector2 currentPos = Vector2.zero;

    [SerializeField] private List<GameObject> roomList;
    [SerializeField] private List<Vector2> positions;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();
        roomList = new List<GameObject>();
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateLevel()
    {
        switch (GenerationMethod)
        {
            case RandomRoomNumberMethod.Fixed:
                GenerateProcLevel(FixedValue);
                break;
            case RandomRoomNumberMethod.Random:
                int random = UnityEngine.Random.Range(RandomLowerBound, RandomUpperBound + 1);
                GenerateProcLevel(random);
                break;
        }
    }

    void GenerateProcLevel(int num)
    {
        for (int i = 0; i < num;)
        {
            int rand = UnityEngine.Random.Range(0, 4);

            if (!positions.Contains(currentPos))
            {
                var room = Instantiate(Room, currentPos, Quaternion.identity);
                if (i == 0)
                {
                    room.GetComponent<RoomBehaviour>().roomType = RoomBehaviour.RoomType.Spawn;
                    foreach (var wall in room.GetComponentsInChildren<SpriteRenderer>())
                    {
                        wall.color = Color.cyan;
                    }
                    foreach (var wall in room.GetComponentsInChildren<Tilemap>())
                    {
                        wall.color = Color.cyan;
                    }
                }
                else if (i == num - 1)
                {
                    room.GetComponent<RoomBehaviour>().roomType = RoomBehaviour.RoomType.Boss;
                    foreach (var wall in room.GetComponentsInChildren<SpriteRenderer>())
                    {
                        wall.color = Color.red;
                    }
                    foreach (var wall in room.GetComponentsInChildren<Tilemap>())
                    {
                        wall.color = Color.red;
                    }
                }
                else
                    room.GetComponent<RoomBehaviour>().roomType = RoomBehaviour.RoomType.Enemies;


                roomList.Add(room);
                positions.Add(currentPos);

                i++;
            }

            switch (rand)
            {
                case 0:
                    currentPos += Vector2.up * MoveAmount;
                    break;
                case 1:
                    currentPos += Vector2.down * MoveAmount;
                    break;
                case 2:
                    currentPos += Vector2.left * MoveAmount;
                    break;
                case 3:
                    currentPos += Vector2.right * MoveAmount;
                    break;
            }
        }
    }
}