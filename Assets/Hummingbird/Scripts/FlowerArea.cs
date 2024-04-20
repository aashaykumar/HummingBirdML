using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerArea : MonoBehaviour
{
    
    public const float AearDiameter = 20f;

    private List<GameObject> _flowerPlants;
    private Dictionary<Collider, Flower> _NectarFlowerDictionary;

    public List<Flower> Flowers { get; private set; }


    public void ResetFlowers()
    {
        foreach (var flower in _flowerPlants)
        {
            float xRot = UnityEngine.Random.Range(-5f, 5f);
            float yRot = UnityEngine.Random.Range(-180f, 180f);
            float zRot = UnityEngine.Random.Range(-5f, 5f);
            flower.transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
        }

        foreach (Flower flower in Flowers)
        {
            flower.ResetFlower();
        }
    }

    public Flower GetFlowerFromNectar(Collider collider)
    {
        return _NectarFlowerDictionary[collider];
    }

    private void Awake()
    {
       _flowerPlants = new List<GameObject>();
        _NectarFlowerDictionary = new Dictionary<Collider, Flower>();
        Flowers = new List<Flower>();
    }

    private void Start()
    {
        FindChildFlowers(transform);
    }

    private void FindChildFlowers(Transform parent)
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            if (child.CompareTag("flower_plant"))
            {
                _flowerPlants.Add(child.gameObject);
                FindChildFlowers(child);
            }
            else
            {
                Flower flower = child.GetComponent<Flower>();

                if(flower != null)
                {
                    Flowers.Add(flower);
                    _NectarFlowerDictionary.Add(flower.nectarCollider, flower);
                }
                else
                {
                    FindChildFlowers(child);
                }
            }
        }
    }
}
