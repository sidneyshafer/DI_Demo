using System;

namespace WazeCredit.Service.LifeTimeServices
{
    public class TransientService
    {
        private readonly Guid guid;

        public TransientService()
        {
            guid = Guid.NewGuid();
        }

        public string GetGuid() => guid.ToString();
    }
}
