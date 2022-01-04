using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logger.Services.Abstractions;

namespace Logger.Helper
{
    public class App
    {
        private readonly IActionsService _actions;
        private readonly ILoggerService _loggerService;
        private readonly ILoggerFileService _fileService;

        public App(IActionsService actions, ILoggerService loggerService, ILoggerFileService fileService)
        {
            _actions = actions;
            _loggerService = loggerService;
            _fileService = fileService;
        }

        public async Task SimulateWork()
        {
            var random = new Random();
            bool result;
            for (var i = 0; i < 50; i++)
            {
                try
                {
                    switch (random.Next(1, 4))
                    {
                        case 1:
                            result = _actions.WriteInfoLog();
                            break;
                        case 2:
                            result = _actions.SkipLogic();
                            break;
                        case 3:
                            result = _actions.BreakLogic();
                            break;
                    }
                }
                catch (BusinessException ex)
                {
                    await _loggerService.LogWarning($"Action got this custom exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    await _loggerService.LogError($"Action failed by a reason: {ex}");
                }
            }
        }

        public void Run()
        {
            _loggerService.OnBackup += async (slim, str) =>
            {
                await _fileService.WriteBackupAsync(str);
                slim.Release();
            };

            var list = new List<Task>();
            list.Add(Task.Run(async () =>
            {
                await SimulateWork();
            }));
            list.Add(Task.Run(async () =>
            {
                await SimulateWork();
            }));

            Task.WaitAll(list.ToArray());
        }
    }
}