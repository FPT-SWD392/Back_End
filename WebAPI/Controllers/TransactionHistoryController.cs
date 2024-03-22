using BusinessObject;
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
                TransactionType = transactionRequest.TransactionType,
            };

            await _transactionHistoryService.CreateTransactionHistory(transactionHistory);
            return Ok(new {});
        }

        [Authorize]
        [HttpGet("GetAllTransactions")]
        public async Task<ActionResult<List<TransactionResponse>>> GetAllTransaction()
        {
            var transactions = await _transactionHistoryService.GetAllTransaction();
            var responses = transactions.Select(t => new TransactionResponse
            {
                TransactionId = t.TransactionId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                IsSuccess = t.IsSuccess
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
                UserId = transaction.UserId,
                TransactionId = transactionId,
                Note = transaction.Note,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                TransactionType = transaction.TransactionType,
                IsSuccess = transaction.IsSuccess,
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
                UserId = t.UserId,
                TransactionId = t.TransactionId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                IsSuccess = t.IsSuccess,
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
                TransactionId = t.TransactionId,
                UserId = userId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                IsSuccess = t.IsSuccess,
            }).ToList();

            return Ok(responses);
        }
        [Authorize]
        [HttpGet("GetDepositeTransactionThisUser")]
        [SwaggerResponse(200, Type = typeof(List<TransactionResponse>))]
        public async Task<ActionResult<List<TransactionResponse>>> GetDepositeTransactionThisUser()
        {
            var userId = int.Parse( _jwtHelper.GetUserIdFromToken(HttpContext));
            var transactionList = await _transactionHistoryService.GetOnlyDepositTransactionByUser(userId);

            var responses = transactionList.Select(t => new TransactionResponse
            {
                TransactionId= t.TransactionId,
                UserId = userId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                IsSuccess = t.IsSuccess,
            }).ToList();

            return Ok(responses);
        }
        [Authorize]
        [HttpGet("GetAllTransactionThisUserByStatus")]
        public async Task<ActionResult<List<TransactionResponse>>> GetAllTransactionThisUserByStatus(TransactionType transactionType)
        {
            var userId = int.Parse(_jwtHelper.GetUserIdFromToken(HttpContext));
            var transactionList = await _transactionHistoryService.GetAllTransactionThisUserByStatus(userId, transactionType);

            var responses = transactionList.Select(t => new TransactionResponse
            {
                TransactionId = t.TransactionId,
                UserId = userId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                IsSuccess = t.IsSuccess,
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
                TransactionId = t.TransactionId,
                UserId = t.UserId,
                Note = t.Note,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                TransactionType = t.TransactionType,
                IsSuccess = t.IsSuccess,
            }).ToList();

            return Ok(responses);
        }
        [Authorize]
        [HttpGet("GetAmountDepositWithIn1Month")]
        public async Task<IActionResult> GetAmountDepositWithIn1Month()
        {
            var transactions = await _transactionHistoryService.GetAmountDepositWithIn1Month(TransactionType.DepositManualAdmin);
            transactions += await _transactionHistoryService.GetAmountDepositWithIn1Month(TransactionType.DepositMomo);
            transactions += await _transactionHistoryService.GetAmountDepositWithIn1Month(TransactionType.DepositOther);
            transactions += await _transactionHistoryService.GetAmountDepositWithIn1Month(TransactionType.DepositVnPay);

            return Ok(transactions);
        }
    }
}
