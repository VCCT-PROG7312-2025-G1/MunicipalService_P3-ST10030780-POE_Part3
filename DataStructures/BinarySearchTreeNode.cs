namespace MunicipalService_P3.Models.DataStructures
{
    public class BinarySearchTreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public BinarySearchTreeNode<T>? Left { get; set; }
        public BinarySearchTreeNode<T>? Right { get; set; }

        public BinarySearchTreeNode(T value) => Value = value;

        public void Insert(T newValue)
        {
            if (newValue.CompareTo(Value) < 0)
            {
                if (Left == null) Left = new BinarySearchTreeNode<T>(newValue);
                else Left.Insert(newValue);
            }
            else
            {
                if (Right == null) Right = new BinarySearchTreeNode<T>(newValue);
                else Right.Insert(newValue);
            }
        }

        public bool Contains(T searchValue)
        {
            if (searchValue.CompareTo(Value) == 0) return true;
            if (searchValue.CompareTo(Value) < 0) return Left?.Contains(searchValue) ?? false;
            return Right?.Contains(searchValue) ?? false;
        }
    }
}