using System;

namespace WazeCredit.Service.LifeTimeServices
{
    public class ScopedService
    {
        private readonly Guid guid;

        public ScopedService()
        {
            guid = Guid.NewGuid();
        }

        public string GetGuid() => guid.ToString();
    }
}
