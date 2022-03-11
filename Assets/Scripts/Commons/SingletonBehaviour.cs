using System.Collections;
using UnityEngine;

namespace Commons
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static T Instance;
        protected virtual bool isUnique { get => false; }
        
        private void Awake()
        {
            if (isUnique && Instance!= null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = (T)this;


        }
    }
}