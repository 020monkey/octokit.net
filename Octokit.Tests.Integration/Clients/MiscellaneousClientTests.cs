﻿using System.Threading.Tasks;
using Octokit.Tests.Integration;
using Xunit;

public class MiscellaneousClientTests
{
    public class TheGetEmojisMethod
    {
        [IntegrationTest]
        public async Task GetsAllTheEmojis()
        {
            var github = Helper.GetAuthenticatedClient();

            var emojis = await github.Miscellaneous.GetEmojis();

            Assert.True(emojis.Count > 1);
        }
    }

    public class TheRenderRawMarkdownMethod
    {
        [IntegrationTest]
        public async Task CanRenderGitHubFlavoredMarkdown()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.RenderRawMarkdown("This is\r\n a **test**");

            Assert.Equal("<p>This is\n a <strong>test</strong></p>\n", result);
        }
    }

    public class TheGetGitIgnoreTemplatesMethod
    {
        [IntegrationTest]
        public async Task ReturnsListOfGitIgnoreTemplates()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.GetGitIgnoreTemplates();

            Assert.True(result.Count > 2);
        }
    }

    public class TheGetLicensesMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveListOfLicenses()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.GetLicenses();

            Assert.True(result.Count > 2);
            Assert.Contains(result, license => license.Key == "mit");
        }
    }
}
