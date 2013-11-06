﻿using System;

namespace Octokit
{
    public class SearchRepo
    {
        /// <summary>
        /// repo name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// full name of repo e.g. dtrupenn/Tetris
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// owner of repo
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// is a private repo?
        /// </summary>
        public bool Private { get; set; }

        /// <summary>
        /// description of repo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// is repo a fork
        /// </summary>
        public bool Fork { get; set; }
    }
}