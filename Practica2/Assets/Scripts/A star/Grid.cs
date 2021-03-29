using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ESTRUCTURA DE NODOS
/// </summary>
public class Grid<T>
{
    // Constants
    private const int DEFAULT_SIZE = 4;
    // Fields
    private T[] _data = new T[DEFAULT_SIZE];
    private int count_ = 0;
    private int capacity_ = DEFAULT_SIZE;
    private bool sorted_;

    public int Count
    {
        get { return count_; }
    }

    public int Capacity
    {
        get { return capacity_; }

        set
        {
            int previousCapacity = _capacity;
            capacity_ = Math.Max(value, _count);
            if (capacity_ != previousCapacity)
            {
                T[] temp = new T[capacity_];
                Array.Copy(_data, temp, count_);
                _data = temp;
            }
        }

    }

    public Grid()
    {

    }
    public Grid(T[] data, int count)
    {

    }
    public T Peek()
    {
        return _data[0];
    }

}
