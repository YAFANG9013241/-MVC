using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.XPath;
using 海面天氣預報MVC.Models;

namespace 海面天氣預報MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? id)
        {
            List<Weather> weatherslList = new List<Weather>();

            XmlDocument doc = new XmlDocument();
            //string xmlPath = @"D:\海面天氣預報0308.xml";
            string path = @"https://opendata.cwb.gov.tw/fileapi/v1/opendataapi/F-A0012-001?Authorization=CWB-BC2E6A08-130F-4DCA-B26C-8E5ADF09F133&downloadType=WEB&format=XML";
            doc.Load(path);

            XmlNode root = doc.DocumentElement.LastChild; //找根元素
            XmlNodeList xnl = root.ChildNodes;
            int count = 1;
            foreach (XmlNode node in xnl)
            {
                if (node.Name != "datasetInfo")
                {
                    for (int i = 1; i < 4; i++)
                    {
                        weatherslList.Add(new Weather
                        {
                            Id = count,
                            Title = node["locationName"].InnerText,
                            Date = Convert.ToDateTime(node.ChildNodes[1].ChildNodes[i]["startTime"].InnerText),
                            Weathers = "https://www.cwb.gov.tw/V8/assets/img/weather_icons/weathers/svg_icon/day/" +
                                   node.ChildNodes[1].ChildNodes[i].ChildNodes[2]["parameterValue"].InnerText.PadLeft(2, '0') + ".svg",
                            weatherTitle = node.ChildNodes[1].ChildNodes[i].ChildNodes[2]["parameterName"].InnerText,
                            WindDir = node.ChildNodes[2].ChildNodes[i].ChildNodes[2]["parameterName"].InnerText,
                            WaveType = node.ChildNodes[5].ChildNodes[i].ChildNodes[2]["parameterName"].InnerText,
                            WaveHeight = node.ChildNodes[4].ChildNodes[i].ChildNodes[2]["parameterName"].InnerText,
                            WindSpeed = node.ChildNodes[3].ChildNodes[i].ChildNodes[2]["parameterName"].InnerText
                        });
                    }
                    count++;
                }
            }

            //ViewBag.head = weatherslList.FirstOrDefault(x => x.Title == "彭佳嶼基隆海面").Title;
            //ViewBag.Message = "海面天氣預報的地區資訊";
            //下拉選單
            //var query = weatherslList.Select(g => g.Title).Distinct();
            //SelectList selectList = new SelectList(query);
            //ViewBag.SelectList = selectList;

            //if (searchString == null)
            //{
            //    searchString = "1";
            //}

            //var result = weatherslList.Where(t => t.Id.Equals(id));
            //ViewBag.area = new SelectList(weatherslList.Distinct(new PropertyComparer<Weather>("Id")), "Id", "Title", searchString);
            //ViewBag.areatitle = result.Select(y => y.Title).FirstOrDefault();

            var demo = weatherslList.Select(t => new { t.Id, t.Title }).Distinct();
            //SelectList list = new SelectList(demo, "id", "Title", selectedValue: id == null ? 1 : id);

            int objId = (id == null ? 1 : (int)id);
            var obj = demo.Where(x => x.Id == objId).FirstOrDefault();
            SelectList list = new SelectList(demo, "id", "Title", obj);

            ViewBag.area = list;
            return View(weatherslList.Where(m => m.Id == (id == null ? 1 : id)));
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        public class PropertyComparer<T> : IEqualityComparer<T>
        {
            private PropertyInfo _PropertyInfo;

            /// <summary>
            /// Creates a new instance of PropertyComparer.
            /// </summary>
            /// <param name="propertyName">The name of the property on type T
            /// to perform the comparison on.</param>
            public PropertyComparer(string propertyName)
            {
                //store a reference to the property info object for use during the comparison
                _PropertyInfo = typeof(T).GetProperty(propertyName,
                    BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
                if (_PropertyInfo == null)
                {
                    throw new ArgumentException(string.Format("{0} is not a property of type {1}.", propertyName, typeof(T)));
                }
            }

            #region IEqualityComparer<T> Members

            public bool Equals(T x, T y)
            {
                //get the current value of the comparison property of x and of y
                object xValue = _PropertyInfo.GetValue(x, null);
                object yValue = _PropertyInfo.GetValue(y, null);

                //if the xValue is null then we consider them equal if and only if yValue is null
                if (xValue == null)
                    return yValue == null;

                //use the default comparer for whatever type the comparison property is.
                return xValue.Equals(yValue);
            }

            public int GetHashCode(T obj)
            {
                //get the value of the comparison property out of obj
                object propertyValue = _PropertyInfo.GetValue(obj, null);

                if (propertyValue == null)
                    return 0;
                else
                    return propertyValue.GetHashCode();
            }

            #endregion IEqualityComparer<T> Members
        }
    }
}