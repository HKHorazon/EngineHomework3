using UnityEngine;

public class PinScript : MonoBehaviour
{

    private Vector3 originPosition = Vector3.zero;


    private void Start()
    {
        originPosition = this.transform.position;
    }


    public void ResetState()
    {
        this.transform.position = originPosition;
        this.transform.rotation = Quaternion.identity;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
}
