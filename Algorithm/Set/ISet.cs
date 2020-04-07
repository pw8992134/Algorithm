namespace Algorithm.Set
{
    public interface ISet<E>
    {
        void Add(E e);

        void Remove(E e);

        bool Contains(E e);

        int Size { get; }

        bool IsEmpty { get; }
    }
}