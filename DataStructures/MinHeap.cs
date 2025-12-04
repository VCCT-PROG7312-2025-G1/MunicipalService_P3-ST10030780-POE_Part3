// DataStructures/MinHeap.cs
using System;
using System.Collections.Generic;
using MunicipalService_P3.Models;

namespace MunicipalService_P3.DataStructures
{
    public class MinHeap
    {
        private readonly List<(ServiceRequest Item, int Priority)> data = new();

        public int Count => data.Count;

        public void Insert(ServiceRequest item)
        {
            data.Add((item, item.Priority));
            SiftUp(data.Count - 1);
        }

        public ServiceRequest ExtractMin()
        {
            if (data.Count == 0) return null;
            var root = data[0].Item;
            data[0] = data[^1];
            data.RemoveAt(data.Count - 1);
            if (data.Count > 0) SiftDown(0);
            return root;
        }

        public ServiceRequest Peek() => data.Count == 0 ? null : data[0].Item;

        private void SiftUp(int i)
        {
            while (i > 0)
            {
                int p = (i - 1) / 2;
                if (data[i].Priority >= data[p].Priority) break;
                Swap(i, p); i = p;
            }
        }

        private void SiftDown(int i)
        {
            while (true)
            {
                int l = 2 * i + 1, r = 2 * i + 2, smallest = i;
                if (l < data.Count && data[l].Priority < data[smallest].Priority) smallest = l;
                if (r < data.Count && data[r].Priority < data[smallest].Priority) smallest = r;
                if (smallest == i) break;
                Swap(i, smallest); i = smallest;
            }
        }

        private void Swap(int a, int b)
        {
            var tmp = data[a]; data[a] = data[b]; data[b] = tmp;
        }

        public IEnumerable<ServiceRequest> ToList() => data.ConvertAll(d => d.Item);
    }
}