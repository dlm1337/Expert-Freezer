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

        public async Task<ExpertFreezerProfileDTO> CreateExpertFreezer(ExpertFreezerProfileDTO ExpertFreezerProfileDTO)
        {
            var lastId = await _repository.GetLastId();
            var nextId = lastId + 1;

            var expertFreezerProfile = new ExpertFreezerProfile
            {
                Id = nextId,
                IsComplete = ExpertFreezerProfileDTO.IsComplete,
                Company = ExpertFreezerProfileDTO.Company,
                FirstName = ExpertFreezerProfileDTO.FirstName,
                LastName = ExpertFreezerProfileDTO.LastName,
                Address = ExpertFreezerProfileDTO.Address,
                Address2 = ExpertFreezerProfileDTO.Address2,
                City = ExpertFreezerProfileDTO.City,
                State = ExpertFreezerProfileDTO.State,
                PostalCode = ExpertFreezerProfileDTO.PostalCode
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
               IsComplete = expertFreezerProfile.IsComplete,
               Id = expertFreezerProfile.Id,
               Company = expertFreezerProfile.Company,
               FirstName = expertFreezerProfile.FirstName,
               LastName = expertFreezerProfile.LastName,
               Address = expertFreezerProfile.Address,
               Address2 = expertFreezerProfile.Address2,
               City = expertFreezerProfile.City,
               State = expertFreezerProfile.State,
               PostalCode = expertFreezerProfile.PostalCode
           };
    }
}