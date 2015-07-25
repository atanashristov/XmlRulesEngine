using System;

namespace XmlRulesEngine.Misc
{
    /// <summary>
    /// Injected for testability.
    /// </summary>
    public class TimeMachine
    {
        /// <summary>
        /// Wraps DateTime.Now. Override in mocks when testing.
        /// </summary>
        /// <value>
        /// DateTime.Now.
        /// </value>
        public virtual DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
