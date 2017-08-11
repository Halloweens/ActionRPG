using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class NewGame : MonoBehaviour {

    public GameObject panelNew;
    public GameObject panelContinue;
    
    public void ClickForPanel()
    {
        panelContinue.SetActive(false);
        panelNew.SetActive(true);
    }

    public void ClickForNewGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void ClickForClosePanel()
    {
        panelContinue.SetActive(false);
        panelNew.SetActive(false);
    }
}
