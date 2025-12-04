using System;
using System.Collections.Generic;
using MunicipalService_P3.Models;   // ✅ Adjusted to your project namespace

namespace MunicipalService_P3.DataStructures
{
    /// <summary>
    /// MinHeap implementation for managing ServiceRequests by priority.
    /// Lower priority value = higher urgency.
    /// </summary>
    public class MinHeap
    {
        private readonly List<(ServiceRequest Item, int Priority)> data = new List<(ServiceRequest, int)>();

        public int Count => data.Count;

        /// <summary>
        /// Insert a new ServiceRequest into the heap.
        /// </summary>
        public void Insert(ServiceRequest item)
        {
            data.Add((item, item.Priority));
            SiftUp(data.Count - 1);
        }

        /// <summary>
        /// Extract the ServiceRequest with the minimum priority value.
        /// </summary>
        public ServiceRequest ExtractMin()
        {
            if (data.Count == 0) return null;

            var root = data[0].Item;
            data[0] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);

            if (data.Count > 0)
                SiftDown(0);

            return root;
        }

        private void SiftUp(int i)
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (data[i].Priority >= data[parent].Priority) break;

                Swap(i, parent);
                i = parent;
            }
        }

        private void SiftDown(int i)
        {
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int smallest = i;

                if (left < data.Count && data[left].Priority < data[smallest].Priority)
                    smallest = left;

                if (right < data.Count && data[right].Priority < data[smallest].Priority)
                    smallest = right;

                if (smallest == i) break;

                Swap(i, smallest);
                i = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            var tmp = data[a];
            data[a] = data[b];
            data[b] = tmp;
        }

        /// <summary>
        /// Return all ServiceRequests currently in the heap (unsorted).
        /// </summary>
        public IEnumerable<ServiceRequest> ToList() => data.ConvertAll(d => d.Item);
    }
}