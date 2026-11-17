using UnityEngine;

public class MunculkanSampah : MonoBehaviour
{
    [SerializeField] private float jeda = 0.8f;
    [SerializeField] private GameObject[] obyekSampah;

    private float timer;

    private void Update()
    {
        if (Data.isGameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= jeda && obyekSampah != null && obyekSampah.Length > 0)
        {
            int randomIndex = Random.Range(0, obyekSampah.Length);
            Instantiate(obyekSampah[randomIndex], transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
