using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    private Stack<GameObject> menuStack = new Stack<GameObject>();


    //si abro un menu, se cierra el anterior y se abre el nuevo
    public void OpenMenu(GameObject menu)
    {
        if (menuStack.Count > 0)
        {
            GameObject currentMenu = menuStack.Peek();
            currentMenu.SetActive(false);
        }
        menu.SetActive(true);
        menuStack.Push(menu);
    }


    //si cierro un menu, se cierra el actual y se abre el anterior
    public void CloseMenu()
    {
        if (menuStack.Count > 0)
        {
            GameObject currentMenu = menuStack.Pop();
            currentMenu.SetActive(false);
        }
        if (menuStack.Count > 0)
        {
            GameObject previousMenu = menuStack.Peek();
            previousMenu.SetActive(true);
        }
    }
}
