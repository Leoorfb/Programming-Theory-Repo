using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperUnit : Unit
{
    public float jumpForce = 2F;

    private Rigidbody rigidbody_;
    private float distToGround;
    private bool IsGrounded_ = true;

    protected override void Start()
    {
        base.Start();
        rigidbody_ = GetComponent<Rigidbody>();
        distToGround = ((GetComponent<CapsuleCollider>().height)/2) + 0.1f;
    }
    protected override IEnumerator MoveTo(Vector3 position)
    {
        position = new Vector3(position.x, transform.position.y, position.z);
        Debug.Log("Entrou corrotina");
        float step = 0.1f;

        while (DistanceTo(position) > step)
        {
            if (IsGrounded_)
            {
                Jump();
            }

            //Debug.Log("corrotina while " + Vector3.Distance(transform.position, position) + " - " + step);
            Vector3 direction = (position - transform.position).normalized;
            //Debug.Log(direction);
            step = speed * Time.deltaTime;
            transform.Translate(direction * step);
            yield return null;
        }
        Debug.Log("Terminou corrotina");
    }

    private float DistanceTo(Vector3 position)
    {
        //return (Vector2.Distance(new Vector2(position.x, position.z), new Vector2(transform.position.x, transform.position.z)));
        return (new Vector2(position.x - transform.position.x, position.z - transform.position.z).magnitude);
    }

    protected bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded_ = true;
        }
    }

    private void Jump()
    {
        rigidbody_.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        IsGrounded_ = false;
    }
}
