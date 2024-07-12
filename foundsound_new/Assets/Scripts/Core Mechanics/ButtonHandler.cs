using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public GameObject panelList; // Панель списка
    public GameObject panelInfo; // Панель информации
    public TextMeshProUGUI panelInfoText; // Текстовый компонент внутри панели информации

    private Button button;
    private TextMeshProUGUI buttonTextTMP;

    void Start()
    {
        button = GetComponent<Button>();
        buttonTextTMP = GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        string buttonText = buttonTextTMP.text;
        string panelInfoValue = GetPanelInfoText(buttonText);

        if (panelInfoValue != null)
        {
            panelList.SetActive(false);
            panelInfo.SetActive(true);
            panelInfoText.text = panelInfoValue;
        }
    }

    string GetPanelInfoText(string buttonText)
    {
        foreach (var data in FindObjectOfType<RecordManager>().objectsData)
        {
            if (data.word == buttonText)
            {
                return data.info; // Используем поле info из ObjectData
            }
        }
        return null;
    }
}
