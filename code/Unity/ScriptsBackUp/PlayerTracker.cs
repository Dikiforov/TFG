using UnityEngine;

public class PlayerMovementTracker : MonoBehaviour
{
    public bool IsMoving { get; private set; } = false;

    private Vector3 lastPosition;

    private void Update()
    {
        if (transform.position != lastPosition)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        lastPosition = transform.position;
    }
}
