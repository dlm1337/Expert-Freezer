using ExpertFreezerAPI.Models;
using ExpertFreezerAPI.Repo;

namespace ExpertFreezerAPI.Service
{
    public interface IExpertFreezerService
    {
        Task<ExpertFreezerProfileDTO> GetExpertFreezer(long id);
        Task<ExpertFreezerProfileDTO> CreateExpertFreezer(ExpertFreezerProfileDTO ExpertFreezerProfileDTO);
        Task<ExpertFreezerProfileDTO> GetLatestExpertFreezer();
    }

    public class ExpertFreezerService : IExpertFreezerService
    {
        private readonly IExpertFreezerRepository _repository;

        public ExpertFreezerService(IExpertFreezerRepository repository)
        {
            _repository = repository;
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
                IsComplete = expertFreezerProfileDTO.IsComplete,
                CompanyName = expertFreezerProfileDTO.CompanyName,
                UserName = expertFreezerProfileDTO.UserName,
                Password = expertFreezerProfileDTO.Password,
                ConfirmPassword = expertFreezerProfileDTO.ConfirmPassword,
                CompanyDescription = expertFreezerProfileDTO.CompanyDescription,
                Services = expertFreezerProfileDTO.Services,
                Address = expertFreezerProfileDTO.Address,
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
               IsComplete = expertFreezerProfile.IsComplete,
               CompanyName = expertFreezerProfile.CompanyName,
               UserName = expertFreezerProfile.UserName,
               CompanyDescription = expertFreezerProfile.CompanyDescription,
               Services = expertFreezerProfile.Services,
               Address = expertFreezerProfile.Address,
               Pricing = expertFreezerProfile.Pricing,
               ProfilePic = expertFreezerProfile.ProfilePic,
               ExtraPics = expertFreezerProfile.ExtraPics,
               ExtraPicsDesc = expertFreezerProfile.ExtraPicsDesc
           };
    }
}