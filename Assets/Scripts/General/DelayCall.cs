using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class DelayCall : MonoBehaviour
    {
        public static Coroutine Call(MonoBehaviour mono, System.Action action, float delay)
        {
            return mono.StartCoroutine(CallCo(action, delay));
        }

        private static IEnumerator CallCo(System.Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }
    }
}