using UnityEngine;
using GridSystem.Managers;

public class Pointer : MonoBehaviour
{
    private void Awake()
    {
        InputManager.Instance.SetPointer(transform);
    }
}
