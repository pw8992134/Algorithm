namespace Algorithm.Map
{
    public interface IDictionary<K,V>
    {
        void Add(K k,V v);

        V Remove(K k);

        bool Contains(K k);

        int Size { get; }

        bool IsEmpty { get; }

        V Get(K k);

        void Set(K k,V v);
    }
}