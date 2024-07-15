using UnityEngine;
using UnityEngine.UI;

public class TimerTrigger : MonoBehaviour
{
    private float timer;
    private bool isPlayerInside;
    private bool isDarkening;
    private bool isFadingOut;
    private float darkenTimer;
    private float fadeOutTimer;

    public Text timerText;
    public Image darkenImage; // UI элемент для затемнения экрана
    public Transform teleportDestination; // Точка телепортации
    public AudioSource audioSource; // Аудиоисточник для увеличения громкости
    public float fadeDuration = 5f; // Длительность затемнения/убирания затемнения
    public float minVolume = 0.5f; // Минимальная громкость аудиоисточника

    private void Start()
    {
        timer = 0f;
        darkenTimer = 0f;
        fadeOutTimer = 0f;
        isPlayerInside = false;
        isDarkening = false;
        isFadingOut = false;
        darkenImage.color = new Color(0, 0, 0, 0); // Убедитесь, что экран прозрачный в начале
        if (audioSource != null)
        {
            audioSource.volume = minVolume; // Убедитесь, что громкость аудиоисточника начально равна минимальной громкости
        }
    }

    private void Update()
    {
        if (isPlayerInside && !isDarkening)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();

            if (timer >= 5f)
            {
                StartDarkening();
            }
        }
        else if (isDarkening)
        {
            darkenTimer += Time.deltaTime;
            float alpha = darkenTimer / fadeDuration; // Затемнение на протяжении fadeDuration секунд
            darkenImage.color = new Color(0, 0, 0, Mathf.Clamp01(alpha));

            if (audioSource != null)
            {
                audioSource.volume = Mathf.Clamp(minVolume + (1f - minVolume) * alpha, minVolume, 1f); // Увеличение громкости аудиоисточника
            }

            if (darkenTimer >= fadeDuration)
            {
                if (isPlayerInside)
                {
                    TeleportPlayer();
                }
                ResetDarkening();
            }
        }
        else if (isFadingOut)
        {
            fadeOutTimer += Time.deltaTime;
            float alpha = 1f - (fadeOutTimer / fadeDuration); // Убирание затемнения на протяжении fadeDuration секунд
            darkenImage.color = new Color(0, 0, 0, Mathf.Clamp01(alpha));

            if (audioSource != null)
            {
                audioSource.volume = Mathf.Clamp(minVolume + (1f - minVolume) * alpha, minVolume, 1f); // Уменьшение громкости аудиоисточника
            }

            if (fadeOutTimer >= fadeDuration)
            {
                ResetFadeOut();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            isFadingOut = false;
            fadeOutTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            ResetTimer();
            if (isDarkening)
            {
                StartFadeOut();
            }
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Timer: " + timer.ToString("F2") + " seconds";
        }
    }

    private void StartDarkening()
    {
        isDarkening = true;
        darkenTimer = 0f;
    }

    private void ResetTimer()
    {
        timer = 0f;
        UpdateTimerUI();
    }

    private void ResetDarkening()
    {
        isDarkening = false;
        darkenTimer = 0f;
        darkenImage.color = new Color(0, 0, 0, 0);
    }

    private void StartFadeOut()
    {
        isFadingOut = true;
        fadeOutTimer = 0f;
    }

    private void ResetFadeOut()
    {
        isFadingOut = false;
        fadeOutTimer = 0f;
        darkenImage.color = new Color(0, 0, 0, 0);
        if (audioSource != null)
        {
            audioSource.volume = minVolume; // Убедитесь, что громкость аудиоисточника не опускается ниже минимальной громкости
        }
    }

    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && teleportDestination != null)
        {
            player.transform.position = teleportDestination.position;
        }
    }
}
