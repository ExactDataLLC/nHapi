using System.Linq;
using NHapi.Base.Model;
using NHapi.Base.Model.Primitive;

namespace NHapi.Base.Conf
{
    public static class Extensions
    {
        public static bool IsEmpty(this IStructure structure)
        {
            AbstractGroup abstractGroup = structure as AbstractGroup;
            if (abstractGroup != null && abstractGroup.Names.Any(n => !string.IsNullOrEmpty(n)))
            {
                return false;
            }

            AbstractSegment abstractSegment = structure as AbstractSegment;
            if (abstractSegment != null)
            {
                for (int i = 1; i <= abstractSegment.NumFields(); i++)
                {
                    IType[] types = abstractSegment.GetField(i);
                    if (types.Any(type => !type.IsEmpty()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IsEmpty(this IType type)
        {
            TSComponentOne tsComponentOne = type as TSComponentOne;
            if (tsComponentOne != null && !string.IsNullOrEmpty(tsComponentOne.Value))
            {
                return false;
            }

            AbstractPrimitive abstractPrimitive = type as AbstractPrimitive;
            if (abstractPrimitive != null && !string.IsNullOrEmpty(abstractPrimitive.Value))
            {
                return false;
            }

            AbstractType abstractType = type as AbstractType;
            if (abstractType != null && !abstractType.ExtraComponents.IsEmpty())
            {
                return false;
            }

            GenericComposite genericComposite = type as GenericComposite;
            if (genericComposite != null && genericComposite.Components.Any(t => !t.IsEmpty()))
            {
                return false;
            }

            Varies varies = type as Varies;
            if (varies != null && !varies.Data.IsEmpty())
            {
                return false;
            }

            return true;
        }

        public static bool IsEmpty(this ExtraComponents extraComponents)
        {
            for (int i = 1; i <= extraComponents.numComponents(); i++)
            {
                if (!extraComponents.getComponent(i).IsEmpty())
                {
                    return false;
                }
            }

            return true;
        }
    }
}