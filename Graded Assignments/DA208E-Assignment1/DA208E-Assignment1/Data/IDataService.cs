namespace DA208E_Assignment1.Data;

public interface IDataService<T>
{
    public List<T> GetAll();
    
    public T? Get(int id);
    
    public void Add(T type);
    
    public bool Update(T type);
    
    public bool Delete(int id);
}