using First.App.Business.Abstract;
using First.App.Domain.Entities;
using GetPostsBackgroundWorker.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GetPostsBackgroundWorker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;
        private HttpClient httpClient;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider) => (_serviceProvider, _logger) = (serviceProvider, logger);

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var request = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

                if (request.IsSuccessStatusCode)
                {
                    string responseBody = await request.Content.ReadAsStringAsync();
                    // get posts
                    List<PostDto> postDtos = JsonConvert.DeserializeObject<List<PostDto>>(responseBody).ToList();
                    List<Post> posts = new List<Post>();
                    // add them to Post list
                    postDtos.ForEach(x =>
                    {
                        posts.Add(new Post
                        {
                            CreatedAt = DateTime.Now,
                            CreatedBy = "Batuhan", // required
                            IsDeleted = false,
                            UserId = x.UserId,
                            Body = x.Body,
                            Title = x.Title
                        });
                    });

                    // call services then dispose them
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        try
                        {
                            // get and use required service
                            var postService = scope.ServiceProvider.GetRequiredService<IPostService>();
                            postService.AddPosts(posts);
                            _logger.LogInformation("new Posts added to database at {0}", DateTime.Now);
                        }
                        catch(Exception ex)
                        {
                            _logger.LogError($"Problem adding posts to database! \n {ex}");
                        }
                    }
                }
                else
                {
                    _logger.LogError("Cannot get posts, Status Code {StatusCode}", request.StatusCode);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
