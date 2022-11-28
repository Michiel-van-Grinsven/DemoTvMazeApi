using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TvMazeApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TvMazeApi.Data
{
    internal class Crawler
    {
        private int page = 0;
        private const int resultsPerPage = 250;



        public Crawler(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var context = new ApplicationDbContext(contextOptions);
            StartAsync(context);
        }


        public async void StartAsync(ApplicationDbContext dbContext)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();

            List<Record> result = null;

            while (result == null || !result.Any())
            {

                result = await ProcessRepositoriesAsync(client, page);
                foreach (var record in result.Where(r => DateTime.Parse(r.Premiered).CompareTo(DateTime.Parse("2014-01-01")) > 0))
                {
                    dbContext.Record.Add(record);
                }
                dbContext.SaveChanges();
                Thread.Sleep(500);
                page++;
            }

        }

        static async Task<List<Record>> ProcessRepositoriesAsync(HttpClient client, int pageNumber)
        {
            await using Stream stream =
                await client.GetStreamAsync($"https://api.tvmaze.com/shows?page={pageNumber}");

            var repositories =
                await JsonSerializer.DeserializeAsync<List<Record>>(stream);
            return repositories ?? new();
        }
    }
}