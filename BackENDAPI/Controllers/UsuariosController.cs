using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackENDAPI.Models;
using BackENDAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using BackENDAPI.Services;
using BackENDAPI;

namespace SolidCursos.Controller  
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        protected ExemploContext Context { get; set; }

        public UsuariosController()
        {
            
        }

        // GET: api/Usuarios
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await Context.Usuario.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await Context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [AllowAnonymous]
        [HttpPost("autenticacao")]
        public ActionResult<dynamic> Autenticacao([FromBody] Usuario usuarioRequest)
        {
            // Recupera o usuário
            var user = UsuarioRepository.Find(usuarioRequest.Login, usuarioRequest.Senha);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.Gerar(usuarioRequest);

            // Oculta a senha
            usuarioRequest.Senha = "";

            // Retorna os dados
            return new
            {
                user = usuarioRequest,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            Context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpGet("salvar")]
        public async Task<ActionResult<Usuario>> PostUsuario([FromQuery] Usuario usuario)
        {
            Context.Usuario.Add(usuario);
            await Context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await Context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            Context.Usuario.Remove(usuario);
            await Context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return Context.Usuario.Any(e => e.Id == id);
        }
    }
}
