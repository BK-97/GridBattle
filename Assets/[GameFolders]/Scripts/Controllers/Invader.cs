using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
public class Invader : MonoBehaviour
{
    [HideInInspector]
    public GridSystem.Grid targetGrid;
    private float invadeTime = 4;
    private bool isInvading = false;
    private float invadeCounter = 0f;
    public void StartInvading()
    {
        isInvading = true;
        invadeCounter = 0f;
    }

    public void Invading()
    {
        if (!gameObject.activeSelf)
            return;
        if (isInvading)
        {
            invadeCounter += Time.deltaTime;

            targetGrid.Invading(invadeTime);
            if (invadeCounter >= invadeTime)
            {
                Invaded();
            }
        }
    }

    private void Invaded()
    {
        isInvading = false;
        targetGrid.Invaded();
    }

    public void CancelInvade()
    {
        isInvading = false;
        if(targetGrid!=null)
            targetGrid.CancelInvading();
    }
}
