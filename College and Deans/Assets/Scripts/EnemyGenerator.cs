using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private Dictionary<int, List<Enemy>> setFacil = new Dictionary<int, List<Enemy>>();
    private int numSetsFacil = 5;
    [SerializeField] private Enemy enemy0;
    [SerializeField] private Enemy enemy1;

    public List<Transform> spawns;

    private void Awake() 
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        CreacionSets();
        SpawnEnemies("facil", spawns);
    }

    //Creacion de los diferentes set de enemigos
    private void CreacionSets()
    {
        List<Enemy> set0 = new List<Enemy>();
        List<Enemy> set1 = new List<Enemy>();  
        List<Enemy> set2 = new List<Enemy>();
        List<Enemy> set3 = new List<Enemy>();  
        List<Enemy> set4 = new List<Enemy>();

        //Creacion set 0
        for (int i = 0; i < 4; i++)
        {
            set0.Add(enemy0);
        }

        //Creacion set 1
        for (int i = 0; i < 3; i++)
        {
            set1.Add(enemy0);
        }
        set1.Add(enemy1);

        //Creacion set 2
        set2.Add(enemy0);
        set2.Add(enemy0);
        set2.Add(enemy1);
        set2.Add(enemy1);

        //Creacion set 3
        set3.Add(enemy0);
        set3.Add(enemy1);
        set3.Add(enemy1);
        set3.Add(enemy1);

        //Creacion set 4
        for (int i = 0; i < 4; i++)
        {
            set4.Add(enemy1);
        }

        //Insercion de los sets en el diccionario
        setFacil.Add(0, set0);
        setFacil.Add(1, set1);
        setFacil.Add(2, set2);
        setFacil.Add(3, set3);
        setFacil.Add(4, set4);
    }
    
    //Metodo para generar los enemigos de cada sala:
    //Recibe la dificultad de la sala y una lista con los lugares donde pueden hacer spawn los enemigos
    public void SpawnEnemies(string tipoSala, List<Transform> spawns)
    {
        switch(tipoSala)
        {
            case "facil":
                int random = Random.Range(0, numSetsFacil);
                List<Enemy> setRandom;
                setFacil.TryGetValue(random, out setRandom);
                
                do
                {
                    Enemy temp = setRandom[0];
                    Transform spawnPosition = spawns[Random.Range(0, spawns.Count)];
                    Instantiate(temp, spawnPosition);
                    spawns.Remove(spawnPosition);
                    setRandom.Remove(temp);
                } while (setRandom.Count != 0);

                break;
            case "medio": 
                break;
            case "dificil":
                break;
        }
    }
}