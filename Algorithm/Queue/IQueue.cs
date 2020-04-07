namespace Algorithm.Queue
{
    public interface IQueue<E>
    {
        int Size { get; }
        E GetFront();

        void Enqueue(E e);

        E Dequeue();

        bool IsEmpty { get; }
    }
}