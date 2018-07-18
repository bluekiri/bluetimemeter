namespace BlueTimeMeter
{
    class BTM_In : BTM
    {
        public BTM_In():base(){}

        public BTM_In(string name, string filePath, string methodName, int codeLine) : base(name,
            filePath, methodName, codeLine)
        {}
    }
}