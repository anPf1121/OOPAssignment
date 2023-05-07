namespace Interface
{
    interface IFile<T>
    {
        int WriteToFile(List<T> t);
        void ReadFromFile(List<T> t);
    }
}