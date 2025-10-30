using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ExpertFreezerAPI.Data;
using ExpertFreezerAPI.Models;

namespace ExpertFreezerAPI.Repo
{
    public interface IExpertFreezerRepository
    {
        Task<ExpertFreezerProfile> GetExpertFreezer(long id);
        Task<ExpertFreezerProfile> CreateExpertFreezer(ExpertFreezerProfile expertFreezerProfile);
        Task<ExpertFreezerProfile> PatchExpertFreezer(PatchProfileDTO patchProfileDTO);
        Task<long> GetLastProfileID();
        Task<bool> UserExists(string username);
        Task AddUser(User user);
        Task<User> FindUserByUsername(string username);
        Task SaveChangesAsync();
    }

    public class ExpertFreezerRepository : IExpertFreezerRepository
    {
        private readonly ExpertFreezerContext _context;

        public ExpertFreezerRepository(ExpertFreezerContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.users.AnyAsync(u => u.Username == username);
        }

        public async Task AddUser(User user)
        {
            await _context.users.AddAsync(user);
        }

        public async Task<User> FindUserByUsername(string username)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ExpertFreezerProfile> GetExpertFreezer(long id)
        {
            var ExpertFreezer = await _context.expertFreezerProfiles.FindAsync(id);

            if (ExpertFreezer == null)
            {
                throw new Exception($"ExpertFreezer with id {id} not found.");  //currently not finding the write profile
            }

            return ExpertFreezer;
        }

        public async Task<ExpertFreezerProfile> CreateExpertFreezer(ExpertFreezerProfile expertFreezerProfile)
        {
            _context.expertFreezerProfiles.Add(expertFreezerProfile);
            await _context.SaveChangesAsync();

            return expertFreezerProfile;
        }

        public async Task<ExpertFreezerProfile> PatchExpertFreezer(PatchProfileDTO patchProfileDTO)
        {
            // Find the existing entity
            var existingProfile = await _context.expertFreezerProfiles.FindAsync(patchProfileDTO.Id);

            if (existingProfile == null)
            {
                throw new KeyNotFoundException($"ExpertFreezerProfile with ID {patchProfileDTO.Id} not found.");
            }

            // Update only the changed fields
            _context.Entry(existingProfile).CurrentValues.SetValues(patchProfileDTO);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return existingProfile;
        }

        public async Task<long> GetLastProfileID()
        {
            return await _context.expertFreezerProfiles.MaxAsync(x => (long?)x.Id) ?? 0;
        }

    }
}