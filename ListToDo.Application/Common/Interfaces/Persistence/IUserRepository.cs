using ListTodo.Domain.Entities;

namespace ListToDo.Application.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}