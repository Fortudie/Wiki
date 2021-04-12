using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Wiki.Pages
{
    /// <summary>
    /// Основной контент статьи на сайте
    /// </summary>
    public class ContentPage
    {
        private readonly IWebDriver _driver;

        public ContentPage(IWebDriver driver)
        {
            _driver = driver;
        }
        
        /// <summary>
        /// Заголовок статьи
        /// </summary>
        public IWebElement ContentHeader => _driver.FindElement(By.Id("firstHeading"));
        
        /// <summary>
        /// Корневой элемент с данными статьи
        /// </summary>
        private IWebElement BodyContentRootElement => _driver.FindElement(By.XPath(BodyContentRootElementPath));
        private const string BodyContentRootElementPath = "//div[@id='mw-content-text']";
        /// <summary>
        /// Ссылки в текстовых полях
        /// </summary>
        public IEnumerable<IWebElement> ArticleTextLinks => _driver.FindElements(By.XPath(ArticleTextLinkPath));
        private const string ArticleTextLinkPath = BodyContentRootElementPath + "//p//a[@href and @title]";
        /// <summary>
        /// Выбор первой ссылки в в контенте статьи
        /// </summary>
        /// <returns></returns>
        public IWebElement GetFirstArticleLink()
        {
            return ArticleTextLinks.First();
        }
    }
}