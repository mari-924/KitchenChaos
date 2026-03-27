using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKichenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnedAmount;
    private int plateSpawnedAmountMax = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if(plateSpawnedAmount < plateSpawnedAmountMax)
            {
                plateSpawnedAmount++;
            }
        }
    }
}
