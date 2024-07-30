using Dapper;
using LearnDapper.Model;
using LearnDapper.Model.Data;
using System.Data;

namespace LearnDapper.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperDBContext context;
        public EmployeeRepo(DapperDBContext context) {
            this.context = context;
        }

        public async Task<string> Create(Employee employee)
        {
            string response = string.Empty;
            string query = "Insert into tbl_employee(name,email,phone,designation) values (@name,@email,@phone,@designation)";
            var parameters = new DynamicParameters();
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }

        public async Task<List<Employee>> GetAll()
        {
            string query = "Select * From tbl_employee";
            using(var connectin=this.context.CreateConnection())
            {
                var emplist= await connectin.QueryAsync<Employee>(query);
                return emplist.ToList();
            }
        }

        public async Task<List<Employee>> GetAllbyrole(string role)
        {
            //string query = "exec sp_getemployeebyrole @role";
            //using (var connectin = this.context.CreateConnection())
            //{
            //    var emplist = await connectin.QueryAsync<Employee>(query,new {role});
            //    return emplist.ToList();
            //}

            string query = "sp_getemployeebyrole";
            using (var connectin = this.context.CreateConnection())
            {
                var emplist = await connectin.QueryAsync<Employee>(query, new { role },commandType:CommandType.StoredProcedure);
                return emplist.ToList();
            }

        }

        public async Task<Employee> Getbycode(int code)
        {
            string query = "Select * From tbl_employee where code=@code";
            using (var connectin = this.context.CreateConnection())
            {
                var emplist = await connectin.QueryFirstOrDefaultAsync<Employee>(query,new {code});
                return emplist;
            }
        }

        public async Task<string> Remove(int code)
        {
            string response = string.Empty;
            string query = "Delete From tbl_employee where code=@code";
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, new { code });
                response = "pass";
            }
            return response;
        }

        public async Task<string> Update(Employee employee, int code)
        {
            string response = string.Empty;
            string query = "update tbl_employee set name=@name,email=@email,phone=@phone,designation=@designation where code=@code";
            var parameters = new DynamicParameters();
            parameters.Add("code", code, DbType.Int32);
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }
    }
}
