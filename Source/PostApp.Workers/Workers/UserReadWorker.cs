using System.Text.Json;
using Hangfire;
using PostApp.BL.Interfaces;
using PostApp.DL.EntityFramework.Models;
using PostApp.Web.Common.HttpClients;
using PostApp.Workers.Interfaces;
using PostApp.Workers.Mapper;

namespace PostApp.Workers.Workers;

public class UserReadWorker : IWorker
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IUserInfoService _userInfoService;
    private readonly UserMapper _userMapper;
    private readonly IDataServiceApiClient _dataServiceApiClient;
    
    public UserReadWorker(
        IBackgroundJobClient backgroundJobClient,
        IUserInfoService userInfoService,
        UserMapper userMapper,
        IDataServiceApiClient dataServiceApiClient)
    {
        _backgroundJobClient = backgroundJobClient;
        _userInfoService = userInfoService;
        _userMapper = userMapper;
        _dataServiceApiClient = dataServiceApiClient;
    }
    
    public async Task ExecuteAsync(int bulkSize)
    {
        try
        {
            var users = await _dataServiceApiClient.GetUsersAsync();
            foreach (var user in users)
            {
                var userInfo = _userMapper.ToUserInfo(user);
                
                _backgroundJobClient.Enqueue(() => ProcessAsync(userInfo));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
    
    public async Task ProcessAsync(User user)
    {
        await _userInfoService.UpsertAsync(user, CancellationToken.None);
    }
}