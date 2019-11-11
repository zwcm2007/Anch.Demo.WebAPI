using Abp.Domain.Entities;

namespace Anch.Demo.Core
{
    public class SM_Account : Entity<string>
    {
        //public string vActID { get; set; }
        public string vActName { get; set; }

        public int iStartYear { get; set; }

        public int iStartMonth { get; set; }

        public string vCreateMan { get; set; }
        
        //dtCreateDate
        //vActPath
        //bStart
        //vStartMan
        //dtStart
        //bCarryForward
        //vCFMan
        //dtCarryForward
        //bStopAct
        //vStopMan
        //dtStopAct
        //vMemo
        //vDefine1
        //vDefine2
        //iDefine3
        //fDefine4
        //fDefine5
        //bDefine6
        //dtDefine7
    }
}