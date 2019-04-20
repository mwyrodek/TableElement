using OpenQA.Selenium;

namespace TableElement.Interfaces
{
    public interface ICell
    {
        int Column{ get;}
        int Row { get; }
        IWebElement Element{ get; }
    }
}