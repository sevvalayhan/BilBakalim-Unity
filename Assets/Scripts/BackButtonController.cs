using UnityEngine;
public class BackButtonController : MonoBehaviour
{
    [SerializeField] private PanelController panelController;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            if (panelController.QuestionPanel.activeSelf)
            {
                if (!panelController.BackToCategoryPanel.activeSelf)
                {
                    panelController.BackToCategoryPanel.SetActive(true);
                }
                else
                {
                    panelController.BackToCategoryPanel.SetActive(false);
                }
            }
            else if (panelController.CategoryPanel.activeSelf)
            {
                panelController.SetPanelActive(panelController.MainMenuPanel);
            }
            else
            {
                if (!panelController.AppQuitPanel.activeSelf)
                {
                    panelController.AppQuitPanel.SetActive(true);
                }
                else
                {
                    panelController.AppQuitPanel.SetActive(false);
                }
            }

        }
    }
}
