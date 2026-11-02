using AuthTemplate.Domain.Entities;

namespace AuthTemplate.Application.Auth.Interfaces;

public interface ITokenProvider
{
    string Generate(User user);
}
