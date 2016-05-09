using System.Linq;

namespace NHapi.Base.Model
{
    public abstract class AbstractComposite : AbstractType, IComposite
    {
        protected AbstractComposite(IMessage message) : base(message)
        {
        }

        protected AbstractComposite(IMessage message, string description)
            : base(message, description)
        {
        }

        public abstract IType[] Components { get; }
        public abstract IType this[int index] { get; }

        public override bool IsEmpty()
        {
            return base.IsEmpty() &&
                   Components.All(c => c.IsEmpty());
        }
    }
}