using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanHuan.Domain
{
    public class OperationResult
    {
        public bool OperationSucceeded { get; private set; }
        public List<string> Errors { get; private set; }
        public List<string> Info { get; private set; }

        public OperationResult(bool operationSucceeded,
            IEnumerable<string> errors = null,
            IEnumerable<string> info = null)
        {
            OperationSucceeded = operationSucceeded;
            Errors = errors?.ToList() ?? new List<string>(0);
            Info = info?.ToList() ?? new List<string>(0);
        }
    }

    public class OperationResult<T> : OperationResult
    {       
        public T ObjectState { get; private set; }

        public OperationResult(bool operationSucceeded, 
                                IEnumerable<string> errors = null, 
                                IEnumerable<string> info = null, 
                                T objectState = default(T))
            : base(operationSucceeded, errors, info)
        {           
            ObjectState = objectState;
        }
    }
}
