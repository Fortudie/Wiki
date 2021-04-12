using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Wiki.Pages;

namespace Wiki
{
    public class WikiTestClass
    {
        public static void Main()
        {
            var driver = Init();
            CheckFollowingLinkQuantity(driver, "Философия");
        }

        /// <summary>
        /// Инициализируем драйвер
        /// </summary>
        /// <returns></returns>
        public static IWebDriver Init()
        {
            //Инит
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            chromeOptions.AddArgument("incognito");

            //chromeOptions.AddArguments("headless");

            var driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, chromeOptions);
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            driver.Navigate().GoToUrl("https://ru.wikipedia.org/");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);

            return driver;
        }


        public static void CheckFollowingLinkQuantity(IWebDriver driver, string targetArticleName)
        {
            //Загружаем модель экрана со статьей
            var contentPage = new ContentPage(driver);
            //Инициализируем набор записей для логирования
            var logList = new List<string>();
            //Создаем переменную - счетчик
            var counter = 0;
            //Кликаем гиперссылку Случайная страница для старта последовательности
            var leftBarElements = new LeftBarElements(driver);
            leftBarElements.RandomPageLink.Click();

            //Делаем запись в логе в случае попадания на первую страницу - целевую
            if (contentPage.ContentHeader.Text == targetArticleName)
            {
                var logString = $"Шаг {counter} - {contentPage.ContentHeader.Text} - {driver.Url}";
                logList.Add(logString);
                File.WriteAllLines("report.txt", logList);
                return;
            }

            //В цикле выполняем переходы до попадания на целевую страницу
            while (contentPage.ContentHeader.Text != targetArticleName)
            {
                //Увеличиваем значение счетчика
                counter++;
                //Делаем запись в лог
                var logString = $"Шаг {counter} - {contentPage.ContentHeader.Text} - {driver.Url}";
                logList.Add(logString);
                //Кликаем на первую ссылку в данных статьи
                contentPage = new ContentPage(driver);
                contentPage.GetFirstArticleLink().Click();
            }

            //Записать результат в файл
            File.WriteAllLines("report.txt", logList);
        }
    }
}