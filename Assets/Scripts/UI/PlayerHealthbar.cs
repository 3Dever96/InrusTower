using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private CharacterStats player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>().gameObject.GetComponent<CharacterStats>();

        if (player != null)
        {
            player.OnSpawn.AddListener(OnSpawn);
            player.OnHit.AddListener(OnHit);
        }
    }

    public void OnSpawn()
    {
        slider.value = Mathf.CeilToInt(player.CurrentHp);
    }

    public void OnHit()
    {
        slider.value = Mathf.CeilToInt(player.CurrentHp);
    }
}
