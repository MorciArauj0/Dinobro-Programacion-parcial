using UnityEngine;

public class ContainerHearts : MonoBehaviour
{
    [SerializeField] private UIHeart[] corazones;

    [SerializeField] private Player player;

   private void Start()
    {
        player.jugadorTomoDaño += activarCorazones;
        player.jugadorSeCuro += activarCorazones;

        activarCorazones(player.getVidaActual());
    }
    

    void OnDisable()
    {
        player.jugadorTomoDaño -= activarCorazones;
        player.jugadorSeCuro -= activarCorazones;
    }


    private void activarCorazones(int vidaActual)
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vidaActual)
            {
                corazones[i].activarCorazon();
            }
            else
            {
                corazones[i].desactivarCorazon();
            }   
        }
    }

    public void actualizarCorazones(int vidaActual)
    {
        activarCorazones(vidaActual);
    }
}
