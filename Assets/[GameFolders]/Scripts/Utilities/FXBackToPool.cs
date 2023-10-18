using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXBackToPool : MonoBehaviour
{
    private void OnDisable()
    {
        PoolingSystem.ReturnObjectToPool(gameObject);
    }
}