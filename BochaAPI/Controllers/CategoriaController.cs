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

    public class CategoriaController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICategoriaRepositorio categoriaRepositorio;

        public CategoriaController(IMapper mapper, ICategoriaRepositorio categoriaRepositorio)
        {
            this.mapper = mapper;
            this.categoriaRepositorio = categoriaRepositorio;
        }


        //CRUD

        //CREAR PRODUCTO
        [HttpPost]
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> CrearCategoriaAsync([FromBody] AddCategoriaRequestDTO nuevaCategoriaCliente)
        {
            //Map DTO to Domain Model
            var categoriaDomainModel = mapper.Map<Categoria>(nuevaCategoriaCliente);

            await categoriaRepositorio.CrearCategoriaAsync(categoriaDomainModel);

            //Map Domain model to DTO

            return Ok(mapper.Map<CategoriaDTO>(categoriaDomainModel));

        }
        //CREAR OBTENER TODO
        [HttpGet]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filtreQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending)
        {
            var categoriaLista = await categoriaRepositorio.GetAllAsync(filterOn, filtreQuery, sortBy, isAscending ?? true);


            //map Domain A DTO

            return Ok(mapper.Map<List<CategoriaDTO>>(categoriaLista));

        }
        //CREAR OBTENER POR ID
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var categoriaEncontrada = await categoriaRepositorio.GetByIdAsync(id);
            if (categoriaEncontrada == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CategoriaDTO>(categoriaEncontrada));

        }
        // EDITAR
        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] AddCategoriaRequestDTO categoriaDomain)
        {
            var categoriaDomainModel = mapper.Map<Categoria>(categoriaDomain);

            categoriaDomainModel = await categoriaRepositorio.PutAsync(id, categoriaDomainModel);

            if (categoriaDomainModel == null)
            {
                return NotFound();
            }

            var dtocategoria = mapper.Map<CategoriaDTO>(categoriaDomainModel);
            return Ok(dtocategoria);
        }

        // ELIMINAR

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var categoriaEliminar = await categoriaRepositorio.DeleteAsync(id);
            if (categoriaEliminar == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CategoriaDTO>(categoriaEliminar));

        }
    }
}
