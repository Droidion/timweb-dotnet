using Shouldly;
using Timweb.Api.Services.Sql;
using Xunit;

namespace ApiTests
{
    public class SqlTests
    {
        [Fact]
        public void AddLimitSkipBuildsSqlWithNoLimitOrSkip()
        {
            HelpersSql.AddLimitSkip("SELECT 1", "10foo", "20bar").ShouldBe("SELECT 1");
            HelpersSql.AddLimitSkip("SELECT 1", null, null).ShouldBe("SELECT 1");
            HelpersSql.AddLimitSkip("SELECT 1", "", "").ShouldBe("SELECT 1");
        }

        [Fact]
        public void AddLimitSkipBuildsSqlWithLimit()
        {
            var result = HelpersSql.AddLimitSkip("SELECT 1", "10", "");
            result.ShouldBe("SELECT 1 LIMIT 10");
        }

        [Fact]
        public void AddLimitSkipBuildsSqlWithSkip()
        {
            var result = HelpersSql.AddLimitSkip("SELECT 1", "", "20");
            result.ShouldBe("SELECT 1 OFFSET 20");
        }

        [Fact]
        public void AddLimitSkipBuildsSqlWithLimitAndSkip()
        {
            var result = HelpersSql.AddLimitSkip("SELECT 1", "10", "20");
            result.ShouldBe("SELECT 1 LIMIT 10 OFFSET 20");
        }
    }
}