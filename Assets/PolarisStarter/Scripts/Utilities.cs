using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinwheel.PolarisStarter
{
    public static class Utilities
    {
        public static T[][] CreateJaggedArray<T>(int dimension1, int dimension2)
        {
            T[][] jaggedArray = new T[dimension1][];
            for (int i = 0; i < dimension1; ++i)
            {
                jaggedArray[i] = new T[dimension2];
            }
            return jaggedArray;
        }

        public static T[] To1dArray<T>(T[][] jaggedArray)
        {
            List<T> result = new List<T>();
            for (int z = 0; z < jaggedArray.Length; ++z)
            {
                for (int x = 0; x < jaggedArray[z].Length; ++x)
                {
                    result.Add(jaggedArray[z][x]);
                }
            }
            return result.ToArray();
        }

        public static void Fill<T>(T[] array, T value)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = value;
            }
        }

        public static bool IsInRange(float number, float minValue, float maxValue)
        {
            return number >= minValue && number <= maxValue;
        }

        public static bool IsInRangeExclusive(float number, float minValue, float maxValue)
        {
            return number > minValue && number < maxValue;
        }

        public static float GetFraction(float value, float floor, float ceil)
        {
            return (value - floor) / (ceil - floor);
        }

        public static void ClearChildren(Transform t)
        {
            int childCount = t.childCount;
            for (int i = childCount - 1; i >= 0; --i)
            {
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                {
                    GameObject.DestroyImmediate(t.GetChild(i).gameObject);
                }
                else
                {
                    GameObject.Destroy(t.GetChild(i).gameObject);
                }
#else
                GameObject.Destroy(t.GetChild(i).gameObject);
#endif
            }
        }
    }
}