namespace MudDesigner.Engine
{
    public struct ComponentTime
    {
        public ComponentTime(double lastUpdateTime, double elapsedSinceLastUpdate, double updateTime, double aliveTime)
        {
            this.AliveTime = aliveTime;
            this.LastUpdateTime = lastUpdateTime;
            this.ElapsedSinceLastUpdate = elapsedSinceLastUpdate;
            this.UpdateTime = updateTime;
        }

        public double LastUpdateTime { get; }
        public double ElapsedSinceLastUpdate { get; }
        public double UpdateTime { get; }
        public double AliveTime { get; }
    }
}
