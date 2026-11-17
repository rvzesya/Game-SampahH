using UnityEngine;
using UnityEngine.UI;

public class DeteksiSampah : MonoBehaviour
{
    [SerializeField] private string nameTag;
    [SerializeField] private AudioClip audioBenar;
    [SerializeField] private AudioClip audioSalah;
    [SerializeField] private Text textScore;

    private AudioSource mediaPlayerBenar;
    private AudioSource mediaPlayerSalah;

    private void Start()
    {
        mediaPlayerBenar = gameObject.AddComponent<AudioSource>();
        mediaPlayerBenar.clip = audioBenar;

        mediaPlayerSalah = gameObject.AddComponent<AudioSource>();
        mediaPlayerSalah.clip = audioSalah;

        if (textScore != null)
            textScore.text = Data.score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || Data.isGameOver)
            return;

        bool benar = collision.CompareTag(nameTag);
        Data.score += benar ? 25 : -5;

        if (textScore != null)
            textScore.text = Data.score.ToString();

        Destroy(collision.gameObject);

        if (benar && mediaPlayerBenar != null)
            mediaPlayerBenar.Play();
        else if (!benar && mediaPlayerSalah != null)
            mediaPlayerSalah.Play();
    }
}
