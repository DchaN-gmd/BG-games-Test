using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    [SerializeField] private Material _targetColor;

    public bool _isTrapZone = false;

    public UnityEvent playerDied;

    private void Start()
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
                //playerDied?.Invoke();
           }
        }
    }

}
