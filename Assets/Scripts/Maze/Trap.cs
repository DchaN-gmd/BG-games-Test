using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Material _targetColor;

    public bool _isTrapZone = false;

    void Start()
    {
        if(_isTrapZone)
        {
            gameObject.GetComponent<Renderer>().material.color = _targetColor.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player) && _isTrapZone)
        {
           if(player.GetComponent<Player>()._isProtected == false)
           {
                Destroy(other.gameObject);
           }
        }
    }

}
