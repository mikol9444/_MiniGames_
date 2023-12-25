using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _09BoardCreator : MonoBehaviour
{   [SerializeField] private int length=9;
    [SerializeField] private int width=9;
    [SerializeField] GameObject cellPrefab;
    _09GameLogic gameLogic;

    private void Start() {

        
        SpawnBoard();
    }
    private void SpawnBoard(){
        gameLogic=FindObjectOfType<_09GameLogic>();
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Vector3 spawnPosition = new Vector3(i,0,j);
                GameObject obj =ObjectPoolManager.SpawnObject(cellPrefab,spawnPosition,Quaternion.identity,PoolType.GameObject);
                 _09Cell cell = obj.AddComponent<_09Cell>();
                 cell.Initialize(i,j);
                gameLogic.AddCell(cell,i,j);
            }
        }

    }
}

