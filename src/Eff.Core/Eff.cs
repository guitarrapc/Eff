﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eff.Core
{
    [AsyncMethodBuilder(typeof(EffMethodBuilder<>))]
    public abstract class Eff<TResult>
    {
        public abstract ValueTask<Eff<TResult>> Handle(IEffMethodHandler<TResult> effHandler, IEffectHandler effectHandler);
    }

    public class Await<TSource, TResult> : Eff<TResult>
    {
        private readonly IEffect<TSource> effect;
        private readonly Func<Eff<TResult>> continuation;

        public Await(IEffect<TSource> effect, 
                        Func<Eff<TResult>> continuation)
        {
            this.effect = effect;
            this.continuation = continuation;
        }

        public IEffect<TSource> Effect => effect;
        public Func<Eff<TResult>> Continuation => continuation;
        
        public override ValueTask<Eff<TResult>> Handle(IEffMethodHandler<TResult> effHandler, IEffectHandler effectHandler)
        {
            return effHandler.Handle(this, effectHandler);
        }
    }

    public class SetResult<TResult> : Eff<TResult>
    {
        private readonly TResult result;

        public SetResult(TResult result)
        {
            this.result = result;
        }

        public TResult Result => result;

        public override ValueTask<Eff<TResult>> Handle(IEffMethodHandler<TResult> effHandler, IEffectHandler effectHandler)
        {
            return effHandler.Handle(this, effectHandler);
        }
    }

    public class SetException<TResult> : Eff<TResult>
    {
        private readonly Exception exception;

        public SetException(Exception exception)
        {
            this.exception = exception;
        }

        public Exception Exception => exception;

        public override ValueTask<Eff<TResult>> Handle(IEffMethodHandler<TResult> effHandler, IEffectHandler effectHandler)
        {
            return effHandler.Handle(this, effectHandler);
        }
    }

    public class Delay<TResult> : Eff<TResult>
    {
        private readonly Func<Eff<TResult>> func;

        public Delay(Func<Eff<TResult>> func)
        {
            this.func = func;
        }

        public Func<Eff<TResult>> Func => func;

        public override ValueTask<Eff<TResult>> Handle(IEffMethodHandler<TResult> effHandler, IEffectHandler effectHandler)
        {
            return effHandler.Handle(this, effectHandler);
        }
    }


}
