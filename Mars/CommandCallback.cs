namespace Mars
{
    public abstract class CommandCallback
    {
    }

    public class CommandCallbackSimple : CommandCallback
    {
        public string name;
        
        public CommandCallbackSimple(string name)
        {
            this.name = name;
        }
    }
}