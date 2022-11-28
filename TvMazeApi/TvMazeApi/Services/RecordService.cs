using Microsoft.EntityFrameworkCore;
using TvMazeApi.Data;
using TvMazeApi.Models;

namespace TvMazeApi.Services
{
    public class RecordService : IService
    {
        private readonly ApplicationDbContext _context;

        public RecordService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public Record GetRecord(int id)
        {
            var users = from u in _context.Record where u.RecordId == id select u;
            if (users.Count() == 1)
            {
                return users.First();
            }
            return null;
        }

        public void SetRecord(Record record)
        {
            _context.Record.Add(record);
            _context.SaveChanges();

        }
    }
}
