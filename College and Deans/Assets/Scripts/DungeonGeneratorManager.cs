using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DungeonGeneratorManager : MonoBehaviour
{
    public enum RandomRoomNumberMethod
    {
        Fixed = 0, Random = 1
    };

    public RandomRoomNumberMethod GenerationMethod;

    public int FixedValue = 8;

    public int RandomLowerBound = 5;
    public int RandomUpperBound = 10;

    public int MoveAmount = 10;

    public GameObject Room;
    //public GameObject[] Rooms;

    public Vector2 currentPos = Vector2.zero;

    private List<Vector2> positions;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();
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
                    foreach (var wall in room.GetComponentsInChildren<SpriteRenderer>())
                    {
                        wall.color = Color.cyan;
                    }
                }
                if (i == num - 1)
                {
                    foreach (var wall in room.GetComponentsInChildren<SpriteRenderer>())
                    {
                        wall.color = Color.red;
                    }
                }

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