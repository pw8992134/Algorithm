namespace Algorithm.Stack
{
    public interface IStack<E>
    {
        bool IsEmpty { get; }

        int Size { get; }

        void Push(E e);

        E Pop();

        E Peek();
    }
}