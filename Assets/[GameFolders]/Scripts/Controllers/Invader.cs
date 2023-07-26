using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
public class Invader : MonoBehaviour
{
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

        if (isInvading)
        {
            invadeCounter += Time.deltaTime;
            Debug.Log("Invading");

            if (invadeCounter >= invadeTime)
            {
                Invaded();
            }
        }
    }

    private void Invaded()
    {
        Debug.Log("Invaded");
        isInvading = false;
        targetGrid.Invaded();
    }

    public void CancelInvade()
    {
        isInvading = false;
    }
}
