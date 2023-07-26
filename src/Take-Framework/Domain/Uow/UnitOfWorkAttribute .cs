using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;

namespace TakeFramework.Domain.Uow
{
    public class UnitOfWorkAttribute : OverrideMethodAspect
    {
        [IntroduceDependency]
        private readonly IUnitOfWork _unitOfWork;


        public override dynamic OverrideMethod()
        {
            this._unitOfWork.BeginTransaction();
            Console.WriteLine("BeginTransaction");
            try
            {
                var result = meta.Proceed();
                this._unitOfWork.Commit();
                Console.WriteLine("Commit");
                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("Rollback");
                this._unitOfWork.Rollback();
                throw;
            }

        }
    }
}
