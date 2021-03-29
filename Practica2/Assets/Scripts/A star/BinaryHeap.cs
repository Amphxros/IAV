using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryHeap 
{
    private Edge[] data;
    private double[] priorities;

    int count_, cap_;
    public BinaryHeap()
    {
        cap_ = 0;
        data = new Edge[cap_];

    }
    public BinaryHeap(int cap)
    {
        cap_ = cap;
        data = new Edge[cap_];

    }
    public int Count
    {
        get
        {
            return count_;
        }
    }

    public void Add(Edge elem, double priority)
    {
        if (count_ == data.Length)
            throw new System.Exception("Heap capacity exceeded");

        // Add the item to the heap in the end position of the array (i.e. as a leaf of the tree)
        int position = count_++;
        data[position] = elem;
        priorities[position] = priority;
        // Move it upward into position, if necessary
        MoveUp(position);

    }
    public Edge Remove() {
        count_--;
        return data[count_ + 1];

    }

    public Edge Remove(Edge e)
    {

        count_--;
        return data[count_ + 1];

    }
    void MoveUp(int position)
    {
        while ((position > 0) && (priorities[Parent(position)] > priorities[position]))
        {
            int original_parent_pos = Parent(position);
            Swap(position, original_parent_pos);
            position = original_parent_pos;
        }
    }
    
    void MoveDown(int position)
    {
        int lchild = LeftChild(position);
        int rchild = RightChild(position);
        int largest = 0;
        if ((lchild < count_) && (priorities[lchild] < priorities[position]))
        {
            largest = lchild;
        }
        else
        {
            largest = position;
        }
        if ((rchild < count_) && (priorities[rchild] < priorities[largest]))
        {
            largest = rchild;
        }
        if (largest != position)
        {
            Swap(position, largest);
            MoveDown(largest);
        }
    }

    void Swap(int position1, int position2)
    {
        Edge temp = data[position1];
        data[position1] = data[position2];
        data[position2] = temp;
     
        double temp2 = priorities[position1];
        priorities[position1] = priorities[position2];
        priorities[position2] = temp2;
    }

    /// Gives the position of a node's parent, the node's position in the queue.
    static int Parent(int position)
    {
        return (position - 1) / 2;
    }

    /// Returns the position of a node's left child, given the node's position.
    static int LeftChild(int position)
    {
        return 2 * position + 1;
    }

    /// Returns the position of a node's right child, given the node's position.
    static int RightChild(int position)
    {
        return 2 * position + 2;
    }

    /// Checks all entries in the heap to see if they satisfy the heap property.
    public void TestHeapValidity()
    {
        for (int i = 1; i < count_; i++)
            if (priorities[Parent(i)] > priorities[i])
                throw new System.Exception("Heap violates the Heap Property at position " + i);
    }
}
