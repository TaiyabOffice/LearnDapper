using LearnDapper.Model;

namespace LearnDapper.Repo
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAll();

        Task<List<Employee>> GetAllbyrole(string role);
        Task<Employee> Getbycode(int code);
        Task<string> Create(Employee employee);
        Task<string> Update(Employee employee,int code);
        Task<string> Remove(int code);
    }
}
