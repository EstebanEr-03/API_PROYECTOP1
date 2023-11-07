using AutoMapper;
using BochaAPI.Domain;
using BochaAPI.Models.DTO;
using BochaAPI.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BochaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MarcaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMarcaRepositorio marcaRepositorio;

        public MarcaController(IMapper mapper, IMarcaRepositorio marcaRepositorio)
        {
            this.mapper = mapper;
            this.marcaRepositorio = marcaRepositorio;
        }


        //CRUD

        //CREAR PRODUCTO
        [HttpPost]
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> CrearMarcaAsync([FromBody] AddMarcaRequestDTO nuevaMarcaCliente)
        {
            //Map DTO to Domain Model
            var marcaDomainModel = mapper.Map<Marca>(nuevaMarcaCliente);

            await marcaRepositorio.CrearMarcaAsync(marcaDomainModel);

            //Map Domain model to DTO

            return Ok(mapper.Map<MarcaDTO>(marcaDomainModel));

        }
        //CREAR OBTENER TODO
        [HttpGet]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filtreQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var marcasLista = await marcaRepositorio.GetAllAsync(filterOn,filtreQuery, sortBy, isAscending ?? true);


            //map Domain A DTO

            return Ok(mapper.Map<List<MarcaDTO>>(marcasLista));

        }
        //CREAR OBTENER POR ID
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var marcaEncontrada = await marcaRepositorio.GetByIdAsync(id);
            if (marcaEncontrada == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<MarcaDTO>(marcaEncontrada));

        }
        // EDITAR
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] AddMarcaRequestDTO marcaDomain)
        {
            var marcaDomainModel = mapper.Map<Marca>(marcaDomain);

            marcaDomainModel = await marcaRepositorio.PutAsync(id, marcaDomainModel);

            if (marcaDomainModel == null)
            {
                return NotFound();
            }

            var dtomarca = mapper.Map<MarcaDTO>(marcaDomainModel);
            return Ok(dtomarca);
        }

        // ELIMINAR

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) 
        {
            var marcaEliminar = await marcaRepositorio.DeleteAsync(id);
            if (marcaEliminar == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<MarcaDTO>(marcaEliminar));

        }
    }
}
