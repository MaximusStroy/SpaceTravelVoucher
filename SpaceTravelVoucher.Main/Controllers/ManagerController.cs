using KellermanSoftware.CompareNetObjects;
using MessagePack;
using MessagePack.Resolvers;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Models;
using SpaceTravelVoucher.DataGisEisEp.EPMessageExchangeWS.Repository.Voucher;
using SpaceTravelVoucher.Main.Repository.Voucher;
using SpaceTravelVoucher.Main.ViewModels;
using Newtonsoft.Json;
using System.Reflection;
using SpaceTravelVoucher.Main.Services;

namespace SpaceTravelVoucher.Main.Controllers
{
    public class ManagerController : Controller
    {
        private readonly VoucherRepository _repository;
        public CreateVoucherRequest VoucherRequest { get; set; }
        public CreateOrEditVoucherViewModel CreateOrEditVoucherViewModel { get; set; }

        public ManagerController()
        {
            VoucherRequest = new CreateVoucherRequest();
            _repository = new VoucherRepository();
            CreateOrEditVoucherViewModel = new CreateOrEditVoucherViewModel();
        }
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Index()
        {
            return View();
        }

        #region Voucher
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult CreateVoucher()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateVoucherPost()
        {
            VoucherRequest = CreateOrEditVoucherViewModel.CreateVoucherRequest;
            if (CreateOrEditVoucherViewModel.IndividualCustomer != null)
                VoucherRequest.Voucher.Order.Customer.Item = CreateOrEditVoucherViewModel.IndividualCustomer;
            else if (CreateOrEditVoucherViewModel.CompanyCustomer != null)
                    VoucherRequest.Voucher.Order.Customer.Item = CreateOrEditVoucherViewModel.CompanyCustomer;
                else if (CreateOrEditVoucherViewModel.ProprietorCustomer != null)
                        VoucherRequest.Voucher.Order.Customer.Item = CreateOrEditVoucherViewModel.ProprietorCustomer;

            await _repository.InsertVoucher(VoucherRequest);

            return View(nameof(ListVoucher));
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult SaveInfo(CreateVoucherRequest model)
        {
            CreateOrEditVoucherViewModel.CreateVoucherRequest = model;
            return RedirectToAction(nameof(CreateVoucher));
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult SaveIndividualType(CreateOrEditVoucherViewModel individual)
        {
            CreateOrEditVoucherViewModel.IndividualCustomer = individual.IndividualCustomer;
            CreateOrEditVoucherViewModel.CompanyCustomer = null;
            CreateOrEditVoucherViewModel.ProprietorCustomer = null;
            return RedirectToAction(nameof(CreateVoucher));
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult SaveCompanyType(CreateOrEditVoucherViewModel company)
        {
            CreateOrEditVoucherViewModel.CompanyCustomer = company.CompanyCustomer;
            CreateOrEditVoucherViewModel.IndividualCustomer = null;
            CreateOrEditVoucherViewModel.ProprietorCustomer = null;
            return RedirectToAction(nameof(CreateVoucher));
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult SaveProprietorType(CreateOrEditVoucherViewModel proprietor)
        {
            CreateOrEditVoucherViewModel.ProprietorCustomer = proprietor.ProprietorCustomer;
            CreateOrEditVoucherViewModel.IndividualCustomer = null;
            CreateOrEditVoucherViewModel.CompanyCustomer = null;
            return RedirectToAction(nameof(CreateVoucher));
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult SaveContract(CreateOrEditVoucherViewModel voucher)
        {
            CreateOrEditVoucherViewModel.CreateVoucherRequest.Voucher.Order.contractNumber = voucher.CreateVoucherRequest.Voucher.Order.contractNumber;
            CreateOrEditVoucherViewModel.CreateVoucherRequest.Voucher.Order.cost = voucher.CreateVoucherRequest.Voucher.Order.cost;
            CreateOrEditVoucherViewModel.CreateVoucherRequest.Voucher.Order.contractDate = voucher.CreateVoucherRequest.Voucher.Order.contractDate;
            return RedirectToAction(nameof(CreateVoucher));
        }


        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ListVoucher()
        {
            return View(await _repository.GetShort());
        }

        public async Task<IActionResult> DetailsVoucher(string number, string codeAuth = "")
        {
            CreateOrEditVoucherViewModel obj = new CreateOrEditVoucherViewModel();
            var list = await _repository.GetMany();
            if (codeAuth == "")
            {
                var model = list.FirstOrDefault(x => x.CreateVoucherRequest.Voucher.number == number);
                if (model != null)
                    obj = Helper.GetVoucher(model);
                return View(obj);
            }
            else
            {
                var model = list.FirstOrDefault(x => x.CreateVoucherRequest.Voucher.number == number && x.CodeAuth == codeAuth);
                if (model != null)
                    obj = Helper.GetVoucher(model);
                return View(obj);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> DetailsVoucher(string number)
        {
            if (ModelState.IsValid)
            {
                var list = await _repository.GetMany();
                var model = list.FirstOrDefault(x => x.CreateVoucherRequest.Voucher.number == number);
                if (model != null)
                {
                    return Redirect($"~/Manager/DetailsVoucher?number={number}");
                }
                else return RedirectToAction(nameof(ListVoucher));
            }
            else return RedirectToAction(nameof(ListVoucher));
        }

        public async Task<IActionResult> EditVoucher(string number)
        {
            var list = await _repository.GetMany();
            var model = list.FirstOrDefault(x => x.CreateVoucherRequest.Voucher.number == number);
            return View(model);
        }

        public string NumberVoucher()
        {
            var num = new GetVoucherNumber();
            var number = num.GetVoucherNumberResponse();
            return $"{number.voucherNumber}\n{number.voucherNumberCreatedDateTime}";
        }
        #endregion

        #region Tour agent
        #endregion

        #region Parther
        #endregion
    }
}
