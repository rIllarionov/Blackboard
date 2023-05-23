
using UnityEngine;


public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _markerOffset;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Input.mousePosition+_markerOffset;
    }
}
