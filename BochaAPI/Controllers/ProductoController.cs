using AutoMapper;
using BochaAPI.Domain;
using BochaAPI.Models.DTO;
using BochaAPI.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BochaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductoController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductoRepositorio productoRepositorio;

        public ProductoController(IMapper mapper, IProductoRepositorio productoRepositorio)
        {
            this.mapper = mapper;
            this.productoRepositorio = productoRepositorio;
        }


        //CRUD

        //CREAR PRODUCTO
        [HttpPost]
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> CrearProductoAsync([FromBody] AddProductoRequestDTO nuevaProductoCliente)
        {
            //Map DTO to Domain Model
            var productoDomainModel = mapper.Map<Producto>(nuevaProductoCliente);

            await productoRepositorio.CrearProductoAsync(productoDomainModel);

            //Map Domain model to DTO

            return Ok(mapper.Map<ProductoDTO>(productoDomainModel));

        }
        //CREAR OBTENER TODO
        [HttpGet]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filtreQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var productosLista = await productoRepositorio.GetAllAsync(filterOn,filtreQuery, sortBy, isAscending ?? true);


            //map Domain A DTO

            return Ok(mapper.Map<List<ProductoDTO>>(productosLista));

        }
        //CREAR OBTENER POR ID
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var productoEncontrada = await productoRepositorio.GetByIdAsync(id);
            if (productoEncontrada == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductoDTO>(productoEncontrada));

        }
        // EDITAR
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] AddProductoRequestDTO productoDomain)
        {
            var productoDomainModel = mapper.Map<Producto>(productoDomain);

            productoDomainModel = await productoRepositorio.PutAsync(id, productoDomainModel);

            if (productoDomainModel == null)
            {
                return NotFound();
            }

            var dtoproducto = mapper.Map<ProductoDTO>(productoDomainModel);
            return Ok(dtoproducto);
        }

        // ELIMINAR

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) 
        {
            var productoEliminar = await productoRepositorio.DeleteAsync(id);
            if (productoEliminar == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductoDTO>(productoEliminar));

        }
    }
}
