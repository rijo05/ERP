using ERP.Application.DTOs.Product;
using ERP.Application.DTOs.User;
using ERP.Application.Interfaces;
using ERP.Application.Mappers;
using ERP.Application.Validators.Common;
using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using ERP.Domain.ValueObjects;
using ERP.Persistence.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Data;
using System.Net.Http;

namespace ERP.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUserDTO> _validatorCreate;
        private readonly IValidator<UpdateUserDTO> _validatorUpdate;
        private readonly UserMapper _Usermapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IValidator<CreateUserDTO> validatorCreate, IValidator<UpdateUserDTO> validatorUpdate, UserMapper userMapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
            _Usermapper = userMapper;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _Usermapper.ToUserResponseDTOList(users);
        }
        public async Task<List<UserResponseDTO>> GetFilteredUsersAsync(UserFilterDTO userFilter)
        {
            var users = await _userRepository.GetFilteredAsync(userFilter);

            return _Usermapper.ToUserResponseDTOList(users);
        }

        public async Task<UserResponseDTO?> GetUserByEmailAsync(Email email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user is null ? null : _Usermapper.ToUserResponseDTO(user);
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(Guid id)
        {
            var user =  await _userRepository.GetByIdAsync(id);
            return user is null ? null : _Usermapper.ToUserResponseDTO(user);
        }

        public async Task<List<UserResponseDTO>> GetUsersByNameAsync(string name)
        {
            var users =  await _userRepository.GetByNameAsync(name);
            return _Usermapper.ToUserResponseDTOList(users);
        }

        public async Task<UserResponseDTO> AddUserAsync(CreateUserDTO userDTO)
        {
            //Validar os dados
            var validationResult = await _validatorCreate.ValidateAsync(userDTO);
            if (!validationResult.IsValid) 
                throw new ValidationException(validationResult.Errors);


            //Verificar se o email já está a ser usado por outro user
            var existingUser = await _userRepository.GetByEmailAsync(new Email(userDTO.Email));
            if (existingUser is not null) 
                throw new Exception("User with this email already exists.");


            var user = new User(userDTO.Name, new Email(userDTO.Email), new Role(userDTO.Role), userDTO.Password);
            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return _Usermapper.ToUserResponseDTO(user);
        }

        public async Task<UserResponseDTO> UpdateUserAsync(Guid id, UpdateUserDTO updateUserDTO)
        {
            //Ver se o user realmente existe
            var user = await EnsureUserExists(id);


            //Ver se já existe um user com o email novo
            if (!string.IsNullOrWhiteSpace(updateUserDTO.Email))
            {
                var existingEmail = await _userRepository.GetByEmailAsync(new Email(updateUserDTO.Email));
                if (existingEmail is not null && existingEmail.Id != user.Id)
                    throw new Exception("Email already in use.");
            }

            //Validar DTO
            var validationResult = await _validatorUpdate.ValidateAsync(updateUserDTO);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);


            user.UpdateFromDTO(updateUserDTO);  
            await _unitOfWork.CommitAsync();
            return _Usermapper.ToUserResponseDTO(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            //Ver se o user existe 
            var user = await EnsureUserExists(id);

            await _userRepository.DeleteAsync(user);
            await _unitOfWork.CommitAsync();
        }

        private async Task<User?> EnsureUserExists(Guid id)
        {
            return await _userRepository.GetByIdAsync(id) ?? throw new Exception("User doesn't exist.");
        }
    }
}
