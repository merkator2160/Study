using HhScraper.Extensions;
using HhScraper.Models;
using HhScraper.Models.Config;
using HhScraper.Models.Database;
using HhScraper.Models.Enums;
using HhScraper.Services.Interfaces;
using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;



namespace HhScraper.Views
{
    public partial class Scraper
    {
        private const Int32 VaitingForBrowserDelay = 5;

        private readonly HtmlDocument _htmlDoc;
        private readonly HhScramperEntities _db;

        private List<Vacancy> _vacancy;
        private List<String> _urlsForVacancyLinkSearch;
        private Int32 _delayCounter;
        private Int32 _currentVacancyIndex;
        private Int32 _currentUrlIndexForLinkSearch;
        private PageProcessingStatus _pageStatus;
        private String _htmlDirectory;

        private States _currentState;
        private States _previousState;
        private Boolean _pageLouded;
        private Int32 _delayDeviation;
        private Int32 _delayPodium;
        private Thread _scrapingThread;
        private readonly RootConfig _config;


        public Scraper(ISettingsService settingsService)
        {
            InitializeComponent();
            _config = settingsService.GetConfig();
            LoadSettings();

            _htmlDoc = new HtmlDocument
            {
                OptionFixNestedTags = true
            };
            _db = new HhScramperEntities();

            WebBrowser.LoadCompleted += PageLouded;
            WebBrowser.Navigated += WbMain_Navigated;

            _currentState = States.Stopped;
        }


        // HANDLERS ///////////////////////////////////////////////////////////////////////////////
        private void WbMain_Navigated(Object sender, NavigationEventArgs e)
        {
            WebBrowser.SuppressJsErrors(true);
        }
        private void NavigateBtn_Click(Object sender, RoutedEventArgs e)
        {
            Navigate(UrlTb.Text.Trim());
        }
        private void CollectBtn_Click(Object sender, RoutedEventArgs e)
        {
            if (_pageLouded)
            {
                if (WebBrowser.Document != null && XPathTb.Text.Trim() != "")
                {
                    if (CollectAttributesRb.IsChecked != null && CollectAttributesRb.IsChecked.Value)
                        XPathAttributeTestReport(XPathAttributeTest(XPathTb.Text.Trim(), AttributeTb.Text.Trim()));

                    if (CollectDataRb.IsChecked != null && CollectDataRb.IsChecked.Value)
                        XPathDataTestReport(XPathDataTest(XPathTb.Text.Trim()));
                }
                else
                {
                    LogRtbAppendText("There is no HTML or xPathWorker!" + "\r\n");
                }
            }
            else
            {
                LogRtbAppendText("The page is still not louded!" + "\r\n");
            }
        }
        private void RunScrampingBtn_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentState == States.Stopped)
                {
                    _vacancy = new List<Vacancy>();

                    if (UseExpressionsCb.IsChecked.Value)
                    {
                        if (!ExpressionUrlProcessing(UrlTb.Text.Trim()))
                        {
                            LogRtbAppendText("There is no any url to search!" + "\r\n");
                            return;
                        }
                    }
                    else
                    {
                        if (!UrlsProcessing(UrlTb.Text.Trim()))
                        {
                            LogRtbAppendText("There is no any url to search!" + "\r\n");
                            return;
                        }
                    }

                    SetCurrentState(States.Started);
                    UiElementsIsEnabled(false);
                    RunScrampingBtnUpdateContent("Stop scramping");

                    _delayDeviation = Convert.ToInt32(DelayDeviationTb.Text.Trim());
                    _delayPodium = Convert.ToInt32(DelayPodiumTb.Text.Trim());

                    _scrapingThread = new Thread(ScrapingThread)
                    {
                        IsBackground = true
                    };
                    _scrapingThread.Start();
                }
                else
                    StopJob("Job was stopped by user.");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void Window_Closing(Object sender, CancelEventArgs e)
        {
            _scrapingThread?.Abort();
            SaveSettings();
        }


        // THREADS ////////////////////////////////////////////////////////////////////////////////
        private void ScrapingThread()
        {
            do
            {
                ScrampingStep();
                Thread.Sleep(1000);
            } while (true);
        }
        private void ScrampingStep()
        {
            if (_delayCounter <= 0)
            {
                #region Started

                if (_currentState == States.Started)
                {
                    LogRtbAppendText("Navigating site: " + (_currentVacancyIndex + 1) + " of " + _urlsForVacancyLinkSearch.Count + "\r\n");
                    Navigate(_urlsForVacancyLinkSearch[_currentUrlIndexForLinkSearch++]);
                    _pageStatus = PageProcessingStatus.Collecting;
                    SetCurrentState(States.CollectingLinks);
                    return;
                }

                #endregion

                if (_pageLouded)
                {

                    #region WatingForBrowser

                    if (_currentState == States.WatingForBrowser)
                        SetCurrentState(_previousState);

                    #endregion
                    #region CollectingLinks

                    if (_currentState == States.CollectingLinks)
                    {
                        if (_currentUrlIndexForLinkSearch <= _urlsForVacancyLinkSearch.Count)
                        {
                            if (_pageStatus == PageProcessingStatus.Navigating)
                            {
                                LogRtbAppendText("Navigating site: " + (_currentUrlIndexForLinkSearch) + " of " + _urlsForVacancyLinkSearch.Count + "\r\n");
                                Navigate(_urlsForVacancyLinkSearch[_currentUrlIndexForLinkSearch - 1]);
                                _pageStatus = PageProcessingStatus.Collecting;
                            }
                            else
                            {
                                LogRtbAppendText("Processing site: " + (_currentUrlIndexForLinkSearch) + " of " + _urlsForVacancyLinkSearch.Count + "\r\n");

                                GetVacancyes();
                                _currentUrlIndexForLinkSearch++;

                                _delayCounter = GenerateDelay(_delayPodium, _delayDeviation);
                                TimeToNextVacancyLblUpdateContent();

                                _pageStatus = PageProcessingStatus.Navigating;
                            }
                            return;
                        }

                        if (_vacancy.Count == 0)
                            StopJob("There is no vacancyes.");

                        PrintCollectedVacancyesReport();
                        SetCurrentState(States.CollectingData);

                        LogRtbAppendText("Navigating site: " + (_currentVacancyIndex + 1) + " of " + _vacancy.Count + "\r\n");
                        Navigate(_vacancy[_currentVacancyIndex].VacancyPageUrl);
                        _pageStatus = PageProcessingStatus.Collecting;
                        return;
                    }

                    #endregion
                    #region CollectingData

                    if (_currentState == States.CollectingData)
                    {
                        if (_currentVacancyIndex < _vacancy.Count)  //_vacancy.Count
                        {
                            if (_pageStatus == PageProcessingStatus.Navigating)
                            {
                                LogRtbAppendText("Navigating site: " + (_currentVacancyIndex + 1) + " of " + _vacancy.Count + "\r\n");
                                Navigate(_vacancy[_currentVacancyIndex].VacancyPageUrl);
                                _pageStatus = PageProcessingStatus.Collecting;
                            }
                            else
                            {
                                LogRtbAppendText("Processing site: " + (_currentVacancyIndex + 1) + " of " + _vacancy.Count + "\r\n");
                                GetVacancyData();
                                WriteVacancyHtmlToFile();
                                SaveVacancyToDb();
                                _currentVacancyIndex++;

                                _delayCounter = GenerateDelay(_delayPodium, _delayDeviation);
                                TimeToNextVacancyLblUpdateContent();

                                _pageStatus = PageProcessingStatus.Navigating;
                            }
                            return;
                        }

                        PrintCollectedDataReport();
                        //WriteAllVacancyesHtmlToFile();
                        //SaveAllVacancyesToDb();
                        StopJob("Job done!");
                    }
                    #endregion
                }
                else
                {
                    #region SET VAITING_FOR_BROWSER_DELAY

                    _delayCounter = VaitingForBrowserDelay;
                    TimeToNextVacancyLblUpdateContent();

                    if (_currentState != States.WatingForBrowser)
                        _previousState = _currentState;

                    SetCurrentState(States.WatingForBrowser);

                    #endregion
                }
            }
            else
            {
                _delayCounter--;
                TimeToNextVacancyLblUpdateContent();
            }
        }
        private void GetVacancyes()
        {
            try
            {
                var linkNodes = _htmlDoc.DocumentNode.SelectNodes(_config.ScraperConfig.HhVacancyPageUrlXpath);

                foreach (var x in linkNodes)
                {
                    var newVacancy = new Vacancy
                    {
                        VacancyPageUrl = "http://nn.hh.ru" + x.Attributes["href"].Value
                    };
                    _vacancy.Add(newVacancy);
                }
            }
            catch (NullReferenceException)
            {
                LogRtbAppendText("Found nothing." + "\r\n");
            }
        }
        private void PrintCollectedVacancyesReport()
        {
            var i = 0;
            LogRtbAppendText("\r\n");
            LogRtbAppendText("Collected " + _vacancy.Count + " vacancyes links" + "\r\n");
            foreach (var x in _vacancy)
            {
                i++;
                LogRtbAppendText(i + ") " + x.VacancyPageUrl.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0] + "\r\n");
            }
            LogRtbAppendText("\r\n");
        }
        private void GetVacancyData()
        {
            var currentTime = $"{DateTime.Now:dd.MM.yy-hh.mm}";
            var hhVacancyId = _vacancy[_currentVacancyIndex].VacancyPageUrl.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[3];
            var relativeFilePAth = "\\Pages\\" + "HH_" + currentTime + "_" + hhVacancyId + ".html";

            _vacancy[_currentVacancyIndex].VacancyPageHtml = _htmlDoc.DocumentNode.OuterHtml;
            _vacancy[_currentVacancyIndex].ScrampingDateTime = currentTime;
            _vacancy[_currentVacancyIndex].HhVacancyID = hhVacancyId;
            _vacancy[_currentVacancyIndex].RelativeFilePath = relativeFilePAth;

            #region Age <- Gender <- Address <- ReadyToRelocate

            try
            {
                var singleNode = _htmlDoc.DocumentNode.SelectSingleNode(_config.ScraperConfig.HhVacancyAgeXpath).InnerText.Replace("&nbsp;", " ").Trim();

                if (singleNode.Contains("пол"))
                {
                    _vacancy[_currentVacancyIndex].Age = "N/A";
                    _vacancy[_currentVacancyIndex].Gender = _htmlDoc.DocumentNode.SelectSingleNode(_config.ScraperConfig.HhVacancyAgeXpath).InnerText.Replace("&nbsp;", " ").Trim();
                    _vacancy[_currentVacancyIndex].Address = _htmlDoc.DocumentNode.SelectSingleNode(_config.ScraperConfig.HhVacancyGenderXpath).InnerText.Replace("&nbsp;", " ").Trim();
                    _vacancy[_currentVacancyIndex].ReadyToBusinessTrip = _htmlDoc.DocumentNode.SelectSingleNode(_config.ScraperConfig.HhVacancyAddressXpath).InnerText.Replace("&nbsp;", " ").Trim();
                }
                else
                {
                    _vacancy[_currentVacancyIndex].Age = singleNode;
                    _vacancy[_currentVacancyIndex].Gender = _htmlDoc.DocumentNode.SelectSingleNode(_config.ScraperConfig.HhVacancyGenderXpath).InnerText.Replace("&nbsp;", " ").Trim();
                    _vacancy[_currentVacancyIndex].Address = _htmlDoc.DocumentNode.SelectSingleNode(_config.ScraperConfig.HhVacancyAddressXpath).InnerText.Replace("&nbsp;", " ").Trim();
                    _vacancy[_currentVacancyIndex].ReadyToBusinessTrip = _htmlDoc.DocumentNode.SelectSingleNode(_config.ScraperConfig.HhVacancyReadyToRelocateXpath).InnerText.Replace("&nbsp;", " ").Trim();
                }
            }
            catch (NullReferenceException)
            {

            }

            #endregion

            _vacancy[_currentVacancyIndex].VacancyPublicationDate = GetInnerText(_config.ScraperConfig.HhVacancyPublicationDateXpath);
            _vacancy[_currentVacancyIndex].DesiredPosition = GetInnerText(_config.ScraperConfig.HhVacancyDesiredPositionXpath);
            _vacancy[_currentVacancyIndex].Salary = GetInnerText(_config.ScraperConfig.HhVacancySalaryXpath);
            _vacancy[_currentVacancyIndex].VacancyPhotoUrl = "http://hh.ru" + GetAttribute(_config.ScraperConfig.HhVacancyFioXpath, "src");

            _vacancy[_currentVacancyIndex].FIO = GetInnerText(_config.ScraperConfig.HhVacancyFioXpath);
            _vacancy[_currentVacancyIndex].AboutMe = GetInnerText(_config.ScraperConfig.HhVacancySkillsXpath);
            _vacancy[_currentVacancyIndex].WorkExperience = GetInnerText(_config.ScraperConfig.HhVacancyWorkExperienceXpath);
            _vacancy[_currentVacancyIndex].WorkHistory = GetInnerText(_config.ScraperConfig.HhVacancyWorkHistoryXpath);
            _vacancy[_currentVacancyIndex].Phone = GetInnerText(_config.ScraperConfig.HhVacancyPhoneXpath);
            _vacancy[_currentVacancyIndex].EMail = GetInnerText(_config.ScraperConfig.HhVacancyEmailXpath);
            _vacancy[_currentVacancyIndex].FieldOfActivity = GetInnerText(_config.ScraperConfig.HhVacancyFieldOfActivityXpath);
        }
        private String GetInnerText(String xPathExpression)
        {
            try
            {
                return _htmlDoc.DocumentNode.SelectSingleNode(xPathExpression).InnerText.Replace("&nbsp;", " ").Trim();
            }
            catch (NullReferenceException)
            {
                return "N/A";
            }
        }
        private String GetAttribute(String xPathExpression, String attributeName)
        {
            try
            {
                return _htmlDoc.DocumentNode.SelectSingleNode(xPathExpression).Attributes[attributeName].Value.Replace("amp;", " ").Trim();
            }
            catch (NullReferenceException)
            {
                return "N/A";
            }
        }

        private void PrintCollectedDataReport()
        {
            var i = 0;
            LogRtbAppendText("\r\n");
            LogRtbAppendText("Was collected " + _vacancy.Count + " vacancyes:" + "\r\n");
            foreach (var x in _vacancy)
            {
                i++;
                LogRtbAppendText(i + ") " + x.FIO + "\r\n");
            }
            LogRtbAppendText("\r\n");
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        private void PageLouded(Object sender, NavigationEventArgs e)
        {
            _htmlDoc.LoadHtml(((HTMLDocument)WebBrowser.Document).body.innerHTML);
            LogRtbAppendText("The page is louded." + "\r\n");
            _pageLouded = true;
        }
        private void Navigate(String url)
        {
            if (WebBrowser.CheckAccess())
            {
                _pageLouded = false;
                WebBrowser.Navigate(url);
                CurrentUrlLbl.Content = url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
            else
            {
                _pageLouded = false;
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    WebBrowser.Navigate(url);
                    CurrentUrlLbl.Content = url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0];
                }));
            }
        }
        private void StopJob(String message)
        {
            UiElementsIsEnabled(true);

            _delayCounter = 0;
            TimeToNextVacancyLblUpdateContent();

            _currentUrlIndexForLinkSearch = 0;
            _currentVacancyIndex = 0;

            SetCurrentState(States.Stopped);

            RunScrampingBtnUpdateContent("Start scramping");
            TimeToNextVacancyLblUpdateContent();
            LogRtbAppendText(message + "\r\n");

            _scrapingThread?.Abort();
        }
        private Int32 GenerateDelay(Int32 podium, Int32 deviation)
        {
            var rnd = new Random();
            var result = podium + rnd.Next(-deviation, deviation);

            if (result < 0)
                return -result;
            else
                return result;
        }
        private void SetCurrentState(States newState)
        {
            _currentState = newState;
            switch (_currentState)
            {
                case States.CollectingData:
                    CurrentStateLblUpdateContent("Collecting data");
                    break;
                case States.CollectingLinks:
                    CurrentStateLblUpdateContent("Collecting links");
                    break;
                case States.Started:
                    CurrentStateLblUpdateContent("Started");
                    break;
                case States.Stopped:
                    CurrentStateLblUpdateContent("Stopped");
                    break;
                case States.WatingForBrowser:
                    CurrentStateLblUpdateContent("Waiting for browser");
                    break;
                default:
                    CurrentStateLblUpdateContent("Unexpected state");
                    break;
            }
        }
        private Boolean UrlsProcessing(String urlsString)
        {
            String[] forbiddenUrlSymbols = { "[", "]", "," };

            foreach (var y in forbiddenUrlSymbols)
            {
                if (urlsString.Contains(y))
                {
                    LogRtbAppendText("Url conteins unresolved symbols!" + "\r\n");
                    return false;
                }
            }

            _urlsForVacancyLinkSearch = urlsString.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return true;
        }
        private Boolean ExpressionUrlProcessing(String urlsString)
        {
            try
            {
                _urlsForVacancyLinkSearch = new List<String>();

                var expressions = urlsString.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                foreach (var x in expressions)
                {
                    var urlParts = x.Split(new[] { '[', ',', ']' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var lower = Convert.ToInt32(urlParts[2]);
                    var upper = Convert.ToInt32(urlParts[1]);

                    for (var i = lower; i <= upper; i++)
                    {
                        _urlsForVacancyLinkSearch.Add(urlParts[0] + i);
                    }
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                LogRtbAppendText("Wrong url expression!" + "\r\n");
                return false;
            }
        }


        private void WriteVacancyHtmlToFile()
        {
            if (_htmlDirectory == null)
            {
                _htmlDirectory = Directory.GetCurrentDirectory();

                if (!Directory.Exists(_htmlDirectory + "\\pages"))
                    Directory.CreateDirectory(_htmlDirectory + "\\pages");
            }

            var filePath = _htmlDirectory + _vacancy[_currentVacancyIndex].RelativeFilePath;
            File.WriteAllText(filePath, _vacancy[_currentVacancyIndex].VacancyPageHtml, Encoding.Unicode);
            LogRtbAppendText("Saved to HTML." + "\r\n");
        }
        private void WriteAllVacancyesHtmlToFile()
        {
            if (_htmlDirectory == null)
            {
                _htmlDirectory = Directory.GetCurrentDirectory();

                if (!Directory.Exists(_htmlDirectory + "\\pages"))
                    Directory.CreateDirectory(_htmlDirectory + "\\pages");
            }

            foreach (var x in _vacancy)
            {
                var filePath = _htmlDirectory + x.RelativeFilePath;
                File.WriteAllText(filePath, x.VacancyPageHtml, Encoding.Unicode);
            }
        }



        private List<XPathTest> XPathAttributeTest(String xPathWorker, String attribute)
        {
            try
            {
                var testList = new List<XPathTest>();
                var htmlNodes = _htmlDoc.DocumentNode.SelectNodes(xPathWorker);

                if (htmlNodes != null)
                {
                    foreach (var x in htmlNodes)
                    {
                        var newVacancy = new XPathTest
                        {
                            Attribute = x.Attributes[attribute].Value
                        };
                        testList.Add(newVacancy);
                    }
                }
                return testList;
            }
            catch (NullReferenceException)
            {
                LogRtbAppendText("Exception!" + "\r\n");
                return new List<XPathTest>();
            }
        }
        private List<XPathTest> XPathDataTest(String xPathWorker)
        {
            try
            {
                var testList = new List<XPathTest>();
                var htmlNodes = _htmlDoc.DocumentNode.SelectNodes(xPathWorker);

                if (htmlNodes != null)
                {
                    foreach (var x in htmlNodes)
                    {
                        var newVacancy = new XPathTest
                        {
                            InnerText = x.InnerText.Replace("&nbsp;", " ").Trim()
                        };
                        testList.Add(newVacancy);
                    }
                }
                return testList;
            }
            catch (NullReferenceException)
            {
                LogRtbAppendText("Exception!" + "\r\n");
                return new List<XPathTest>();
            }
        }
        private void XPathAttributeTestReport(List<XPathTest> testList)
        {
            LogRtb.Document.Blocks.Clear();
            if (testList.Count != 0)
            {
                foreach (var x in testList)
                    LogRtbAppendText(x.Attribute.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0] + "\r\n");
            }
            else
            {
                LogRtbAppendText("Found nothing." + "\r\n");
            }
        }
        private void XPathDataTestReport(List<XPathTest> testList)
        {
            LogRtb.Document.Blocks.Clear();
            if (testList.Count != 0)
            {
                foreach (var x in testList)
                    LogRtbAppendText(x.InnerText + "\r\n");
            }
            else
            {
                LogRtbAppendText("Found nothing." + "\r\n");
            }
        }



        private void SaveVacancyToDb()
        {
            try
            {
                if (_vacancy[_currentVacancyIndex].FIO == "N/A") return;

                var fio = _vacancy[_currentVacancyIndex].FIO;
                var vacancyFromDbByFio = _db.Vacancies.First(p => p.FIO == fio);

                _vacancy[_currentVacancyIndex].VacancyID = vacancyFromDbByFio.VacancyID;

                _db.Vacancies.AddOrUpdate(_vacancy[_currentVacancyIndex]);
                _db.SaveChanges();
                LogRtbAppendText("Vacancy updated. " + "\r\n");
            }
            catch (EntityException)
            {
                LogRtbAppendText("Can't connect to DB." + "\r\n");
            }
            catch (InvalidOperationException)
            {
                _db.Vacancies.AddOrUpdate(_vacancy[_currentVacancyIndex]);
                _db.SaveChanges();
                LogRtbAppendText("Vacancy added. " + "\r\n");
            }
        }
        private void SaveAllVacancyesToDb()
        {
            try
            {
                foreach (var x in _vacancy)
                {
                    if (x.FIO == "N/A") continue;

                    var vacancyFromDbByFio = _db.Vacancies.Where(p => p.FIO == x.FIO);

                    if (vacancyFromDbByFio.Count() > 1)
                    {
                        LogRtbAppendText("Vacancy duplicates was found: " + x.FIO + "\r\n");
                    }

                    if (vacancyFromDbByFio.Count() != 0)
                    {
                        x.VacancyID = vacancyFromDbByFio.First().VacancyID;
                    }

                    _db.Vacancies.AddOrUpdate(x);
                }

                _db.SaveChanges();
                LogRtbAppendText("All vacancyes was successfully saved to DB." + "\r\n");
            }
            catch (EntityException)
            {
                LogRtbAppendText("Can't connect to DB." + "\r\n");
            }
        }
        private void SaveSettings()
        {
            try
            {
                var newSettings = _db.Settings.SingleOrDefault(p => p.SettingsID == 0);

                newSettings.UrlTb = UrlTb.Text;
                newSettings.XPathTb = XPathTb.Text;
                newSettings.CollectDataRb = CollectDataRb.IsChecked;
                newSettings.CollectLinksRb = CollectAttributesRb.IsChecked;
                newSettings.UseExpressionsCb = UseExpressionsCb.IsChecked;
                newSettings.DelayDeviationTb = DelayDeviationTb.Text;
                newSettings.DelayPodiumTb = DelayPodiumTb.Text;
                newSettings.SettingsID = 0;
                newSettings.AttributeTb = AttributeTb.Text;

                _db.SaveChanges();
            }
            catch (NullReferenceException)
            {
                var newSettings = new Setting
                {
                    UrlTb = UrlTb.Text,
                    XPathTb = XPathTb.Text,
                    CollectDataRb = CollectDataRb.IsChecked,
                    CollectLinksRb = CollectAttributesRb.IsChecked,
                    UseExpressionsCb = UseExpressionsCb.IsChecked,
                    DelayDeviationTb = DelayDeviationTb.Text,
                    DelayPodiumTb = DelayPodiumTb.Text,
                    SettingsID = 0,
                    AttributeTb = AttributeTb.Text
                };

                _db.Settings.Add(newSettings);
                _db.SaveChanges();
            }
            catch (EntityException)
            {
                LogRtbAppendText("Can't connect to DB." + "\r\n");
            }
        }
        private void LoadSettings()
        {
            try
            {
                var settingsFromDb = _db.Settings.SingleOrDefault(p => p.SettingsID == 0);

                UrlTb.Text = settingsFromDb.UrlTb;
                XPathTb.Text = settingsFromDb.XPathTb;
                CollectDataRb.IsChecked = Convert.ToBoolean(settingsFromDb.CollectDataRb);
                CollectAttributesRb.IsChecked = Convert.ToBoolean(settingsFromDb.CollectLinksRb);
                UseExpressionsCb.IsChecked = Convert.ToBoolean(settingsFromDb.UseExpressionsCb);
                DelayDeviationTb.Text = settingsFromDb.DelayDeviationTb;
                DelayPodiumTb.Text = settingsFromDb.DelayPodiumTb;
                AttributeTb.Text = settingsFromDb.AttributeTb;
            }
            catch (NullReferenceException)
            {
                LogRtbAppendText("Settings not found." + "\r\n");
            }
            catch (EntityException)
            {
                LogRtbAppendText("Can't connect to DB." + "\r\n");
            }
        }


        private void LogRtbAppendText(String someText)
        {
            if (LogRtb.CheckAccess())
            {
                LogRtb.AppendText(someText);
                LogRtb.ScrollToEnd();
            }

            else
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    LogRtb.AppendText(someText);
                    LogRtb.ScrollToEnd();
                }));
        }
        private void LogRtbClear()
        {
            if (LogRtb.CheckAccess())
                LogRtb.Document.Blocks.Clear();
            else
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => LogRtb.Document.Blocks.Clear()));
        }
        private void TimeToNextVacancyLblUpdateContent()
        {
            if (TimeToNextVacancyLbl.CheckAccess())
                TimeToNextVacancyLbl.Content = _delayCounter.ToString();
            else
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => TimeToNextVacancyLbl.Content = _delayCounter.ToString()));
        }
        private void RunScrampingBtnUpdateContent(String content)
        {
            if (RunScrampingBtn.CheckAccess())
                RunScrampingBtn.Content = content;
            else
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => RunScrampingBtn.Content = content));
        }
        private void CurrentStateLblUpdateContent(String content)
        {
            if (CurrentStateLbl.CheckAccess())
                CurrentStateLbl.Content = content;
            else
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => CurrentStateLbl.Content = content));
        }
        private void UiElementsIsEnabled(Boolean value)
        {
            if (TestGb.CheckAccess())
            {
                TestGb.IsEnabled = value;
                UseExpressionsCb.IsEnabled = value;
                DelayDeviationTb.IsEnabled = value;
                DelayPodiumTb.IsEnabled = value;
            }
            else
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    TestGb.IsEnabled = value;
                    UseExpressionsCb.IsEnabled = value;
                    DelayDeviationTb.IsEnabled = value;
                    DelayPodiumTb.IsEnabled = value;
                }));
        }


        // CRUTCHES ///////////////////////////////////////////////////////////////////////////////
        private String ExtractValueCrutch(String html, String startSuffix, String endSuffix)
        {
            var startIndex = html.IndexOf(startSuffix) + startSuffix.Length;

            html = html.Substring(startIndex, html.Length - startIndex);
            var endIndex = html.IndexOf(endSuffix);

            var fieldValue = html.Substring(0, endIndex);
            return fieldValue;
        }
        private void FunctionTestBtn_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}
