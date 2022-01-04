using System;
using Logger.Helper;
using Logger.Models;
using Logger.Services.Abstractions;

namespace Logger.Services
{
    public class ActionsService : IActionsService
    {
        private readonly ILoggerService _logger;

        public ActionsService(ILoggerService logger)
        {
            _logger = logger;
        }

        public bool WriteInfoLog()
        {
            _logger.LogAsync(LogType.Info, $"Start method: {nameof(WriteInfoLog)}");
            return true;
        }

        public bool SkipLogic()
        {
            throw new BusinessException("Skipped logic in method");
        }

        public bool BreakLogic()
        {
            throw new Exception("I broke a logic");
        }
    }
}