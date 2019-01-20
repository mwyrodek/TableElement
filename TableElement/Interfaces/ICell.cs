using OpenQA.Selenium;

namespace TableElement
{
    public interface ICell
    {
        int Column{ get;}
        int Row { get; }
        IWebElement Element{ get; }
    }
}