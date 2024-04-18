﻿using UnityEngine;


namespace SIS.ShopInventory
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField]
        GameObject itemTypeTabButtonContainer;
        public GameObject ItemTypeTabButtonContainer => itemTypeTabButtonContainer;

        [SerializeField]
        GameObject tabContainer;
        public GameObject TabContainer => tabContainer;
    }
}