namespace GoEdu.Repositories
{
    public interface ICRUD<T>
    {
        public void Insert(T Entity);
        public List<T> GetAll();
        public T GetByID(int id);
        public void Update(int id, T Entity);
        public void Delete(int id);
        public void SaveData();
    }
}
