namespace MunicipalService_P3.Models.DataStructures
{
    public class MaxHeapQueue<T> where T : IComparable<T>
    {
        private readonly List<T> _heap = new();

        public int Count => _heap.Count;

        public void Enqueue(T item)
        {
            _heap.Add(item);
            HeapifyUp(_heap.Count - 1);
        }

        public T? Peek() => _heap.Count > 0 ? _heap[0] : default;

        public T? Dequeue()
        {
            if (_heap.Count == 0) return default;
            var root = _heap[0];
            _heap[0] = _heap[^1];
            _heap.RemoveAt(_heap.Count - 1);
            HeapifyDown(0);
            return root;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_heap[index].CompareTo(_heap[parent]) <= 0) break;
                (_heap[index], _heap[parent]) = (_heap[parent], _heap[index]);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int last = _heap.Count - 1;
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = index;

                if (left <= last && _heap[left].CompareTo(_heap[largest]) > 0) largest = left;
                if (right <= last && _heap[right].CompareTo(_heap[largest]) > 0) largest = right;

                if (largest == index) break;
                (_heap[index], _heap[largest]) = (_heap[largest], _heap[index]);
                index = largest;
            }
        }
    }
}