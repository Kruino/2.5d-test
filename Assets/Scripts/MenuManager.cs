using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameObject _player;
    public TMP_Text textField;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        // Subscribe to the event for value changes
        _player.GetComponentInChildren<PlayerInfo>().OnPointsChanged += UpdateValueText;
    }

    private void UpdateValueText(int newValue)
    {
        // Update the value in the UI Text component
        textField.text = "Points: " + newValue;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the MenuManager is destroyed
        if (_player != null)
        {
            _player.GetComponentInChildren<PlayerInfo>().OnPointsChanged -= UpdateValueText;
        }
    }
}