using TvMazeApi.Models;

namespace TvMazeApi.Services
{
    public interface IService
    {
        Record GetRecord(int id);

        void SetRecord(Record record);
    }
}
