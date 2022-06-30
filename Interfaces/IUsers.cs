using KaseyWebApi.DataModel;

namespace KaseyWebApi.Interfaces;

public interface IUsers
{
    public List<UserInfo> GetUsers();

    public UserInfo GetUserById(int id);

    public void CreateUser(UserInfo user);

    public void UpsertUser(UserInfo user);

    public UserInfo DeleteEmployee(int id);

    public bool CheckUser(int id);
}