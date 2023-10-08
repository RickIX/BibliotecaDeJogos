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
    [Route("jogo")]
    public class JogoController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public JogoController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                List<Jogo> jogos =
                    _ctx.Jogos.ToList();
                return jogos.Count == 0 ? NotFound() : Ok(jogos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("criar")]
        public IActionResult CriarJogo([FromBody] Jogo jogo)
        {
            try
            {
                _ctx.Jogos.Add(jogo);
                _ctx.SaveChanges();
                return Created("", jogo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("atualizar/{id}")]
        public IActionResult AtualizarDetalhesDoJogo([FromRoute] int id, [FromBody] Jogo jogo)
        {
            try
            {
                Jogo jogoCadastrado = _ctx.Jogos.Find(id);
                if (jogoCadastrado != null)
                {
                    jogoCadastrado.NomeJogo = jogo.NomeJogo;
                    jogoCadastrado.Genero = jogo.Genero;
                    _ctx.Jogos.Update(jogoCadastrado);
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

        [HttpDelete]
        [Route("remover/{id}")]
        public IActionResult RemoverJogo([FromRoute] int id)
        {
            try
            {
                Jogo jogoCadastrado = _ctx.Jogos.Find(id);
                if (jogoCadastrado != null)
                {
                    _ctx.Jogos.Remove(jogoCadastrado);
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

        [HttpGet]
        [Route("recomendar")]
        public async Task<IActionResult> RecomendarJogoAleatorio([FromQuery] string genero = null)
        {
            try
            {
                IQueryable<Jogo> jogosQuery = _ctx.Jogos.AsQueryable();

                if (!string.IsNullOrEmpty(genero))
                {
                    if (Enum.TryParse(genero, out GeneroJogo generoEnum))
                    {
                        jogosQuery = jogosQuery.Where(j => j.Genero == generoEnum);
                    }
                    else
                    {
                        return BadRequest("Gênero especificado é inválido.");
                    }
                }

                var jogos = await jogosQuery.ToListAsync();

                if (jogos.Count == 0)
                {
                    return NotFound("Nenhum jogo encontrado com os critérios especificados.");
                }

                var random = new Random();
                var jogoAleatorio = jogos[random.Next(jogos.Count)];

                return Ok(jogoAleatorio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }













    }

    


    
}
