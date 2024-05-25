using ExpertFreezerAPI.Models;
using ExpertFreezerAPI.Repo;
using Microsoft.AspNetCore.Identity;

namespace ExpertFreezerAPI.Service
{
    public interface IExpertFreezerService
    {
        Task<ExpertFreezerProfileDTO> GetExpertFreezer(long id);
        Task<ExpertFreezerProfileDTO> CreateExpertFreezer(ExpertFreezerProfileDTO ExpertFreezerProfileDTO);
        Task<ExpertFreezerProfileDTO> GetLatestExpertFreezer();
        Task<UserDTO> Register(RegistrationDTO registrationDTO);
        Task<UserDTO> GetUser(long id);
    }

    public class ExpertFreezerService : IExpertFreezerService
    {
        private readonly IExpertFreezerRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ExpertFreezerService(IExpertFreezerRepository repository, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDTO> Register(RegistrationDTO registrationDTO)
        {
            if (await _repository.UserExists(registrationDTO.Username))
            {
                throw new Exception("Username is already taken");
            }

            var user = new User
            {
                Username = registrationDTO.Username,
                Email = registrationDTO.Email,
                Password = registrationDTO.Password
            };

            // Hash the password before saving
            user.Password = _passwordHasher.HashPassword(user, registrationDTO.Password);

            await _repository.AddUser(user);
            await _repository.SaveChangesAsync();

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return userDTO;
        }

        public async Task<UserDTO> GetUser(long id)
        {
            var user = await _repository.FindUserById(id);

            if (user == null)
            {
                return null;
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return userDTO;
        }

        public async Task<ExpertFreezerProfileDTO> GetExpertFreezer(long id)
        {
            try
            {
                var expertFreezerProfile = await _repository.GetExpertFreezer(id);

                if (expertFreezerProfile == null)
                {
                    throw new Exception($"ExpertFreezer with id {id} not found.");
                }

                return ItemToDTO(expertFreezerProfile);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in some other way
                throw new Exception("An error occurred while getting ExpertFreezer.", ex);
            }
        }

        public async Task<ExpertFreezerProfileDTO> CreateExpertFreezer(ExpertFreezerProfileDTO expertFreezerProfileDTO)
        {
            var lastId = await _repository.GetLastId();
            var nextId = lastId + 1;

            var expertFreezerProfile = new ExpertFreezerProfile
            {
                Id = nextId,
                CompanyName = expertFreezerProfileDTO.CompanyName,
                CompanyDescription = expertFreezerProfileDTO.CompanyDescription,
                Services = expertFreezerProfileDTO.Services,
                Address = expertFreezerProfileDTO.Address,
                Email = expertFreezerProfileDTO.Email,
                Pricing = expertFreezerProfileDTO.Pricing,
                ProfilePic = expertFreezerProfileDTO.ProfilePic,
                ExtraPics = expertFreezerProfileDTO.ExtraPics,
                ExtraPicsDesc = expertFreezerProfileDTO.ExtraPicsDesc
            };

            var createdExpertFreezer = await _repository.CreateExpertFreezer(expertFreezerProfile);

            return ItemToDTO(createdExpertFreezer);
        }

        public async Task<ExpertFreezerProfileDTO> GetLatestExpertFreezer()
        {
            try
            {
                var latestExpertFreezer = await _repository.GetLatestExpertFreezer();

                if (latestExpertFreezer == null)
                {
                    throw new Exception("No ExpertFreezer found.");
                }

                return ItemToDTO(latestExpertFreezer);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in some other way
                throw new Exception("An error occurred while getting the latest ExpertFreezer.", ex);
            }
        }
        private static ExpertFreezerProfileDTO ItemToDTO(ExpertFreezerProfile expertFreezerProfile) =>
           new ExpertFreezerProfileDTO
           {
               Id = expertFreezerProfile.Id,
               CompanyName = expertFreezerProfile.CompanyName,
               CompanyDescription = expertFreezerProfile.CompanyDescription,
               Services = expertFreezerProfile.Services,
               Address = expertFreezerProfile.Address,
               Email = expertFreezerProfile.Email,
               Pricing = expertFreezerProfile.Pricing,
               ProfilePic = expertFreezerProfile.ProfilePic,
               ExtraPics = expertFreezerProfile.ExtraPics,
               ExtraPicsDesc = expertFreezerProfile.ExtraPicsDesc
           };
    }
}