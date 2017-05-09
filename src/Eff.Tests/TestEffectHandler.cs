﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eff.Core
{
    public class TestEffectHandler : IEffectHandler
    {
        private readonly DateTime now;
        public TestEffectHandler(DateTime now)
        {
            this.now = now;
        }

        public TestEffectHandler() : this(DateTime.Now)
        { }

        public async ValueTask<ValueTuple> Handle<TResult>(IEffect<TResult> effect)
        {
            switch (effect)
            {
                case DateTimeNowEffect dateTimeNowEffect:
                    dateTimeNowEffect.SetResult(now);
                    break;
            }

            return ValueTuple.Create();
        }

        public async ValueTask<ValueTuple> Handle<TResult>(TaskEffect<TResult> effect)
        {
            var result = await effect.Task;
            effect.SetResult(result);

            return ValueTuple.Create();
        }

        public async ValueTask<ValueTuple> Handle(TaskEffect effect)
        {
            await effect.Task;
            effect.SetResult(ValueTuple.Create());
            return ValueTuple.Create();
        }

        public async ValueTask<ValueTuple> Handle<TResult>(EffEffect<TResult> effect)
        {
            var result = await effect.Eff;
            effect.SetResult(result);
            return ValueTuple.Create();
        }
    }


}
