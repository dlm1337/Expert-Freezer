using ExpertFreezerAPI.Models;
using ExpertFreezerAPI.Repo;
using Microsoft.AspNetCore.Identity;

namespace ExpertFreezerAPI.Service
{
    public interface IExpertFreezerService
    {
        Task<ExpertFreezerProfileDTO> GetExpertFreezer(long id);
        Task<ExpertFreezerProfileDTO> CreateExpertFreezer(long userId, ExpertFreezerProfileDTO expertFreezerProfileDTO);
        Task<ExpertFreezerProfileDTO> PatchExpertFreezer(PatchProfileDTO patchProfileDTO);
        Task<UserDTO> Register(RegistrationDTO registrationDTO);
        Task<UserDTO> GetUser(string username);
        Task<UserDTO> Login(LoginDTO loginDTO);
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
                Password = registrationDTO.Password,
                Email = registrationDTO.Email
            };

            // Hash the password before saving
            user.Password = _passwordHasher.HashPassword(user, registrationDTO.Password);

            await _repository.AddUser(user);
            await _repository.SaveChangesAsync();

            // Create ExpertFreezer profile
            var expertFreezerProfileDTO = new ExpertFreezerProfileDTO
            {
                Username = registrationDTO.Username
            };

            await CreateExpertFreezer(user.Id, expertFreezerProfileDTO);

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return userDTO;
        }
        public async Task<UserDTO> Login(LoginDTO loginDTO)
        {
            var user = await _repository.FindUserByUsername(loginDTO.Username);

            if (user == null)
            {
                return null; // User not found
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDTO.Password);

            if (result != PasswordVerificationResult.Success)
            {
                return null; // Password verification failed
            }

            // Password verification successful, return user DTO
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
        public async Task<UserDTO> GetUser(string username)
        {
            var user = await _repository.FindUserByUsername(username);

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

                return ProfileToDTO(expertFreezerProfile);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in some other way
                throw new Exception("An error occurred while getting ExpertFreezer.", ex);
            }
        }

        public async Task<ExpertFreezerProfileDTO> CreateExpertFreezer(long userId, ExpertFreezerProfileDTO expertFreezerProfileDTO)
        {
            var expertFreezerProfile = new ExpertFreezerProfile
            {
                Id = userId,  // Use the user ID directly
                Username = expertFreezerProfileDTO.Username,
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

            return ProfileToDTO(createdExpertFreezer);
        }


        public async Task<ExpertFreezerProfileDTO> PatchExpertFreezer(PatchProfileDTO patchProfileDTO)
        {

            var patchedExpertFreezer = await _repository.PatchExpertFreezer(patchProfileDTO);

            return ProfileToDTO(patchedExpertFreezer);

        }

        private static ExpertFreezerProfileDTO ProfileToDTO(ExpertFreezerProfile expertFreezerProfile) =>
           new ExpertFreezerProfileDTO
           {
               Id = expertFreezerProfile.Id,
               Username = expertFreezerProfile.Username,
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