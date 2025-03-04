using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 init_pos;
    public float shake_dur = 0f;
    public float shake_amount = 10.0f;

    void Start()
    {
        init_pos = transform.position;
    }

    public void StartShake(float duration, float amount)
    {
        shake_dur = duration;
        shake_amount = amount;
        init_pos = transform.position; // Store the original position
    }

    void Update()
    {
        if (shake_dur > 0)
        {
            Debug.Log("shaking");
            transform.position = init_pos + (Vector3)Random.insideUnitCircle * shake_amount;

            shake_dur -= Time.deltaTime;
            if (shake_dur <= 0)
            {
                transform.position = init_pos; // Reset position
            }
        }
    }
}
