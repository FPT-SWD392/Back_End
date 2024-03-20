using BusinessObject.DTO;
using BusinessObject.SqlObject;
using JwtTokenAuthorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly ITokenHelper _jwtHelper;

        public TransactionHistoryController(ITransactionHistoryService transactionHistoryService, ITokenHelper jwtHelper)
        {
            _transactionHistoryService = transactionHistoryService;
            _jwtHelper = jwtHelper;
        }

        [Authorize]
        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransactionHistory([FromBody]TransactionRequest transactionRequest)
        {
            var transactionHistory = new TransactionHistory
            {
                UserId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext)),
                Note = transactionRequest.Note,
                Amount = transactionRequest.Amount,
                TransactionDate = DateTime.Now,
                TransactionType = transactionRequest.TransactionType
            };

            await _transactionHistoryService.CreateTransactionHistory(transactionHistory);
            return Ok();
        }

        [Authorize]
        [HttpGet("GetAllTransactions")]
        public async Task<ActionResult<List<TransactionResponse>>> GetAllTransaction()
        {
            var transactions = await _transactionHistoryService.GetAllTransaction();
            var responses = transactions.Select(t => new TransactionResponse
            {
                UserId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext)),
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType
            }).ToList();

            return Ok(responses);
        }

        [Authorize]
        [HttpGet("GetTransactionById/{transactionId}")]
        public async Task<ActionResult<TransactionResponse>> GetTransactionHistoryById(int transactionId)
        {
            var transaction = await _transactionHistoryService.GetTransactionHistoryById(transactionId);
            if (transaction == null)
            {
                return NotFound();
            }

            var response = new TransactionResponse
            {
                UserId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext)),
                Note = transaction.Note,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                TransactionType = transaction.TransactionType
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet("GetTransactionByUserId/{userId}")]
        public async Task<ActionResult<List<TransactionResponse>>> GetUserTransactionHistories(int userId)
        {
            var transactions = await _transactionHistoryService.GetUserTransactionHistories(userId);

            var responses = transactions.Select(t => new TransactionResponse
            {
                UserId = Int32.Parse(_jwtHelper.GetUserIdFromToken(HttpContext)),
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType
            }).ToList();

            return Ok(responses);
        }
        [Authorize]
        [HttpGet("GetOnlyDepositTransactionByUser/{userId}")]
        [SwaggerResponse(200, Type = typeof(List<TransactionResponse>))]
        public async Task<ActionResult<List<TransactionResponse>>> GetOnlyDepositTransactionByUser(int userId)
        {
            var transactionList = await _transactionHistoryService.GetOnlyDepositTransactionByUser(userId);

            var responses = transactionList.Select(t => new TransactionResponse
            {
                UserId = userId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType
            }).ToList();

            return Ok(responses);
        }
        [Authorize]
        [HttpGet("GetAllDepositTransaction")]
        [SwaggerResponse(200, Type=typeof(List<TransactionResponse>))]
        public async Task<ActionResult<List<TransactionResponse>>> GetAllDepositTransaction()
        {
            var transactionList = await _transactionHistoryService.GetAllDepositTransaction();

            var responses = transactionList.Select(t => new TransactionResponse
            {
                UserId = t.UserId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType
            }).ToList();

            return Ok(responses);
        }
    }
}
