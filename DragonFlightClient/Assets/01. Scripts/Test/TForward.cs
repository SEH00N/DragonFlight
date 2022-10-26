using UnityEngine;

public class TForward : MonoBehaviour
{
    [SerializeField] Vector3 f;

    private void Update()
    {
        f = transform.forward;
        transform.position += (Time.deltaTime * transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red, 1f);
    }
}
