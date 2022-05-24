using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Xezebo.DI
{
    [RequireComponent(typeof(ZenjectBinding))]
    public class AutoBinder : MonoBehaviour
    {
        [SerializeField] BindingIdentifiers _identifierEnum;
        [SerializeField] Component _component;

        private void OnValidate()
        {
            ZenjectBinding zb = GetComponent<ZenjectBinding>();
            zb.Identifier = _identifierEnum.ToString();
            if (_component != null)
            {
                zb.Components = new Component[1];
                zb.Components[0] = _component;
            }
        }

        
    }
}