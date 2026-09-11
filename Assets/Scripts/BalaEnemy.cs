using UnityEngine;

public class BalaEnemy : Daño
{
    [SerializeField] private int velocidad;
    [SerializeField] private GameObject gameOverPanel;

    void Update()
    {
        if (!gameOverPanel.activeSelf)
        {
            transform.Translate(Time.deltaTime * velocidad * Vector2.right);
        }
    }
}
