using System.Data.Common;
using KaseyWebApi.Context;
using KaseyWebApi.DataModel;
using KaseyWebApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KaseyWebApi.Repository;

public class EmployeeRepository : IEmployees
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
    }

    public List<Employee> GetEmployees()
    {
        try
        {
            return _dbContext.Employees.ToList();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Employee GetEmployeeDetails(int id)
    {
        try
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee != null)
            {
                return employee;
            }

            throw new ArgumentNullException();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void AddEmployee(Employee employee)
    {
        try
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void UpdateEmployee(Employee employee)
    {
        try
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public Employee DeleteEmployee(int id)
    {
        try
        {
            var employee =
                _dbContext
                    .Employees
                    .First(e => e.EmployeeId == id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
                return employee;
            }

            throw new ArgumentNullException();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public bool CheckEmployee(int id)
    {
        return _dbContext.Employees.Any(e => e.EmployeeId == id);
    }
}