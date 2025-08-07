using System;
using Delta.Performance.Clocks;

namespace Delta.Performance
{
    public class Chronometer : IDisposable
    {
        private enum State
        {
            Created,
            Started,
            Stopped
        }

        private readonly Action<string> log;
        private State state = State.Created;
        
        internal Chronometer(Action<string> logAction, IClock clock)
        {
            log = logAction;
        }

        public void Start()
        {
            log("Starting chronometer");
            state = State.Started;
        }

        public void Stop()
        {
            state = State.Stopped;
            log("Stopped chronometer");
        }

        public void Dispose()
        {
            if (state == State.Started)
                Stop();
        }
    }
}
