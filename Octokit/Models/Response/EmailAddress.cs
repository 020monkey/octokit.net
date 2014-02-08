using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("DebuggerDisplay,nq")]
    public class EmailAddress
    {
        /// <summary>
        /// The email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// true if the email is verified; otherwise false
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// true if this is the users primary email; otherwise false
        /// </summary>
        public bool Primary { get; set; }

        private string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "EmailAddress: Email: {0}; Primary: {1}, Verified: {2}", Email, Primary, Verified);
            }
        }
    }
}