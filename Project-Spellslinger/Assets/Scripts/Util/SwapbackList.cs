using System;
using UnityEngine;

public class SwapbackList<T>
{
    public T[] Elements;
    public int Count;
    public int Capacity;

    public SwapbackList(int capacity)
    {
        Elements = new T[capacity];
        Capacity = capacity;
        Count = 0;
    }

    public void Extend(bool downwards)
    {
        var newCapacity = Capacity * 2;
        var newArray = new T[newCapacity];

        if (downwards)
        {
            Array.Copy(Elements, 0, newArray, Capacity, Capacity);
        }
        else
        {
            Array.Copy(Elements, newArray, Capacity);
        }

        Elements = newArray;
        Capacity = newCapacity;
    }

    public void Add(T member)
    {
        if (Capacity <= Count)
        {
            Extend(downwards: false);
        }

        Elements[Count] = member;
        Count++;
    }

    public T Pop()
    {
        return Elements[--Count];
    }

    public T GetAndRemoveAt(int index)
    {
        var element = Elements[index];
        Elements[index] = Elements[--Count];
        return element;
    }

    public void Clear()
    {
        Count = 0;
    }

    public void DebugPrint()
    {
        Debug.Log($"Capacity: {Capacity}, Count: {Count}");
        for (int i = 0; i < Count; i++)
        {
            Debug.Log($"index: {i}, element: {Elements[i]}");
        }
    }
}