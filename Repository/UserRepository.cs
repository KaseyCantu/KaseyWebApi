using System.Data.Common;
using KaseyWebApi.Context;
using KaseyWebApi.DataModel;
using KaseyWebApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KaseyWebApi.Repository;

public class UserRepository : IUsers
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
    }

    public List<UserInfo> GetUsers()
    {
        try
        {
            return _dbContext.Users.ToList();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public UserInfo GetUserById(int id)
    {
        try
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                return user;
            }

            throw new ArgumentNullException();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void CreateUser(UserInfo user)
    {
        try
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public void UpsertUser(UserInfo user)
    {
        try
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public UserInfo DeleteEmployee(int id)
    {
        try
        {
            var user = _dbContext.Users.Find(id);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return user;
            }

            throw new ArgumentNullException();
        }
        catch (DbException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public bool CheckUser(int id)
    {
        throw new NotImplementedException();
    }
}