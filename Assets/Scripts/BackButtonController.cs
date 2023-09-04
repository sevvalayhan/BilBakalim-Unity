using UnityEngine;
public class BackButtonController : MonoBehaviour
{
    [SerializeField] private PanelController panelController;

    void Update()
    {
        BackToOtherPanel();
    }
    public void BackToOtherPanel()
    {
        switch (Input.GetKeyDown(KeyCode.Escape))
        {
            case true when panelController.QuestionPanel.activeSelf:
                if (!panelController.BackToCategoryPanel.activeSelf)
                {
                    panelController.BackToCategoryPanel.SetActive(true);
                }
                else
                {
                    panelController.BackToCategoryPanel.SetActive(false);
                }
                break;

            case true when panelController.CategoryPanel.activeSelf:
                panelController.SetPanelActive(panelController.MainMenuPanel);
                break;

            case true when panelController.MainMenuPanel.activeSelf || panelController.ResultPanel.activeSelf:
                if (!panelController.AppQuitPanel.activeSelf)
                {
                    panelController.AppQuitPanel.SetActive(true);
                }
                else
                {
                    panelController.AppQuitPanel.SetActive(false);
                }
                break;
        }
    }
    void Method()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
