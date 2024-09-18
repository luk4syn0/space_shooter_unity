using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VelocityVisualizer : MonoBehaviour
{
    // The length of the arrow, in meters
    [SerializeField] [Range(0.5F, 2)] private float arrowLength = 1.0F;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        var position = transform.position;
        var velocity = _rigidbody.velocity;

        if (velocity.magnitude < 0.1f) return;

        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, position, Quaternion.LookRotation(velocity), arrowLength, EventType.Repaint);
    }
}