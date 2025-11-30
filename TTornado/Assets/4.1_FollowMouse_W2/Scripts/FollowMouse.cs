using Unity.VisualScripting;
using UnityEngine;


public class FollowMouse : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    void Update()
    {

        Vector3 mousePosition = Input.mousePosition;//screen space
        mousePosition.z = Camera.main.transform.position.y;

        ////object in world space
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Ray ray = new Ray(Camera.main.transform.position, worldPosition - Camera.main.transform.position);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100000, 1 << 8))
        {
            ValueManager.WorldMousePosition = hit.point;
           
        }
        //transform.position = Vector3.Lerp(transform.position, worldPosition, Speed * Time.deltaTime);

        //ValueManager.WorldMousePosition = worldPosition;

    }
   
}
