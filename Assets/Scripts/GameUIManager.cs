using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;

    public Slider playerHealthSlider;
    public Slider playerDamageSlider;
    public Text ammoText;
    public Slider zombieHealthSlider;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void UpdatePlayerHealth(int current, int max)
    {
        if (playerHealthSlider)
            playerHealthSlider.value = (float)current / max;
    }

    public void UpdatePlayerDamage(int damage)
    {
        if (playerDamageSlider)
            playerDamageSlider.value = damage;
    }

    public void UpdateAmmo(int current, int max)
    {
        if (ammoText)
            ammoText.text = current + " / " + max;
    }

    public void UpdateZombieHealth(int current, int max)
    {
        if (zombieHealthSlider)
            zombieHealthSlider.value = (float)current / max;
    }
}
