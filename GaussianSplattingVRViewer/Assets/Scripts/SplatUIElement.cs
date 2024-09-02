using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplatUIElement : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NameText;
    public string UID;
    public Button ActionButton;
    private void OnEnable()
    {
        ActionButton.onClick.AddListener(ActionButton_Action);
    }
    private void OnDisable()
    {
        ActionButton.onClick.RemoveListener(ActionButton_Action);

    }

    void ActionButton_Action()
    {
        SplatController.Instance.EnableSplat(UID);

        SplatSceneManager.instance.DeactivateCarousalMenu();
    }
    public void PopulateUI(string Name, string Id)
    {
        NameText.text = Name;
        UID = Id;
    }
}
