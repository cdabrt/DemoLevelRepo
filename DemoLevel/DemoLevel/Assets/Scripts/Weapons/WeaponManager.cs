using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Weapons
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> heldItems;
        [SerializeField] private float pickupDistance = 3;
        private Transform _playerCamera;
        private bool _inPickupRange;
        private GameObject _toBePickedUp;
        private int _currentItem;
        private GameObject _pickupText;

        private void Start()
        {
            var parent = transform.parent;
            _playerCamera = parent.transform;
            _pickupText = parent.Find("Canvas").GetChild(0).gameObject;
            
            foreach (var instantiatedItem in heldItems.Select(weapon => Instantiate(weapon, transform)))
            {
                instantiatedItem.transform.parent = transform;
                instantiatedItem.transform.position = transform.position;
            }

            if (heldItems.Count == 0) return;
            _currentItem = 0;
            transform.GetChild(_currentItem).transform.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_pickupText.activeSelf != _inPickupRange)
            {
                _pickupText.SetActive(_inPickupRange);
            }
        }

        private void FixedUpdate()
        {
            if (Physics.Raycast(_playerCamera.position, _playerCamera.forward, out var hit, pickupDistance))
            {
                //Add tag pickup if ever added
                if (hit.transform.CompareTag("Weapon"))
                {
                    _inPickupRange = true;
                    _toBePickedUp = hit.transform.gameObject;
                }
            } else
            {
                _inPickupRange = false;
            }
        }

        private void OnWeaponSelect(InputValue scrollValue)
        {
            if (heldItems.Count == 0) return;
            transform.GetChild(_currentItem).transform.gameObject.SetActive(false);
            
            if (scrollValue.Get<float>() > 0f)
            {
                if (_currentItem < heldItems.Count - 1)
                {
                    _currentItem++;
                }
                else
                {
                    _currentItem = 0;
                }
            }
            //For some reason scrollValue goes from a set value to 0.
            //0 is a limbo state, so everything above or below 0 we care about.
            else if (scrollValue.Get<float>() < 0f)
            {
                if (_currentItem > 0)
                {
                    _currentItem--;
                }
                else
                {
                    _currentItem = heldItems.Count - 1;
                } 
            }
            
            transform.GetChild(_currentItem).transform.gameObject.SetActive(true);
        }

        private void OnFire()
        {
            if (heldItems.Count == 0) return;
            var heldItem = transform.GetChild(_currentItem).transform.gameObject;
            
            //Item in hand and is a weapon:
            if (heldItem != null && heldItem.CompareTag("Weapon"))
            {
                var weapon = transform.GetChild(_currentItem).GetComponent<Weapon>();
                weapon.Attack();
            }
        }

        private void OnPickup()
        {
            if (!_inPickupRange) return;
            if (heldItems.Count != 0)
            {
                transform.GetChild(_currentItem).transform.gameObject.SetActive(false);
            }
                
            //Pick up and use new weapon
            AddItem();
            _currentItem = heldItems.Count - 1;
            transform.GetChild(_currentItem).transform.gameObject.SetActive(true);
            _inPickupRange = false;
        }

        private void AddItem()
        {
            var instantiatedItem = Instantiate(_toBePickedUp, transform);
            instantiatedItem.transform.parent = transform;
            instantiatedItem.transform.position = transform.position;
            instantiatedItem.SetActive(false);
            heldItems.Add(instantiatedItem);
            Destroy(_toBePickedUp);
            _toBePickedUp = null;
        }
    }
}
