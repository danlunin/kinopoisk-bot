namespace FilmAdvisor
{
    public interface IBot
    {
        void Run();
        string Ask(Question question);
        void SendMessage(string text);
    }
}
