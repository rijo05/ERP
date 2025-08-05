using ERP.Application.DTOs.User;
using ERP.Application.Services;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using ERP.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace ERP.Application.Mappers
{
    public class UserMapper
    {
        private readonly HateoasLinkService _hateoasLinkService;

        public UserMapper(HateoasLinkService hateoasLinkService)
        {
            _hateoasLinkService = hateoasLinkService;
        }

        public UserResponseDTO ToUserResponseDTO(User user)
        {
            return new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email.Value,
                Role = user.Role.ToString(),
                CanDelete = CanDelete(user.Role),
                CanEditOwnProfile = CanEditOwnProfile(user.Role),
                IsActive = user.IsActive,
                Links = GenerateLinks(user)
            };
        }

        public List<UserResponseDTO> ToUserResponseDTOList(List<User> users)
        {
            return users.Select(x => ToUserResponseDTO(x)).ToList();
        }


        private bool CanDelete(Role role)
        {
            return role.roleName == RoleType.Admin;
        }

        private bool CanEditOwnProfile(Role role)
        {
            return true;
        }

        private Dictionary<string, object> GenerateLinks(User user)
        {
            return _hateoasLinkService.GenerateLinks(
                        user.Id,
                        "Users",
                        "GetById",
                        "Update",
                        "Delete"
            );
        }
    }
}
