using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellAreaEnter : MonoBehaviour
{
    private PlayerInfo _playerInfo;

    private void Start()
    {
        _playerInfo =  GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Sellable")) return;
        Destroy(other.gameObject);
        _playerInfo.AddPoints(other.gameObject.GetComponentInChildren<ItemInfo>().Value);
    }
}
