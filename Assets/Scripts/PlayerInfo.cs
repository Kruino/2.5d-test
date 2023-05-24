using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public string playerName = "Player";  // Name of the player
    public int damageAmount { get; private set; } = 1; // Amount of damage the player does
    private int points { get; set; } = 0;
    private int multiplier = 1;
    
    public delegate void ValueChangedDelegate(int newValue);
    public event ValueChangedDelegate OnPointsChanged;
    
    public void AddPoints(int amount)
    {
        points += (amount * multiplier);
        OnPointsChanged?.Invoke(points);
    }
}