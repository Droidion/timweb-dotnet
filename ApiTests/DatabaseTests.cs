using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Shouldly;
using Timweb.Api.Services;
using Xunit;

namespace Timweb.ApiTests
{
    [UsedImplicitly]
    public record Result
    {
        public int Res;
    }
    
    public class DatabaseTests
    {
        [Fact]
        public async void QueryDbReturnsResult()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Test.json")
                .Build();
            var db = new Database(config);
            var result = await db.QueryDb<Result>("SELECT 1 AS res");
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result[0].ShouldBe(new Result { Res = 1});
        }
    }
}