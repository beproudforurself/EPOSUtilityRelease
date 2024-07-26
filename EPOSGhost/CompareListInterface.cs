using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOSGhost
{
    public interface CompareListInterface
    {
        public  void CompareListGenerate(string PNbase, string PNCompared) { }
        public  void EPSW_AutoSearch(string Bnumber,string sampleStage,string destFilePath,UIProcessBar uiProcessBar1) { }

    }
    
}
