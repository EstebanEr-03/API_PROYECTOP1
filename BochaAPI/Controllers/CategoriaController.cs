using AutoMapper;
using BochaAPI.Data;
using BochaAPI.Domain;
using BochaAPI.Models.DTO;
using BochaAPI.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BochaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriaController : ControllerBase
    {
        private readonly BochaDbContext dbContext;

        //Con esta implementacion se denota buenas practicas ya que se usa directamente categoriaRepositorio para usar la DB
        private readonly ICategoriaRepositorio categoriaRepositorio;//Es una capa entre la data y la apliacion
        private readonly IMapper mapper;

        public CategoriaController(BochaDbContext dbContext,ICategoriaRepositorio categoriaRepositorio,IMapper mapper)
        {

            this.dbContext = dbContext;
            this.categoriaRepositorio = categoriaRepositorio;
            this.mapper = mapper;
        }
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll() {

            //Se obtiene la data de la database
            var categoriasDomain = await categoriaRepositorio.GetAllAsync();


            //Mapear Domain Models to DTOs
            /*var categoriasDto = new List<categoriaDTO>();
            foreach (var Categoria in categoriasDomain)
            {
                categoriasDto.Add(new categoriaDTO()
                {
                    IdCategoria = Categoria.IdCategoria,
                    Nombre = Categoria.Nombre,
                    Code = Categoria.Code,
                    CategoriaImageURL = Categoria.CategoriaImageURL

                });
            }*/

            //MAPEAR DOMAIN MODELS A DTO
            var categoriasDto= mapper.Map<List<CategoriaDTO>>(categoriasDomain);


            //Retornamos DTOS para el cliente
            return Ok(categoriasDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var Categoria=dbContext.Categoriaes.Find(id);
            //se obtiene la regiion de la base de datos
            //var Categoria =await dbContext.Categoriaes.FirstOrDefaultAsync(x => x.IdCategoria == id); //con esta se pude buscar otras parametros
            var categoria =await categoriaRepositorio.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }
            //Mapear categoriaes domain model to DTO

            mapper.Map<CategoriaDTO>(categoria);

            /*var categoriaDTO = new categoriaDTO
            {

                IdCategoria = categoria.IdCategoria,
                Nombre = categoria.Nombre,
                Code = categoria.Code,
                categoriaImageURL = categoria.categoriaImageURL

            };*/


            //Retorna el DTO al cliente
            return Ok(mapper.Map<CategoriaDTO>(categoria));

        }
        //Post crear nueva categoria
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        //[FromBody] nos da el cliente
        public async Task<IActionResult> Crear([FromBody] AddCategoriaRequestDto nuevaCategoria)
        {
            //Convertir el DTO que recibo del cliente a Domain

            var categoriaDomainModel=mapper.Map<Categoria>(nuevaCategoria);//<DESTINO ES categoria(DOMAIN MODEL)>

            /*var categoriaDomainModel = new categoria
            {
                Code = nuevaCategoria.Code,
                Nombre = nuevaCategoria.Nombre,
                categoriaImageURL = nuevaCategoria.categoriaImageURL

            };*/

            /*Usar el domain para cerar una categoria
            await dbContext.categoriaes.AddAsync(categoriaDomainModel);
            await dbContext.SaveChangesAsync();*/

            //Crear con el repositorio 
            categoriaDomainModel = await categoriaRepositorio.CreateAsync(categoriaDomainModel);

            //Se debe enviar el DTO NO EL DOMEIN

            var categoriaDTO = mapper.Map<CategoriaDTO>(categoriaDomainModel);
            /*var categoriaDTO = new categoriaDTO
            {
                IdCategoria = categoriaDomainModel.IdCategoria,
                Code = categoriaDomainModel.Code,
                Nombre = categoriaDomainModel.Nombre,
                categoriaImageURL = categoriaDomainModel.categoriaImageURL
            };*/
            //Invoca metodo con el nombre para presentarlo en Swagger

            return CreatedAtAction(nameof(GetById), new { id = categoriaDTO.IdCategoria }, categoriaDTO);
        }
        //Update categoria

        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoriaRequestDTO actualizarCategoria)
        {
            //Mapear el dto para el domainmodel

            var categoriaDomainModel = mapper.Map<Categoria>(actualizarCategoria);
            /*{
                Code = actualizarCategoria.Code,
                Nombre = actualizarCategoria.Nombre,
                CategoriaImageURL = actualizarCategoria.CategoriaImageURL

            };*/

            /*Checkea si existe esta Categoria
            var categoriaBuscar = await dbContext.Categoriaes.FirstOrDefaultAsync(x => x.IdCategoria == id);*/
            categoriaDomainModel = await categoriaRepositorio.UpdateAsync(id, categoriaDomainModel);

            if (categoriaDomainModel == null)
            {
                return NotFound();
            }

            //Mapear el dto para el domainmodel
            /*categoriaDomainModel.Code = actualizarCategoria.Code;
            categoriaDomainModel.Nombre= actualizarCategoria.Nombre;
            categoriaDomainModel.CategoriaImageURL = actualizarCategoria.CategoriaImageURL;

            await dbContext.SaveChangesAsync();*/

            //convertir el domain model en un dto

            var categoriaDTO = mapper.Map<CategoriaDTO>(categoriaDomainModel);

            /*{
                IdCategoria = categoriaDomainModel.IdCategoria,
                Code = categoriaDomainModel.Code,
                Nombre= categoriaDomainModel.Nombre,
                CategoriaImageURL = categoriaDomainModel.CategoriaImageURL

            };*/

            return Ok(categoriaDTO);
        }
        //Borrar una Categoria
        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Eliminar([FromRoute]Guid id)
        {
            var categoriaBuscar = await categoriaRepositorio.DeleteAsync(id);
            if (categoriaBuscar == null)
            {
                return NotFound();
            }

            //retornar el objeto borrado
            //map domain model to DTO
            var DtoCategoria =mapper.Map<CategoriaDTO>(categoriaBuscar);
                
                /*new categoriaDTO
            {
                Nombre = categoriaBuscar.Nombre,
                IdCategoria = categoriaBuscar.IdCategoria,
                Code = categoriaBuscar.Code,
                CategoriaImageURL = categoriaBuscar.CategoriaImageURL

            };*/

            return Ok(DtoCategoria);
        }
    }
}
