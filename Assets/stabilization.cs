using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stabilization : movement
{
    [Header("Upright Settings")]
    public float uprightStrength = 10f;
    public float uprightDamping = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        KeepUpRight();
    }

    void KeepUpRight()
    {
        Vector3 currentUp = transform.up;
        Vector3 desiredUp = Vector3.up;

        Vector3 tiltAxis = Vector3.Cross(currentUp, desiredUp);

        float tiltAmount = Vector3.Dot(currentUp, desiredUp);

        tiltAmount = Mathf.Clamp(tiltAmount, -1f, 1f);

        float angle = Mathf.Acos(tiltAmount);

        if (angle < .001f || tiltAxis == Vector3.zero)
        {
            return;
        }

        tiltAxis.Normalize();

        tiltAxis = Vector3.ProjectOnPlane(tiltAxis, transform.up);
        if (tiltAxis == Vector3.zero)
        {
            return;
        }

        float strength = 500f;
        float damping = 50f;

        Vector3 correctiveTorque = tiltAxis * angle * strength - rigid.angularVelocity * damping;

        if (!float.IsNaN(correctiveTorque.x))
        {
            rigid.AddTorque(correctiveTorque, ForceMode.Acceleration);
        }
        /*Quaternion desiredRotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        Quaternion rotError = desiredRotation * Quaternion.Inverse(rigid.rotation);
        rotError.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f)
        {
            angle -= 360f;
        }

        Vector3 torque = axis * (angle * Mathf.Deg2Rad * uprightStrength) - rigid.angularVelocity * uprightDamping;*/
        //rigid.AddTorque(torque, ForceMode.Acceleration);
    }
}
