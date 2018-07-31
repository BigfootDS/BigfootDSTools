using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BigfootDS;

namespace BigfootDS
{

    public class BigfootEventInvoker : MonoBehaviour
    {
        [System.Serializable]
        public class SpecifiedEvent : UnityEvent { }

        public SpecifiedEvent eventAction = new SpecifiedEvent();

        public void StartEventAction()
        {
            eventAction.Invoke();
        }


    }
}