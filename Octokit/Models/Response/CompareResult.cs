﻿using System.Collections.Generic;

namespace Octokit
{
    public class CompareResult
    {
        public string Url { get; set; }
        public string HtmlUrl { get; set; }
        public string PermalinkUrl { get; set; }
        public string DiffUrl { get; set; }
        public string PatchUrl { get; set; }
        public Commit BaseCommit { get; set; }
        public string Status { get; set; }
        public int AheadBy { get; set; }
        public int BehindBy { get; set; }
        public int TotalCommits { get; set; }
        public IReadOnlyCollection<Commit> Commits { get; set; }
    }
}
