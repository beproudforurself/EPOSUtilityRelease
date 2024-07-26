using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EPOSGhost
{
    public class DataTableClass
    {
        public string Action { get; set; }
        public string ActionVari { get; set; }
        public string VM { get; set; }
        public string sp { get; set; }
        public string TcdVariant { get; set; }
        public string ValidSample { get; set; }
        public string VaildPilot { get; set; }
        public string VariName { get; set; }
        public string VariVersion { get; set; }
        public string VariStatus { get; set; }
        public string BProjectName { get; set; }
        public string LaoName { get; set; }
        public string LayoutStatus { get; set; }
        public string LayoutVersion { get; set; }
        public string InsertDate { get; set; }
        public string InsertUser { get; set; }
        public string CustName { get; set; }
        public string ProjOwner { get; set; }
        public string Product { get; set; }
        public string Chipset { get; set; }
        public string uc { get; set; }
        public string epcNr { get; set; }
        public string Projectdescription { get; set; }
    }

    public class EDMSDataTableClass
    {
        public string CustName { get; set; }
        public string Shv { get; set; }
        public string Stage { get; set; }
        public string Generation { get; set; }
        public string Bno { get; set; }
        public string MasterBNumber { get; set; }
        public string EdmsSw { get; set; }
        public string EdmsRelType { get; set; }
        public string Edmstype { get; set; }
        public string Esw { get; set; }
        public string EdmsStatus { get; set; }
        public string ProdStat { get; set; }
        public string ExpectedRd { get; set; }
        public string DeliveryDate { get; set; }
        public string Producer { get; set; }
        public string Requestor { get; set; }
        public string HswBl { get; set; }
        public string Uc { get; set; }
        public string Memory { get; set; }
        public string FotaCsw { get; set; }
        public string Rbbldrstatus { get; set; }
        public string Rbbldrversion { get; set; }
    }

    public class SW_BB_NO_Class
    {
        public string Customer { get; set; }
        public string Variant { get; set; }
        public string PartNo { get; set; }
        public string Esw { get; set; }
        public string Epsw { get; set; }
        public string Status { get; set; }
        public string Typ { get; set; }
        public string Reqduedate { get; set; }
        public string Negduedate { get; set; }
        public string ExpectedRd { get; set; }
        public string DeliveryDate { get; set; }
        public string SdDate { get; set; }
        public string HswBl { get; set; }
        public string Uc { get; set; }
        public string Dattyp { get; set; }
        public string ReasonForProd { get; set; }
        public string Kanban { get; set; }
        public string Deviation { get; set; }
        public string BlBuild { get; set; }
        public string Producer { get; set; }
        public string Rbbldrstatus { get; set; }
        public string Rbbldrversion { get; set; }
        public string Remark { get; set; }
        public string Id { get; set; }
        public string Sw_bb_no { get; set; }
        public string Sam { get; set; }
    }

}
