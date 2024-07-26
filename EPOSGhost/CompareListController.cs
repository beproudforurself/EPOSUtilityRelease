using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunny.UI;
using System.Globalization;

namespace EPOSGhost
{
    public class ASample_CompareListController : CompareListInterface
    {
        public void CompareListGenerate(string PNbase, string PNCompared)
        {       
            string url = "https://rb-wam.bosch.com/apxepos/f?p=101:50:102041068515629:::::";
            string LayoutnameBase = "";
            string LaoutnameCompared = "";
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"\\bosch.com\dfsrb\DfsCN\DIV\CC\Prj\Engineering_HardWare\ECU_Designer\06_A_Sample\12_Tools&Devices\EPOSUtility2.0\geckodriver.exe");
            
            FirefoxOptions options = new FirefoxOptions();
            options.BinaryLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Firefox.FirefoxDriver(service,options))
            {

                driver.Navigate().GoToUrl(url);
                Thread.Sleep(5000);
                LayoutnameBase = getLayoutname(PNbase, LayoutnameBase);
                Thread.Sleep(2000);
                driver.FindElements(By.ClassName("a-IRR-button--remove"))[0].Click();
                Thread.Sleep(5000);
                LaoutnameCompared = getLayoutname(PNCompared, LaoutnameCompared);
                Thread.Sleep(2000);

                OnlyAsampleChangeListGenerate(LayoutnameBase, LaoutnameCompared, PNbase, PNCompared);

                string getLayoutname(string PN, string layname)
                {
                    var element_PNbase = driver.FindElement(By.Id("ERRATA_search_field"));
                    element_PNbase.SendKeys(PN);
                    Thread.Sleep(1500);
                    driver.FindElement(By.Id("ERRATA_search_button")).Click();
                    Thread.Sleep(3000);
                    var table = driver.FindElement(By.ClassName("a-IRR-table"));
                    if ((table is IWebElement) == false) throw new Exception("Error：不支持");
                    var tbody = table.FindElement(By.TagName("tbody"));
                    Thread.Sleep(3500);
                    List<List<IWebElement>> res = new List<List<IWebElement>>();
                    foreach (var tr in tbody.FindElements(By.TagName("tr")))
                    {
                        List<IWebElement> row = new List<IWebElement>();
                        foreach (var td in tr.FindElements(By.TagName("td")))
                        {
                            row.Add(td);
                        }
                        res.Add(row);
                    }
                    Thread.Sleep(1500);
                    List<List<string>> resnew = new List<List<string>>();
                    foreach (var row in res)
                    {
                        List<string> newRow = new List<string>();
                        foreach (var item in row)
                        {
                            var d = item.Text;
                            newRow.Add(item.Text);
                        }
                        resnew.Add(newRow);
                    }
                    List<DataTableClass> dataTableClasses = new List<DataTableClass>();
                    foreach (var tableitem in resnew)
                    {
                        if (tableitem.Count > 0)
                        {
                            DataTableClass tableStage = new DataTableClass();
                            int index = 0;
                            foreach (var pro in typeof(DataTableClass).GetProperties())
                            {
                                pro.SetValue(tableStage, tableitem[index]);
                                index++;
                            }
                            dataTableClasses.Add(tableStage);
                        }
                    }
                    var chosenItem = dataTableClasses.OrderByDescending(p => p.InsertDate.Split(' ')[0].Split('.')[2]).FirstOrDefault();
                    layname = (chosenItem.LaoName + "_V" + chosenItem.LayoutVersion).Trim();
                    return layname;

                }
            }
        }

        private static void OnlyAsampleChangeListGenerate(string LayoutnameBase, string LaoutnameCompared, string PNbase, string PNCompared)
        {

            string url = "https://rb-wam.bosch.com/apxepos/f?p=101:16:111516908018753:::::";
            try
            {
                FirefoxOptions options = new FirefoxOptions();
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"\\bosch.com\dfsrb\DfsCN\DIV\CC\Prj\Engineering_HardWare\ECU_Designer\06_A_Sample\12_Tools&Devices\EPOSUtility2.0\geckodriver.exe");
                options.BinaryLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                using (IWebDriver driver = new OpenQA.Selenium.Firefox.FirefoxDriver(service,options))
                {
                    driver.Navigate().GoToUrl(url);

                    Thread.Sleep(5000);
                    FindelementsLayout(LayoutnameBase, "P16_GET_LAYOUT1", "PopupLov_16_P16_GET_LAYOUT1_dlg");
                    FindelementsLayout(LaoutnameCompared, "P16_GET_LAYOUT2", "PopupLov_16_P16_GET_LAYOUT2_dlg");
                    FindelementsVariant(PNbase, "P16_GET_VARIANT1");
                    FindelementsVariant(PNCompared, "P16_GET_COMPARE");
                    ReadAsampleFormat InstanceRead = new ReadAsampleFormat();
                    List<string> AsampleFormat = InstanceRead.AsampleFormats();
                    Thread.Sleep(1500);
                    FindelementsVariant("1. A-Sample partlist change", "RGN_COMPARE_PARTLISTS_not_used_saved_reports");
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_actions_button")).Click();
                    Thread.Sleep(1500);
                    driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_actions_menu_7")).Click();
                    Thread.Sleep(1500);
                    Thread.Sleep(1500);
                    customerDefineColumn(AsampleFormat);
                    Thread.Sleep(1500);
                    driver.FindElement(By.ClassName("ui-button--hot")).Click();
                    Thread.Sleep(3000);
                    var trelements = driver.FindElement(By.Id("457796574888269099")).FindElement(By.TagName("tr")).FindElements(By.TagName("a"));
                    Thread.Sleep(2000);
                    foreach (var trelement in trelements)
                    {
                        if (trelement.GetAttribute("data-fht-column-idx") == "0")
                        {
                            trelement.Click();
                            Thread.Sleep(1500);
                            break;
                        }
                    }
                    var currencynew = driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_column_01"));
                    var choseElement = new OpenQA.Selenium.Support.UI.SelectElement(currencynew);
                    foreach (var chose in choseElement.Options)
                    {
                        if (chose.Text == "Layout Module1")
                        {
                            chose.Click();
                            Thread.Sleep(2500);
                            break;
                        }
                    }
                    driver.FindElement(By.ClassName("ui-button--hot")).Click();
                    Thread.Sleep(1500);
                    driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_actions_button")).Click();
                    Thread.Sleep(1500);
                    driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_actions_menu_12")).Click();
                    Thread.Sleep(1500);
                    //define Layout
                    void FindelementsLayout(string customer, string elePagewebsiteid, string elePagewebsitemodal)
                    {
                        var element = driver.FindElement(By.Id(elePagewebsiteid));
                        element.SendKeys(customer);
                        Thread.Sleep(1000);
                        foreach (var item in driver.FindElement(By.Id(elePagewebsitemodal)).FindElements(By.ClassName("a-IconList-item")))
                        {
                            if (item.GetAttribute("tabindex") == "0")
                            {
                                item.Click();
                                Thread.Sleep(3000);
                            }
                        }
                    }
                    foreach (var ele in driver.FindElements(By.ClassName("a-IRR-iconList-item")))
                    {
                        if (ele.GetAttribute("data-value") == "XLSX")
                        {
                            ele.Click();
                        }
                    }
                    Thread.Sleep(1500);
                    driver.FindElement(By.ClassName("ui-button--hot")).Click();
                    Thread.Sleep(10000);
                    //define Variant
                    void FindelementsVariant(string customer, string elePagewebsiteid)
                    {
                        var currency = driver.FindElement(By.Id(elePagewebsiteid));
                        var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(currency);
                        foreach (var single in selectElement.Options)
                        {
                            if (single.Text.Contains(customer))
                            {
                                single.Click();
                                Thread.Sleep(1500);
                                break;
                            }
                        }

                    }
                    void customerDefineColumn(List<string> Customer)
                    {

                        for (int i = 0; i <= Customer.Count() - 1; i++)
                        {
                            var elePagewebsiteid = "RGN_COMPARE_PARTLISTS_not_used_group_by_column_" + $"0{i + 1}";
                            var currency = driver.FindElement(By.Id(elePagewebsiteid));
                            var xpathstring = $"//select[@id='{elePagewebsiteid}']/optgroup[@label='Displayed']/option";
                            var xpathstringSecond = $"//select[@id='{elePagewebsiteid}']/optgroup[@label='Other']/option";
                            var elementsSecond = driver.FindElement(By.ClassName("ui-dialog")).FindElements(By.XPath(xpathstringSecond));
                            var elements = driver.FindElement(By.ClassName("ui-dialog")).FindElements(By.XPath(xpathstring));
                            var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(currency);
                            foreach (var single in elements)
                            {
                                if (single.Text.Trim() == Customer[i].Trim())
                                {
                                    single.Click();
                                    Thread.Sleep(1500);
                                    break;
                                }
                            }
                            foreach (var single in elementsSecond)
                            {
                                if (single.Text.Trim() == Customer[i].Trim())
                                {
                                    single.Click();
                                    Thread.Sleep(1500);
                                    break;
                                }
                            }
                            if (i < Customer.Count() - 1)
                            {
                                driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_add_column")).Click();
                                Thread.Sleep(1500);
                            }
                            else
                            {
                                break;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show(ex.Message);
            }

        }
    }

    public class HWPR_CompareList : CompareListInterface
    {
        private static void OnlyLayoutCompare(string LayoutnameBase, string LaoutnameCompared, string PNbase, string PNCompared)
        {

            string url = "https://rb-wam.bosch.com/apxepos/f?p=101:16:111516908018753:::::";
            try
            {
                FirefoxOptions options = new FirefoxOptions();
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"\\bosch.com\dfsrb\DfsCN\DIV\CC\Prj\Engineering_HardWare\ECU_Designer\06_A_Sample\12_Tools&Devices\EPOSUtility2.0\geckodriver.exe");
                options.BinaryLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                using (IWebDriver driver = new OpenQA.Selenium.Firefox.FirefoxDriver(service, options))
                {
                    driver.Navigate().GoToUrl(url);

                    Thread.Sleep(5000);
                    FindelementsLayout(LayoutnameBase, "P16_GET_LAYOUT1", "PopupLov_16_P16_GET_LAYOUT1_dlg");
                    FindelementsLayout(LaoutnameCompared, "P16_GET_LAYOUT2", "PopupLov_16_P16_GET_LAYOUT2_dlg");
                    FindelementsVariant(PNbase, "P16_GET_VARIANT1");
                    FindelementsVariant(PNCompared, "P16_GET_COMPARE");

                    //define Layout
                    void FindelementsLayout(string customer, string elePagewebsiteid, string elePagewebsitemodal)
                    {
                        var element = driver.FindElement(By.Id(elePagewebsiteid));
                        element.SendKeys(customer);
                        Thread.Sleep(1000);
                        foreach (var item in driver.FindElement(By.Id(elePagewebsitemodal)).FindElements(By.ClassName("a-IconList-item")))
                        {
                            if (item.GetAttribute("tabindex") == "0")
                            {
                                item.Click();
                                Thread.Sleep(1500);
                            }
                        }
                    }
                    //define Variant
                    void FindelementsVariant(string customer, string elePagewebsiteid)
                    {
                        var currency = driver.FindElement(By.Id(elePagewebsiteid));
                        var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(currency);
                        foreach (var single in selectElement.Options)
                        {
                            if (single.Text.Contains(customer))
                            {
                                single.Click();
                                Thread.Sleep(1500);
                                break;
                            }
                        }

                    }
                    FindelementsVariant("1. Primary Report", "RGN_COMPARE_PARTLISTS_not_used_saved_reports");
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_actions_button")).Click();
                    Thread.Sleep(1500);
                    driver.FindElement(By.Id("RGN_COMPARE_PARTLISTS_not_used_actions_menu_12")).Click();
                    Thread.Sleep(1500);
                    foreach (var ele in driver.FindElements(By.ClassName("a-IRR-iconList-item")))
                    {
                        if (ele.GetAttribute("data-value") == "XLSX")
                        {
                            ele.Click();
                        }
                    }

                    Thread.Sleep(1500);
                    driver.FindElement(By.ClassName("ui-button--hot")).Click();
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace.ToString());
            }

        }
        public void CompareListGenerate(string PNbase, string PNCompared)
        {
            string url = "https://rb-wam.bosch.com/apxepos/f?p=101:50:102041068515629:::::";
            string LayoutnameBase = "";
            string LaoutnameCompared = "";
            FirefoxOptions options = new FirefoxOptions();
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"\\bosch.com\dfsrb\DfsCN\DIV\CC\Prj\Engineering_HardWare\ECU_Designer\06_A_Sample\12_Tools&Devices\EPOSUtility2.0\geckodriver.exe");
            options.BinaryLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Firefox.FirefoxDriver(service, options))
            {

                driver.Navigate().GoToUrl(url);
                Thread.Sleep(5000);
                LayoutnameBase = getLayoutname(PNbase, LayoutnameBase);
                Thread.Sleep(1500);
                driver.FindElements(By.ClassName("a-IRR-button--remove"))[0].Click();
                Thread.Sleep(5000);
                LaoutnameCompared = getLayoutname(PNCompared, LaoutnameCompared);
                Thread.Sleep(1500);
                Thread.Sleep(1000);
                OnlyLayoutCompare(LayoutnameBase, LaoutnameCompared, PNbase, PNCompared);


                string getLayoutname(string PN, string layname)
                {
                    var element_PNbase = driver.FindElement(By.Id("ERRATA_search_field"));
                    element_PNbase.SendKeys(PN);
                    Thread.Sleep(1500);
                    driver.FindElement(By.Id("ERRATA_search_button")).Click();
                    Thread.Sleep(3500);
                    var table = driver.FindElement(By.ClassName("a-IRR-table"));
                    if ((table is IWebElement) == false) throw new Exception("Error：不支持");
                    var tbody = table.FindElement(By.TagName("tbody"));
                    Thread.Sleep(3500);
                    List<List<IWebElement>> res = new List<List<IWebElement>>();
                    foreach (var tr in tbody.FindElements(By.TagName("tr")))
                    {
                        List<IWebElement> row = new List<IWebElement>();
                        foreach (var td in tr.FindElements(By.TagName("td")))
                        {
                            row.Add(td);
                        }
                        res.Add(row);
                    }
                    Thread.Sleep(1500);
                    List<List<string>> resnew = new List<List<string>>();
                    foreach (var row in res)
                    {
                        List<string> newRow = new List<string>();
                        foreach (var item in row)
                        {
                            var d = item.Text;
                            newRow.Add(item.Text);
                        }
                        resnew.Add(newRow);
                    }
                    List<DataTableClass> dataTableClasses = new List<DataTableClass>();
                    foreach (var tableitem in resnew)
                    {
                        if (tableitem.Count > 0)
                        {
                            DataTableClass tableStage = new DataTableClass();
                            int index = 0;
                            foreach (var pro in typeof(DataTableClass).GetProperties())
                            {
                                pro.SetValue(tableStage, tableitem[index]);
                                index++;
                            }
                            dataTableClasses.Add(tableStage);
                        }
                    }
                    var chosenItem = dataTableClasses.OrderByDescending(p => p.InsertDate.Split(' ')[0].Split('.')[2]).FirstOrDefault();
                    layname = (chosenItem.LaoName + "_V" + chosenItem.LayoutVersion).Trim();
                    return layname;

                }
            }
        }
    }

    public class EPSW_Autosearch : CompareListInterface
    {
        public void EPSW_AutoSearch(string Bnumber, string sampleStage,string DestionFilePath,UIProcessBar uiProcessBar1)
        {
            try
            {
                string url = "https://rb-wam.bosch.com/apxepos/f?p=370:1:103263139809718:::::";
                FirefoxOptions options = new FirefoxOptions();
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"\\bosch.com\dfsrb\DfsCN\DIV\CC\Prj\Engineering_HardWare\ECU_Designer\06_A_Sample\12_Tools&Devices\EPOSUtility2.0\geckodriver.exe");
                options.BinaryLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                using (IWebDriver driver = new OpenQA.Selenium.Firefox.FirefoxDriver(service, options))
                {
                    driver.Navigate().GoToUrl(url);
                    Thread.Sleep(5000);
                    var element_input = driver.FindElement(By.Id("R2223117447222635201_search_field"));
                    element_input.SendKeys(Bnumber);
                    Thread.Sleep(1500);
                    driver.FindElement(By.Id("R2223117447222635201_search_button")).Click();
                    Thread.Sleep(2500);
                    var resnew = findTableelements(driver);
                    List<EDMSDataTableClass> dataTableClasses = new List<EDMSDataTableClass>();
                    foreach (var tableitem in resnew)
                    {
                        if (tableitem.Count > 0)
                        {
                            EDMSDataTableClass tableStage = new EDMSDataTableClass();
                            int index = 0;
                            foreach (var pro in typeof(EDMSDataTableClass).GetProperties())
                            {
                                pro.SetValue(tableStage, tableitem[index]);
                                index++;
                            }
                            dataTableClasses.Add(tableStage);
                        }
                    }
                    //定义委托事件
                    Func<EDMSDataTableClass, DateTime?> ToDateTimeNullable = item =>
                    {
                        DateTime result;
                        if (DateTime.TryParseExact(item.DeliveryDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                            return result;
                        return null; // 或者抛出异常，或者返回默认值（如DateTime.MinValue）
                    };
                    var replaceDateResult = dataTableClasses.OrderByDescending(item => ToDateTimeNullable(item)).ThenBy(item => item.DeliveryDate).ToList();
                    Thread.Sleep(1000);
                    var findSW_BB_Url = "";
                    if (string.IsNullOrEmpty(sampleStage))
                    {
                        findSW_BB_Url = replaceDateResult.Where(item => item.Bno == Bnumber).FirstOrDefault().MasterBNumber.Split(",")[0];
                    }
                    else
                    {
                        findSW_BB_Url = replaceDateResult.Where(item => item.Bno == Bnumber && item.Stage == sampleStage.ToUpper()).FirstOrDefault().MasterBNumber.Split(",")[0];
                    }

                    FirefoxOptions optionsFindSWBB = new FirefoxOptions();
                    FirefoxDriverService service1 = FirefoxDriverService.CreateDefaultService(@"\\bosch.com\dfsrb\DfsCN\DIV\CC\Prj\Engineering_HardWare\ECU_Designer\06_A_Sample\12_Tools&Devices\EPOSUtility2.0\geckodriver.exe");
                    optionsFindSWBB.BinaryLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                    using (IWebDriver drivernew = new OpenQA.Selenium.Firefox.FirefoxDriver(service1,optionsFindSWBB))
                    {
                        drivernew.Navigate().GoToUrl(findSW_BB_Url);
                        var resnewstage = findTableelements(drivernew);
                        List<SW_BB_NO_Class> dataTableClassesnew = new List<SW_BB_NO_Class>();
                        foreach (var tableitem in resnewstage)
                        {
                            if (tableitem.Count > 0)
                            {
                                SW_BB_NO_Class tableStage = new SW_BB_NO_Class();
                                int index = 0;
                                foreach (var pro in typeof(SW_BB_NO_Class).GetProperties())
                                {
                                    pro.SetValue(tableStage, tableitem[index]);
                                    index++;
                                }
                                dataTableClassesnew.Add(tableStage);
                            }
                        }
                        //定义委托事件
                        Func<SW_BB_NO_Class, DateTime?> ToDateTimeNullablenew = item =>
                        {
                            DateTime result;
                            if (DateTime.TryParseExact(item.DeliveryDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                                return result;
                            return null; // 或者抛出异常，或者返回默认值（如DateTime.MinValue）
                        };
                        var replaceDateResultnew = dataTableClassesnew.OrderByDescending(item => ToDateTimeNullablenew(item)).FirstOrDefault();
                        var sw_bb_no = replaceDateResultnew.Sw_bb_no;
                        var deliverydate = replaceDateResultnew.DeliveryDate.Split("-")[2];
                        string EPSWFilepathFirst = @"\\bosch.com\dfsrb\DfsDE\DIV\CS\DE_CS$\Product_Doc\Ecu-vdc\Engineering_Plant\xXSW_Productions_GEN93\ePSW_Productions_GEN93\" + deliverydate + @"\ePSW_Sample_Productions" + @"\" + sw_bb_no;
                        string EPSWFilepathSecond = @"\\bosch.com\dfsrb\DfsDE\DIV\CS\DE_CS$\Product_Doc\Ecu-vdc\Engineering_Plant\xXSW_Productions_GEN93\pTSW_Productions_GEN93\" + deliverydate + @"\pTSW_Sample_Productions" + @"\" + sw_bb_no;
                        string test = @"\\abtvdfs2.de.bosch.com\ismdfs\loc\szh\AS\CNE\006_Cross_Functional\03_HSW_Function_Team\02_HSWDoc\01_HSWTraining\Gen09\RTRT_Training_Materials";
                        string test1 = @"T:\OneWeekPark\autotest";
                        string destionation = @"C:\Users\ZPG1SZH\testresult";
                        string test2 = @"";
                        CopyDirectory(EPSWFilepathFirst, EPSWFilepathSecond, DestionFilePath,true, uiProcessBar1);
                        System.Diagnostics.Process.Start("explorer.exe", DestionFilePath);
                        //CopyDirectory(test, test2, DestionFilePath, true, uiProcessBar1);
                    }
                    static void CopyDirectory(string sourceDir, string sourceDirSecond, string destinationDir, bool recursive,UIProcessBar uiProcessBar1,int totalItems = 0)
                    {
                        try
                        {
                            var dir = new DirectoryInfo(sourceDir);
                            if (!dir.Exists) dir = new DirectoryInfo(sourceDirSecond);
                            DirectoryInfo[] dirEntity = dir.GetDirectories();
                            Directory.CreateDirectory(destinationDir);
                            int itemsCopied = 0;
                            foreach (FileInfo file in dir.GetFiles())
                            {
                                string targetFilePath = Path.Combine(destinationDir, file.Name);
                                file.CopyTo(targetFilePath);
                            }
                            if (recursive)
                            {
                                int subdirCount = dirEntity.Length;
                                totalItems += subdirCount;
                                foreach (DirectoryInfo subdir in dirEntity)
                                {
                                    uiProcessBar1.Value++;
                                    string newDestionationDir = Path.Combine(destinationDir, subdir.Name);
                                    CopyDirectory(subdir.FullName,string.Empty, newDestionationDir, true, uiProcessBar1,totalItems);
                                    itemsCopied++;
                                    UpdateProgressBar(uiProcessBar1,totalItems,itemsCopied);
                                }                               
                            }
                            uiProcessBar1.Value = uiProcessBar1.Maximum;                          
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    //Func of update progressbar
                    static void UpdateProgressBar(UIProcessBar progressBar, int totalItems, int itemsCopied)
                    {                       
                        if (totalItems > 0)
                        {
                            double percentage = (double)itemsCopied / totalItems * 100;
                            progressBar.Value = (int)Math.Round(percentage); // 更新进度条的值
                        }
                        else
                        { 
                            progressBar.Value++; // 简单的递增，可能不准确
                        }
                    }
                    List<List<string>> findTableelements(IWebDriver driver)
                    {
                        var table = driver.FindElement(By.ClassName("a-IRR-tableContainer"));
                        if ((table is IWebElement) == false) throw new Exception("Error：不支持");
                        var tbody = table.FindElement(By.TagName("tbody"));
                        List<List<IWebElement>> res = new List<List<IWebElement>>();
                        foreach (var tr in tbody.FindElements(By.TagName("tr")))
                        {
                            List<IWebElement> row = new List<IWebElement>();
                            foreach (var td in tr.FindElements(By.TagName("td")))
                            {
                                row.Add(td);
                            }
                            res.Add(row);
                        }
                        Thread.Sleep(1500);
                        List<List<string>> resnew = new List<List<string>>();
                        foreach (var row in res)
                        {
                            List<string> newRow = new List<string>();
                            if (row.Count > 0)
                            {
                                foreach (var item in row)
                                {
                                    var aEntity = item.FindElements(By.TagName("a"));
                                    if (aEntity.Count() > 0)
                                    {
                                        string href = aEntity[0].GetAttribute("href");
                                        var d = item.Text;
                                        newRow.Add(href + ',' + d);
                                    }
                                    else
                                    {
                                        newRow.Add(item.Text);
                                    }
                                }
                                resnew.Add(newRow);
                            }
                        }
                        return resnew;
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.ShowError(ex.Message);
            }

        }
    }
}
