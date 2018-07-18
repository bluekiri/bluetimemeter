namespace BlueTimeMeter
{
    class BTM_Out : BTM
    {
        public BTM_Out() : base(){ }
        public BTM_Out(string name, string filePath, string methodName, int codeLine) : base(name,
            filePath, methodName, codeLine)
        { }
    }
}