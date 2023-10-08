using GameLibrary.Data;
using GameLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Controllers
{
    [ApiController]
    [Route("jogador")]
    public class JogadorController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public JogadorController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        // GET: api/jogador/listar
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Jogador> jogadores = _ctx.Jogadores.ToList();
                return jogadores.Count == 0 ? NotFound() : Ok(jogadores);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("registrarJogador")]
        public IActionResult RegistrarJogador([FromBody] Jogador jogador)
        {
            try
            {
                _ctx.Jogadores.Add(jogador);
                _ctx.SaveChanges();
                return Created("", jogador);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public IActionResult Buscar([FromRoute] int id)
        {
            try
            {
                Jogador jogadorCadastrado = _ctx.Jogadores.Find(id);
                return jogadorCadastrado != null ? Ok(jogadorCadastrado) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            try
            {
                Jogador jogadorCadastrado = _ctx.Jogadores.Find(id);
                if (jogadorCadastrado != null)
                {
                    _ctx.Jogadores.Remove(jogadorCadastrado);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("atualizarPerfil/{id}")]
        public IActionResult AtualizarPerfil([FromRoute] int id, [FromBody] Jogador jogador)
        {
            try
            {
                Jogador jogadorCadastrado = _ctx.Jogadores.Find(id);
                if (jogadorCadastrado != null)
                {
                    jogadorCadastrado.NomeJogador = jogador.NomeJogador;
                    jogadorCadastrado.Senha = jogador.Senha;
                    jogadorCadastrado.Email = jogador.Email;
                    _ctx.Jogadores.Update(jogadorCadastrado);
                    _ctx.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
