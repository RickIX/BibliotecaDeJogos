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
    [Route("jogosjogados")]
    public class JogosJogadosController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public JogosJogadosController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost]
        [Route("registrar")]
        public IActionResult RegistrarJogoJogado([FromBody] JogosJogados jogoJogado)
        {
            try
            {
                // Verifica se o jogador e o jogo associados a este registro existem no banco de dados.
                var jogadorExistente = _ctx.Jogadores.Find(jogoJogado.JogadorId);
                var jogoExistente = _ctx.Jogos.Find(jogoJogado.JogoId);

                if (jogadorExistente == null || jogoExistente == null)
                {
                    return NotFound("Jogador ou Jogo não encontrados.");
                }

                _ctx.RegistrosJogosJogados.Add(jogoJogado);
                _ctx.SaveChanges();
                return Created("", jogoJogado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

[HttpPut]
[Route("atualizar/{id}")]
public IActionResult AtualizarDetalhesDoJogoJogado([FromRoute] int id, [FromBody] JogosJogados novoJogoJogado)
{
    try
    {
        JogosJogados jogoJogadoCadastrado = _ctx.RegistrosJogosJogados.Find(id);
        if (jogoJogadoCadastrado != null)
        {
            // Verifica se o jogador e o jogo associados a este registro existem no banco de dados.
            var jogadorExistente = _ctx.Jogadores.Find(novoJogoJogado.JogadorId);
            var jogoExistente = _ctx.Jogos.Find(novoJogoJogado.JogoId);

            if (jogadorExistente == null || jogoExistente == null)
            {
                return NotFound("Jogador ou Jogo não encontrados.");
            }

            // Atualize os campos do jogo jogado existente com os novos valores.
            jogoJogadoCadastrado.JogadorId = novoJogoJogado.JogadorId;
            jogoJogadoCadastrado.JogoId = novoJogoJogado.JogoId;

            // Salve as alterações no contexto do EF.
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
[Route("remover/{jogadorId}/{jogoId}")]
public IActionResult RemoverJogoJogado([FromRoute] int jogadorId, [FromRoute] int jogoId)
{
    try
    {
        JogosJogados jogoJogadoCadastrado = _ctx.RegistrosJogosJogados
            .FirstOrDefault(j => j.JogadorId == jogadorId && j.JogoId == jogoId);

        if (jogoJogadoCadastrado != null)
        {
            _ctx.RegistrosJogosJogados.Remove(jogoJogadoCadastrado);
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
        [Route("listar")]
        public IActionResult ListarJogosJogados()
        {
            try
            {
                var jogadoresComJogos =
                    _ctx.Jogadores
                        .Select(jogador => new
                        {
                            jogador.JogadorId,
                            jogador.NomeJogador,
                            jogador.Email,
                            Jogos = _ctx.RegistrosJogosJogados
                                .Where(j => j.JogadorId == jogador.JogadorId)
                                .Select(j => new
                                {
                                    j.JogoId,
                                    NomeJogo = j.Jogo.NomeJogo,
                                    Genero = j.Jogo.Genero.ToString()
                                }).ToList()
                        }).ToList();

                return jogadoresComJogos.Count == 0 ? NotFound() : Ok(jogadoresComJogos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
