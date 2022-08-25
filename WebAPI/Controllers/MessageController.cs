using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entidades.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MessageController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IMessage _IMessage;
        private readonly IServiceMessage _IServiceMessage;
        public MessageController(IMessage IMessage, IMapper IMapper, IServiceMessage iServiceMessage)
        {
            _IMessage = IMessage;
            _IMapper = IMapper;
            _IServiceMessage = iServiceMessage;
        }
        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]

        public async Task<List<Notifies>> Add(MessageViewModel messageViewModel)
        {
            messageViewModel.UserId = await RetornarIdUsuarioLogado();
            var messageMap = _IMapper.Map<Message>(messageViewModel);
            await _IServiceMessage.Adicionar(messageMap);
            return messageMap.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPut("/api/Put")]

        public async Task<List<Notifies>> Update(MessageViewModel messageViewModel)
        {
            var messageMap = _IMapper.Map<Message>(messageViewModel);
            await _IServiceMessage.Atualizar(messageMap);
            return messageMap.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/Delete")]

        public async Task<List<Notifies>> Delete(MessageViewModel messageViewModel)
        {
            var messageMap = _IMapper.Map<Message>(messageViewModel);
            await _IMessage.Add(messageMap);
            return messageMap.Notitycoes;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetEntityById")]

        public async Task<MessageViewModel> GetEntityById(int id)
        {
            var message = await _IMessage.GetEntityById(id);
            var messageMap = _IMapper.Map<MessageViewModel>(message);
            return messageMap;
        }

        [Produces("application/json")]
        [HttpGet("/api/List")]
        [Authorize]
        public async Task<List<MessageViewModel>> List()
        {
            var mensagens = await _IMessage.List();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(mensagens);
            return messageMap;
        }

        [Produces("application/json")]
        [HttpGet("/api/ListAtivos")]
        [Authorize]
        public async Task<List<MessageViewModel>> ListAtivos()
        {
            var mensagens = await _IServiceMessage.ListarAtivos();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(mensagens);
            return messageMap;
        }
        private async Task<string> RetornarIdUsuarioLogado()
        {

            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }
            return String.Empty;

        }
    }
}
