using System;
using NHapi.Base.Model;

namespace NHapi.Base.Util
{	
	public class SegmentAddingMessageEnumerator : MessageEnumerator
	{
		public SegmentAddingMessageEnumerator(IStructure start, String direction) 
            : base(start, direction)
		{
		}

	    public override bool MoveNext()
	    {
	        WasCurrentAdded = false;
	        return base.MoveNext();
	    }

	    public bool WasCurrentAdded
	    {
	        get; private set;
        }

        protected override bool nextFromGroupEnd(Position currPos, string direction)
        {
            //assert isLast(currPos);
            bool nextExists = true;

            // Adding a unexpected segment to the closest child segment doesn't seem like the best thing to do in all cases.
            bool makeNewSegmentIfNeeded = (currPos.parent.ParentStructure == null);

            //the following conditional logic is a little convoluted -- its meant as an optimization 
            // i.e. trying to avoid calling matchExistsAfterCurrentPosition

            if (!makeNewSegmentIfNeeded &&
                currPos.parent is IMessage)
            {
                nextExists = false;
            }
            else if (!makeNewSegmentIfNeeded ||
                     MatchExistsAfterPosition(currPos, direction, false, true))
            {
                IGroup grandparent = currPos.parent.ParentStructure;
                Index parentIndex = GetIndex(grandparent, currPos.parent);
                Position parentPos = new Position(grandparent, parentIndex);

                try
                {
                    bool parentRepeats = parentPos.parent.IsRepeating(parentPos.index.name);
                    if (parentRepeats && Contains(parentPos.parent.GetStructure(parentPos.index.name, 0), direction, false, true))
                    {
                        nextRep(parentPos);
                    }
                    else
                    {
                        nextExists = SetNextPosition(parentPos, direction);
                    }
                }
                catch (HL7Exception e)
                {
                    throw new ApplicationException("HL7Exception arising from bad index: " + e.Message);
                }
            }
            else
            {
                newSegment(currPos.parent, direction);
            }
            return nextExists;
        }

        /// <summary> Sets the next position to a new segment of the given name, within the 
        /// given group. 
        /// </summary>
        protected void newSegment(IGroup parent, String name)
        {
            WasCurrentAdded = true;
            parent.addNonstandardSegment(name);
            nextPosition = new Position(parent, parent.Names[parent.Names.Length - 1], 0);
        }

    }
}