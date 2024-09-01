using C1_Blazor_Demo_DotNet8.Components.Pages;
using C1_Blazor_Demo_DotNet8.Models.ViewModels;
using C1_Blazor_Demo_DotNet8.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C1_Blazor_Demo_DotNet8.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _DashboardRepository;
        private readonly IConfiguration _configuration;

        public DashboardController(DashboardRepository RepositoryDashboard, IConfiguration configuration)
        {

            _DashboardRepository = RepositoryDashboard;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetReworkdata")]
        public IActionResult GetReworkdata()
        {
            string userID = "sakib31";
            string branchId = "0031";
            var result = _DashboardRepository.GetReworkdata(userID, branchId);
            return Ok(result);
        }
        public class CustomerModel
        {
            public int CustomerId { get; set; }
            public string CustomerCategoryId { get; set; }
            public string CustomerFullName { get; set; }
            public string CustomerShortName { get; set; }
            public string CustomerSalutation { get; set; }
            public string CustomerFirstName { get; set; }
            public string CustomerMiddleName { get; set; }
            public string CustomerLastName { get; set; }

            public DateTime MakeDt { get; set; }
            public string Auth1stBy { get; set; }
            public DateTime? Auth1stDt { get; set; }
            public string Auth2ndBy { get; set; }
            public DateTime? Auth2ndDt { get; set; }
            public string AuthStatusId { get; set; }
            public string LastAction { get; set; }

            [Required(ErrorMessage = "Make BY is required.")]
            public string MakeBy { get; set; }
        }

        [HttpPost]
        [Route("add_customer")]
        public IActionResult add_customer([FromBody] CustomerModel objmodel)
        {
            if (objmodel == null)
            {
                return BadRequest("Invalid user data.");
            }

            // Call the repository method
            var result = _DashboardRepository.AddCustomer(objmodel.CustomerId, objmodel.CustomerShortName,objmodel.CustomerSalutation,
                                                            objmodel.CustomerFirstName,objmodel.CustomerMiddleName,
                                                                objmodel.CustomerLastName );

            return Ok(result);
        }
        public class AccountModel
        {
            public int BranchId { get; set; }

            [Required(ErrorMessage = "Account Number is required")]
            [Range(10000000000, 99999999999, ErrorMessage = "Account Number must be exactly 11 digits")]
            public int AccountNumber { get; set; }

            [Required(ErrorMessage = "Product ID is required")]
            [RegularExpression(@"^\d{3}$", ErrorMessage = "Product ID must be a 3-digit number")]
            public int ProductId { get; set; }

            public string CurrId { get; set; }
            public string AccType { get; set; }

            [Required(ErrorMessage = "Customer ID is required")]
            [Range(1000000000, 9999999999, ErrorMessage = "Customer ID must be exactly 10 digits")]
            public long? CustomerId { get; set; }

            public int GroupId { get; set; }
            public string AccStatus { get; set; }
            public decimal BalanceLcy { get; set; }

            public DateTime MakeDt { get; set; } = DateTime.Now;
            public string Auth1stBy { get; set; }
            public DateTime? Auth1stDt { get; set; }
            public string Auth2ndBy { get; set; }
            public DateTime? Auth2ndDt { get; set; }
            public string LastAction { get; set; }
            public string AuthStatusId { get; set; }

            [Required(ErrorMessage = "Make BY is required.")]
            public string MakeBy { get; set; }

        }
        [HttpGet]
        [Route("add_account")]
        public IActionResult AddAccount([FromBody] AccountModel objmodel)
        {
            if (objmodel == null)
            {
                return BadRequest("Invalid user data.");
            }

            // Call the repository method
            var result = _DashboardRepository.AddAccounts(objmodel.BranchId, objmodel.AccountNumber, objmodel.ProductId,objmodel.CurrId,
                                                            objmodel.CustomerId, objmodel.GroupId, objmodel.BalanceLcy, objmodel.MakeBy);

            return Ok(result);
        }
    }
}
