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
        Task<long> GetLastId();
        Task<ExpertFreezerProfile> GetLatestExpertFreezer();
        Task<bool> UserExists(string username);
        Task AddUser(User user);
        Task<User> FindUserById(long id);
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

        public async Task<User> FindUserById(long id)
        {
            return await _context.users.FindAsync(id);
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
                throw new Exception($"ExpertFreezer with id {id} not found.");
            }

            return ExpertFreezer;
        }

        public async Task<ExpertFreezerProfile> CreateExpertFreezer(ExpertFreezerProfile expertFreezerProfile)
        {
            _context.expertFreezerProfiles.Add(expertFreezerProfile);
            await _context.SaveChangesAsync();

            return expertFreezerProfile;
        }

        public async Task<long> GetLastId()
        {
            return await _context.expertFreezerProfiles.MaxAsync(x => (long?)x.Id) ?? 0;
        }

        public async Task<ExpertFreezerProfile> GetLatestExpertFreezer()
        {
            var latestExpertFreezer = await _context.expertFreezerProfiles.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            if (latestExpertFreezer == null)
            {
                throw new Exception("No ExpertFreezer found.");
            }

            return latestExpertFreezer;
        }
    }
}