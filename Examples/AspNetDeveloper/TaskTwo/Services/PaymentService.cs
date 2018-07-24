using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTwo.Models;

namespace TaskTwo.Services
{
    public class PaymentService
    {
        private readonly InputParams _inputParams;

        public PaymentService(InputParams inputParams)
        {
            _inputParams = inputParams;
        }


        // FUNCTIONS //////////////////////////////////////////////////////////////////////////////
        public async Task<InputParams> Start()
        {
            _inputParams.BalanceAmount = _inputParams.InitAmount;

            var players = new List<Task>();
            for (var i = 0; i < _inputParams.CountPeople; i++)
            {
                var task = new Task(TakeAmount);
                players.Add(task);
            }
            players.ForEach(x => x.Start());

            Task.WaitAll(players.ToArray());
            return _inputParams;
        }
        private void TakeAmount()
        {
            while (true)
            {
                lock (_inputParams)
                {
                    if (CanTakeMore())
                    {
                        //Delay for data base trunsaction
                        _inputParams.BalanceAmount = _inputParams.BalanceAmount - 0.1M;
                    }
                    else
                    {
                        return;
                    }
                }

                Task.Delay(100);
            }
        }
        private Boolean CanTakeMore()
        {
            if (_inputParams.BalanceAmount > 1)
            {
                return true;
            }

            return false;
        }
    }
}