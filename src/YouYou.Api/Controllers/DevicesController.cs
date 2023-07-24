using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YouYou.Api.Helpers;
using YouYou.Api.ViewModels;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;

namespace YouYou.Api.Controllers
{
    [Authorize(Roles = "Admin, Operador")]
    [Route("/api/[controller]")]
    [ApiController]
    public class DevicesController : MainController<DevicesController>
    {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IUser _appUser;
        public DevicesController(IDeviceService deviceService,
            IMapper mapper,
            IErrorNotifier erroNotifier,
            IUriService uriService, IUser appUser) : base(erroNotifier, appUser)
        {
            _deviceService = deviceService;
            _mapper = mapper;
            _uriService = uriService;
            _appUser = appUser;
        }
        /// <summary>
        /// Criar Dispositivo
        /// </summary>
        /// <param name="createDeviceViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Device>> CreateDevice(CreateDeviceViewModel createDeviceViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var device = _mapper.Map<Device>(createDeviceViewModel);

            await _deviceService.CreateDevice(device, _appUser.GetUserId());

            return CustomResponse(createDeviceViewModel);
        }
        /// <summary>
        /// Obter Todos os dispositivos com os filtros
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDevices([FromQuery] DeviceFilter filter)
        {
            string route = Request.Path.Value;

            var validFilter = _mapper.Map<DeviceFilter>(filter);
            var devices = _mapper.Map<IEnumerable<ListDeviceViewModel>>(await _deviceService.GetAllDevices(validFilter));

            int totalRecords = await _deviceService.GetTotalRecords(validFilter);
            var pagedReponse = PaginationHelper.CreatePagedReponse<ListDeviceViewModel>(devices.ToList(), validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }
        /// <summary>
        /// Obter Dispositivo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<DeviceDetailDTO> GetDeviceDetail(int id)
        {
            return await _deviceService.GetDetailDevice(id);
        }
        /// <summary>
        /// Editar um dispositivo
        /// </summary>
        /// <param name="updateDeviceViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateDevice(UpdateDeviceViewModel updateDeviceViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var device = await _deviceService.GetDeviceById(updateDeviceViewModel.Id);
            if (device == null) return NotFound();

            _mapper.Map<UpdateDeviceViewModel, Device>(updateDeviceViewModel, device);

            await _deviceService.UpdateDevice(device, _appUser.GetUserId());

            return CustomResponse(updateDeviceViewModel);
        }
        /// <summary>
        /// Deletar um dispositivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDevice(int id)
        {
            var device = await _deviceService.GetDeviceById(id);
            if (device == null) return NotFound();
            if (device.IsActive == true || device.PersonId != null)
                NotifyError("Dispositivo ativo ou vinculado a um vendedor.");
            else
                await _deviceService.DeleteDevice(device);
            return CustomResponse();
        }
        /// <summary>
        /// Desabilitar um dispositivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Disable/{id:int}")]
        public async Task<ActionResult> Disable(int id)
        {
            var device = await _deviceService.GetDeviceById(id);
            if (device == null) return NotFound();

            await _deviceService.Disable(id);
            return CustomResponse();
        }
        /// <summary>
        /// Habilitar um dispositivo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Enable/{id:int}")]
        public async Task<ActionResult> Enable(int id)
        {
            var device = await _deviceService.GetDeviceById(id);
            if (device == null) return NotFound();

            await _deviceService.Enable(id);
            return CustomResponse();
        }
    }
}
