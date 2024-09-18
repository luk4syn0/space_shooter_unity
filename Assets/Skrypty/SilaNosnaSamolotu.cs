using UnityEngine;

public class SilaNosnaSamolotu : MonoBehaviour
{
    
    public float liftCoefficient = 0.1f; // Współczynnik siły nośnej
    public float baseGravityScale = 1f;  // Podstawowa skala grawitacji przy zerowej prędkości
    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Obliczanie prędkości samolotu
        speed = rb.velocity.x;

        // Obliczanie siły nośnej proporcjonalnej do kwadratu prędkości
        float lift = liftCoefficient * Mathf.Pow(speed, 2);

        // Ustawienie nowej skali grawitacji, która zmniejsza się wraz z rosnącą siłą nośną
        rb.gravityScale = Mathf.Max(baseGravityScale - lift, 0.0f);
    }
}
