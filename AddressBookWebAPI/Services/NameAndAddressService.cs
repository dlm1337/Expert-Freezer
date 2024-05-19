using AddressBookAPI.Models;
using AddressBookAPI.Repo;

namespace AddressBookAPI.Service
{
    public interface INameAndAddressService
    {
        Task<NameAndAddressDTO> GetNameAndAddress(long id);
        Task<NameAndAddressDTO> CreateNameAndAddress(NameAndAddressDTO NameAndAddressDTO);
        Task<NameAndAddressDTO> GetLatestNameAndAddress();
    }

    public class NameAndAddressService : INameAndAddressService
    {
        private readonly INameAndAddressRepository _repository;

        public NameAndAddressService(INameAndAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<NameAndAddressDTO> GetNameAndAddress(long id)
        {
            try
            {
                var NameAndAddress = await _repository.GetNameAndAddress(id);

                if (NameAndAddress == null)
                {
                    throw new Exception($"NameAndAddress with id {id} not found.");
                }

                return ItemToDTO(NameAndAddress);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in some other way
                throw new Exception("An error occurred while getting NameAndAddress.", ex);
            }
        }

        public async Task<NameAndAddressDTO> CreateNameAndAddress(NameAndAddressDTO nameAndAddressDTO)
        {
            var lastId = await _repository.GetLastId();
            var nextId = lastId + 1;

            var nameAndAddress = new NameAndAddress
            {
                Id = nextId,
                IsComplete = nameAndAddressDTO.IsComplete,
                Company = nameAndAddressDTO.Company,
                FirstName = nameAndAddressDTO.FirstName,
                LastName = nameAndAddressDTO.LastName,
                Address = nameAndAddressDTO.Address,
                Address2 = nameAndAddressDTO.Address2,
                City = nameAndAddressDTO.City,
                State = nameAndAddressDTO.State,
                PostalCode = nameAndAddressDTO.PostalCode
            };

            var createdNameAndAddress = await _repository.CreateNameAndAddress(nameAndAddress);

            return ItemToDTO(createdNameAndAddress);
        }

        public async Task<NameAndAddressDTO> GetLatestNameAndAddress()
        {
            try
            {
                var latestNameAndAddress = await _repository.GetLatestNameAndAddress();

                if (latestNameAndAddress == null)
                {
                    throw new Exception("No NameAndAddress found.");
                }

                return ItemToDTO(latestNameAndAddress);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it in some other way
                throw new Exception("An error occurred while getting the latest NameAndAddress.", ex);
            }
        }
        private static NameAndAddressDTO ItemToDTO(NameAndAddress NameAndAddress) =>
           new NameAndAddressDTO
           {
               IsComplete = NameAndAddress.IsComplete,
               Id = NameAndAddress.Id,
               Company = NameAndAddress.Company,
               FirstName = NameAndAddress.FirstName,
               LastName = NameAndAddress.LastName,
               Address = NameAndAddress.Address,
               Address2 = NameAndAddress.Address2,
               City = NameAndAddress.City,
               State = NameAndAddress.State,
               PostalCode = NameAndAddress.PostalCode
           };
    }
}