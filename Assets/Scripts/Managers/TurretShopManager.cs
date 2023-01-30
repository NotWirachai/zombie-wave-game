using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopManager : MonoBehaviour
{

    [SerializeField] private GameObject turretCardPrefab;
    [SerializeField] private Transform turretPanelContainer;

    [Header("Turret Setting")]
    [SerializeField] private TurretSetting[] turrets;

    private Node _currentNodeSelected;

    void Start()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            CreateTurretCard(turrets[i]);
        }
    }

    private void CreateTurretCard(TurretSetting turretSetting) 
    {
        GameObject newInstance = Instantiate(turretCardPrefab, turretPanelContainer.position, Quaternion.identity);
        newInstance.transform.SetParent(turretPanelContainer);
        newInstance.transform.localScale = Vector3.one;

        TurretCard cardTurret = newInstance.GetComponent<TurretCard>();
        cardTurret.SetupTurretButton(turretSetting);

    }

    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
    }

    private void PlaceTurret(TurretSetting turretLoaded) 
    {
        if (_currentNodeSelected != null)
        {
            GameObject turretInstance = Instantiate(turretLoaded.TurretPrefab);
            turretInstance.transform.localPosition = _currentNodeSelected.transform.position;
            turretInstance.transform.parent = _currentNodeSelected.transform;

            Turret turretPlaced = turretInstance.GetComponent<Turret>();
            _currentNodeSelected.SetTurret(turretPlaced);
        }
    }

    private void TurretSold() 
    {
        _currentNodeSelected = null;
    }

    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
        Node.OnTuuretSold += TurretSold;
        TurretCard.OnPlaceTurret += PlaceTurret;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
        Node.OnTuuretSold -= TurretSold;
        TurretCard.OnPlaceTurret -= PlaceTurret;
    }
}
