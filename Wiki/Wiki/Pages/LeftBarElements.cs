using OpenQA.Selenium;

namespace Wiki.Pages
{
    /// <summary>
    /// Левая панель сайта
    /// </summary>
    public class LeftBarElements
    {
        private readonly IWebDriver _driver;

        public LeftBarElements(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Гиперссылка "Случайная статья"
        /// </summary>
        public IWebElement RandomPageLink => _driver.FindElement(By.XPath("//a[@accessKey='x']"));
    }
}
