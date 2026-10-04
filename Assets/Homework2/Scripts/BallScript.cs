using UnityEngine;

public class BallScript : MonoBehaviour
{

    private bool isThrown = false;
    private Vector3 originPosition = Vector3.zero;

    [SerializeField] private GameObject targetPos;
    [SerializeField] private float targetPosRandomRange = 2f;


    [SerializeField] private float FowardSpeed = 500;

    private void Start()
    {
        originPosition = this.transform.position;
    }

    public void ThrowBall()
    {
        if (isThrown) { return; }

        isThrown = true;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null) return;
        if (targetPos == null) return;

        Vector3 pos = targetPos.transform.position + new Vector3(
            Random.Range(-targetPosRandomRange, targetPosRandomRange), 0, 0
        );
        Vector3 direction = pos - this.transform.position;

        rigidbody.AddForce(direction.normalized * FowardSpeed);
    }

    public void ResetState()
    {
        isThrown = false;
        this.transform.position = originPosition;
        this.transform.rotation = Quaternion.identity;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; 
    }
}
