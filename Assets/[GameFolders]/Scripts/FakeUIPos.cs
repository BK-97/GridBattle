using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeUIPos : MonoBehaviour
{
    public RectTransform screenOverlay;
    private Vector3 offSet;
    private void Start()
    {
        offSet = new Vector3(0,0,-Camera.main.transform.position.z);
    }
    private void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenOverlay.position+offSet);

        transform.position = worldPosition;
    }
}