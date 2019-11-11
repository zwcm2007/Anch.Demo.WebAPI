using System.ComponentModel.DataAnnotations;

namespace BaiTeng.POS.Application
{
    public class AddCustomerInput
    {
        [Required]
        public string CustomerName { get; set; }

        //public DateTime SvrBeginDate { get; set; }

        //public DateTime SvrEndDate { get; set; }

        [Required]
        public string ServiceUrl { get; set; } = "";

        //public string vActID { get; set; }
    }
}