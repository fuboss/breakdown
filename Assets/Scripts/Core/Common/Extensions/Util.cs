using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityCore.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UnityCore.Tools
{
    public static class Util
    {
        public static T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            T comp = go.GetComponent<T>();

            if (comp == null)
            {
                Transform t = go.transform.parent;

                while (t != null && comp == null)
                {
                    comp = t.gameObject.GetComponent<T>();
                    t = t.parent;
                }
            }
            return comp;
        }

        /// <summary>
        /// Finds the specified component on the game object or one of its parents.
        /// </summary>
        public static T FindInParents<T>(Transform trans) where T : Component
        {
            if (trans == null) return null;
            return trans.GetComponentInParent<T>();

        }

        /// <summary>
        /// Add a new child game object.
        /// </summary>
        public static GameObject AddChild(GameObject parent, bool undo)
        {
            GameObject go = new GameObject();
#if UNITY_EDITOR
            if (undo) UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
            if (parent != null)
            {
                Transform t = go.transform;
                t.SetParent(parent.transform, false);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.layer = parent.layer;
            }
            return go;
        }

        /// <summary>
        /// Instantiate an object and add it to the specified parent.
        /// </summary>

        public static GameObject AddChild(GameObject parent, GameObject prefab)
        {
            GameObject go = Object.Instantiate(prefab);
            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.SetParent(parent.transform,true);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.layer = prefab.layer;
            }
            return go;
        }

        /// <summary>
        /// Instantiate an object and add it to the specified parent.
        /// </summary>

        public static T AddChild<T>(GameObject parent, T prefab) where T: MonoBehaviour
        {
            T go = Object.Instantiate(prefab);
            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.SetParent(parent.transform, true);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.gameObject.layer = prefab.gameObject.layer;
                go.name = prefab.name;
            }
            return go;
        }

        /// <summary>
        /// Ensure that the angle is within -180 to 180 range.
        /// </summary>

        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerStepThrough]
        static public float WrapAngle(float angle)
        {
            while (angle > 180f) angle -= 360f;
            while (angle < -180f) angle += 360f;
            return angle;
        }

        public static IEnumerator UnscaledWait(float seconds)
        {
            var finishTime = Time.unscaledTime + seconds;
            while (Time.unscaledTime < finishTime)
                yield return null;
        }

        public static bool CustomApproximately(float val1, float val2, float epsilon = 0.001f)
        {
            return Mathf.Abs(Mathf.Abs(val1) - Mathf.Abs(val2)) < epsilon;
        }

        public static T GetCopyOf<T>(this Component comp, T other) where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
                }
            }
            FieldInfo[] finfos = type.GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp as T;
        }

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
        public static RectTransform AsRectTransform(this Transform transform)
        {
            return (RectTransform) transform;
        }

        public static bool IsSelected(this Selectable target)
        {
            return EventSystem.current.currentSelectedGameObject != null && (target != null && EventSystem.current.currentSelectedGameObject.Equals(target.gameObject));
        }

        public static void DelayedAction(this MonoBehaviour target, float seconds, Action action)
        {
            if (target.gameObject.activeInHierarchy)
                target.StartCoroutine(DoDelayedAction(seconds, action));
        }
        
        public static void DelayedAction(this MonoBehaviour target, int frames, Action action)
        {
            if (target.gameObject.activeInHierarchy && frames > 0)
                target.StartCoroutine(DoDelayedAction(frames, action));
            else
                action.SafeRaise();
        }

        private static IEnumerator DoDelayedAction(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action.SafeRaise();
        }
        
        private static IEnumerator DoDelayedAction(int frames, Action action)
        {
            for (int i = 0; i < frames; i++)
                yield return null;
            
            action.SafeRaise();
        }
        /// <summary>
        /// Copy transform values to the target
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void Copy(this Transform from, Transform to)
        {
            to.SetParent(from.parent);
            to.localPosition = from.localPosition;
            to.localRotation = from.localRotation;
            to.localScale = from.localScale;
        }
    }
}