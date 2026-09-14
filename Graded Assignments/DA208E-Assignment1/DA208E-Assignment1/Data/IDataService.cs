// Sixten Peterson (AQ9300) 2026-09-14
namespace DA208E_Assignment1.Data;

/// <summary>
/// Simple generic interface for data services, this was built in case we ever extend with more services or want to make a database service in addition to the in-memory alternative used for assignment 1.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDataService<T>
{
    /// <summary>
    /// Get all data stored by the service
    /// </summary>
    /// <returns>A list of the data from the service</returns>
    public List<T> GetAll();
    
    /// <summary>
    /// Attempts to get an instance of data from the service.
    /// </summary>
    /// <param name="id">The id of the instance</param>
    /// <returns>The data</returns>
    public T? Get(int id);
    
    /// <summary>
    /// Attempts to add data to the service.
    /// </summary>
    /// <param name="type">The data to add</param>
    public void Add(T type);
    
    /// <summary>
    /// Attempts to update the specified data stored in the service.
    /// </summary>
    /// <param name="type">The new data</param>
    /// <returns>Bool representing if the data updated successfully or not</returns>
    public bool Update(T type);
    
    /// <summary>
    /// Attempts to delete the specified data stored in the service.
    /// </summary>
    /// <param name="id">The id of the data to delete</param>
    /// <returns>Bool representing if the data was deleted successfully or not</returns>
    public bool Delete(int id);
}