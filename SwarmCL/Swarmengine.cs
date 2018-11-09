using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SwarmCL
{
    public class SwarmEngine
    {
        private TargetTypes targetType;
        private XmlDocument runLog = new XmlDocument();
        private XmlElement runLogNode;
        private string operatingFolder = @"C:\Resources\Swarm";
        public SwarmEngine(TargetTypes _targetType, string target)
        {
            Console.Write("Swarm Created");
            targetType = _targetType;
            if (File.Exists(operatingFolder + @"\RunLog.xml"))
            {
                runLog.Load(operatingFolder + @"\RunLog.xml");

            }
            else
            {
                runLog.LoadXml("<runLog><run target='" + target + "'><targetItems></targetItems></run></runLog>");

            }

            XmlElement foundRunLogNode = (XmlElement)runLog.SelectSingleNode(@"//runLog/run[@target='" + target + "']");
            if (foundRunLogNode != null)
            {
                runLogNode = foundRunLogNode;
            }
            else
            {
                runLogNode = runLog.CreateElement("run");
                runLogNode.SetAttribute("target", target);
                runLog.FirstChild.AppendChild(runLogNode);
            }

            runLogNode.SetAttribute("lastRun", DateTime.Now.ToString("ddMMyyyy HH:mm:ss"));

           
            SaveRunLog();

            Console.WriteLine("SwarmStart");
        }

        public void Start()
        {
            Console.WriteLine("- Swarm start " + DateTime.Now.ToString("HH:mm:ss"));
            List<string> targets = File.ReadAllLines(@"\\WXWS202887\Transport\For Carl\Paths to files 2.txt").ToList();
            targets.ForEach(t =>
            {
                XmlElement item = UpsertTargetItem(t);

            });
            SaveRunLog();
            Console.WriteLine("- Swarm finish " + DateTime.Now.ToString("HH:mm:ss"));

        }

        public void SaveRunLog()
        {
            runLog.Save(operatingFolder + @"\RunLog.xml");
        }

        public XmlElement UpsertTargetItem(string target)
        {
            XmlElement foundRunLogNode = (XmlElement)runLog.SelectSingleNode(@"//runLog/run/targetItems/target[@target='" + target + "']");
            if (foundRunLogNode == null)
            {
                foundRunLogNode = runLog.CreateElement("target");
                foundRunLogNode.SetAttribute("target", target);
                foundRunLogNode.SetAttribute("Status", "new");
                runLog.SelectSingleNode(@"//runLog/run/targetItems").AppendChild(foundRunLogNode);
            }

            return foundRunLogNode;
        }
    }

    public enum TargetTypes
    {
        FileList
    }
}
